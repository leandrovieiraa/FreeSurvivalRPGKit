using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAnimator : CharacterAnimator {

	void Awake() {
		EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
	}

	protected override void Start() {
		base.Start ();
	}
	
	protected override void OnAttack() {
        // attack animation
		base.OnAttack ();
	}

	void OnEquipmentChanged(Equipment newItem, Equipment oldItem) {
		
		if (oldItem != null) {
			if (oldItem.equipSlot == EquipmentSlot.Weapon) {
				// unequip old weapon
			}
			if (oldItem.equipSlot == EquipmentSlot.Shield) {
                // unequip old shield
            }
        }

		if (newItem != null) {
			
			if (newItem.equipSlot == EquipmentSlot.Weapon) {
				// set new animation from new weapon
			}
			if (newItem.equipSlot == EquipmentSlot.Shield) {
                // set new animation from new shield
            }
        }
	}
}
