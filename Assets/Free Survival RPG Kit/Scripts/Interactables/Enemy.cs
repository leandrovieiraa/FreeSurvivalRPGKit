using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This makes our enemy interactable. */

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable {

	CharacterStats stats;

	void Start ()
	{
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

	void Die() {
        // play die animation, send events to player and destroy enemy object
        Player.instance.GetComponent<CharacterAnimator>().animator.SetBool("inBattle", false);
        Player.instance.GetComponent<CharacterAnimator>().animator.SetBool("Attack", false);
        Destroy (gameObject);
	}

}
