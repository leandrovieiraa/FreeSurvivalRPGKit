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

        // remove player from this dead object target variable
        GetComponent<EnemyController>().target = null;

        //call death function
        GetComponent<CharacterCombat>().Death();

        // call respawn function
        StartCoroutine(CallRespawnFunction());
	}

}
