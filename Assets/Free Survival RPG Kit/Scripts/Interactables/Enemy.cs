using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This makes our enemy interactable. */

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable {
   
    [Header("[Enemy Respawn Settings]")]
    public string prefabName = "";
    public float respawnTime = 10f;
    public Vector3 respawnSpot;
    public Quaternion respawnRotation;

    [Header("[Enemy Stats Settings]")]
    public CharacterStats stats;

    [Header("[Enemy Loot Settings]")]
    public int minGoldGive = 100;
    public int maxGoldGive = 1000;
    public Transform lootPosition;
    public GameObject[] loots;

    void Start ()
	{      
        // get init position for respawn
        respawnSpot = gameObject.transform.position;
        respawnRotation = gameObject.transform.rotation;

        // get stats objects
		stats = GetComponent<CharacterStats>();
		stats.OnHealthReachedZero += Die;
    }

	// When we interact with the enemy: We attack it.
	public override void Interact()
	{
		Debug.Log("Interact");
        CharacterCombat combatManager = Player.instance.playerCombatManager;
		combatManager.Attack(stats);
	}

    IEnumerator CallRespawnFunction()
    {
        // log
        Debug.LogFormat("Enemy {0} Dead", gameObject.name);

        // desactive graphics and colliders to prevent player interact with dead object
        // gameObject.transform.Find("Graphics").gameObject.SetActive(false);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;

        // wait respawn time
        yield return new WaitForSeconds(respawnTime);

        // load prefab
        GameObject enemyPrefab = Resources.Load("Prefabs/NPCs/" + prefabName, typeof(GameObject)) as GameObject;

        // call respawn function
        GameObject.Find("GameManager").GetComponent<RespawnManager>().DoEnemyRespawn(enemyPrefab, respawnSpot, respawnRotation, gameObject);
    }

    void Die()
    {
        // set normal animations and remove this dead object from player target variablee
        Player.instance.GetComponent<CharacterCombat>().Normal();
        Player.instance.GetComponent<PlayerController>().focus = null;
        Player.instance.GetComponent<CharacterCombat>().healthSlider.gameObject.SetActive(false);

        // remove player from this dead object target variable
        GetComponent<EnemyController>().target = null;

        //call death function
        GetComponent<CharacterCombat>().Death();

        // disable battle ui
        GetComponent<CharacterCombat>().healthSlider.gameObject.SetActive(false);

        // drop loot
        DropLoot();

        // call respawn function
        StartCoroutine(CallRespawnFunction());
	}

    void DropLoot()
    {
        // pick random gold amount and  give to player
        int goldValue = Random.Range(minGoldGive, maxGoldGive);
        GameObject.Find("GameManager").GetComponent<Inventory>().gold += goldValue;

        // pick random item to drop
        int lootIndex = Random.Range(0, loots.Length);
        GameObject currentLoot = loots[lootIndex];

        Instantiate(currentLoot, lootPosition.position, Quaternion.identity);
        Debug.Log("Drop Loot: " + currentLoot.name + ", Gold Drop: " + goldValue);
    }

}
