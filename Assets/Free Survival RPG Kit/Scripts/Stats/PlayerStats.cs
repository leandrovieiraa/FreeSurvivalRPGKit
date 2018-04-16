using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
	This component is derived from CharacterStats. It adds some things:
		- Gaining modifiers when equipping items
        - Hunger and thirsty system
        - Damage type for survival aspects     
*/

public class PlayerStats : CharacterStats
{
    public Stat maxHunger;          // Maximum amount of hunger
    public int currentHunger { get; protected set; }    // Current amount of hunger

    public Stat maxThirsty;          // Maximum amount of thirsty
    public int currentThirsty { get; protected set; }    // Current amount of thirsty

    public event System.Action OnHungerReachedZero;
    public event System.Action OnThirstyhReachedZero;

    // Use this for initialization
    public override void Start ()
    {
		base.Start();

        currentHunger = maxHunger.GetValue();
        currentThirsty = maxThirsty.GetValue();

        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
	}

	void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
	{
		if (newItem != null) {
			armor.AddModifier (newItem.armorModifier);
			damage.AddModifier (newItem.damageModifier);
		}

		if (oldItem != null)
		{
			armor.RemoveModifier(oldItem.armorModifier);
			damage.RemoveModifier(oldItem.armorModifier);
		}
	}

    public void SurvivalDamage(int damage, string type)
    {
        if (type.Equals("Hunger"))
        {
            // Subtract damage from hunger
            currentHunger -= damage;
            Debug.Log(transform.name + " takes " + damage + " hunger.");

            // If we hit 0. Die.
            if (currentHunger <= 0)
            {
                if (OnHungerReachedZero != null)
                {
                    OnHungerReachedZero();
                }
            }
        }

        if (type.Equals("Thirsty"))
        {
            // Subtract damage from hunger
            currentThirsty -= damage;
            Debug.Log(transform.name + " takes " + damage + " thirsty.");

            // If we hit 0. Die.
            if (currentThirsty <= 0)
            {
                if (OnThirstyhReachedZero != null)
                {
                    OnThirstyhReachedZero();
                }
            }
        }
    }

    // Heal Hunger
    public void HealHunger(int amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger.GetValue());
    }

    // Heal thirsty
    public void HealThirsty(int amount)
    {
        currentThirsty += amount;
        currentThirsty = Mathf.Clamp(currentThirsty, 0, maxThirsty.GetValue());
    }
}
