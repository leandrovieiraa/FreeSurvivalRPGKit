using UnityEngine;

/* An Item that can be consumed. So far that just means regaining health */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Consumable")]
public class Consumable : Item {

	public int healthGain;		// How much Health?
    public int hungerGain;		// How much Hunger?
    public int thirstyGain;      // How much Thirsty?

    // This is called when pressed in the inventory
    public override void Use()
	{
		// Heal the player
		PlayerStats playerStats = Player.instance.playerStats;

        if (healthGain > 0)
		    playerStats.Heal(healthGain);

        if (hungerGain > 0)
            playerStats.HealHunger(hungerGain);

        if (thirstyGain > 0)
            playerStats.HealThirsty(thirstyGain);

        Debug.Log(name + " consumed!");

		RemoveFromInventory();	// Remove the item after use
	}

}
