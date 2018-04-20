using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/* This class just makes it faster to get certain components on the player. */

 public class Player : MonoBehaviour {

	#region Singleton

	public static Player instance;

	void Awake ()
	{
		instance = this;
	}

    #endregion

    public CharacterCombat playerCombatManager;
    public PlayerStats playerStats;

    void Start()
    {
        // set minimap target on start
        GameObject.Find("MinimapCamera").GetComponent<Minimap>().player = this.gameObject.transform;

        // set all functions of player
        playerStats.OnHealthReachedZero += Die;
        playerStats.OnHungerReachedZero += Die;
        playerStats.OnThirstyhReachedZero += Die;
    }

	void Die()
    {
        // call death function
        playerCombatManager.Death();

        // enable death ui
        GameObject.Find("GameManager").GetComponent<RespawnManager>().DeathUI.SetActive(true);
        
        // disable battle health slider
        GetComponent<CharacterCombat>().healthSlider.gameObject.SetActive(false);
    }
}
