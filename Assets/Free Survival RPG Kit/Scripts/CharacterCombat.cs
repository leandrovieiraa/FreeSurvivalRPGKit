using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This resorts combat for all characters. */

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

	public float attackRate = 1f;
	private float attackCountdown = 0f;

	public event System.Action OnAttack;

	CharacterStats myStats;
	CharacterStats enemyStats;


	void Start ()
	{
		myStats = GetComponent<CharacterStats>();
		// set health ui graphics with values
	}

	void Update ()
	{
		attackCountdown -= Time.deltaTime;
	}

	public void Attack (CharacterStats enemyStats)
	{
		if (attackCountdown <= 0f)
		{
			this.enemyStats = enemyStats;
			attackCountdown = 1f / attackRate;

			StartCoroutine(DoDamage(enemyStats,.6f));

			if (OnAttack != null) {
				OnAttack ();
			}
		}
	}


	IEnumerator DoDamage(CharacterStats stats, float delay) {
		Debug.Log("Start damage system");
		yield return new WaitForSeconds (delay);

		Debug.Log (transform.name + " attacking for " + myStats.damage.GetValue () + " damage");
		enemyStats.TakeDamage (myStats.damage.GetValue ());
	}
}
