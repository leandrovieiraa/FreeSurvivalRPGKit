using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This resorts combat for all characters. */

[RequireComponent(typeof(CharacterStats))]
public class SurvivalSystem : MonoBehaviour
{

    public float hungerDelay = 60f;
    public int hungerDamage = 1;
    Coroutine hungerCoroutine;

    public float thirstyDelay = 90f;
    public int thirstyDamage = 1;
    Coroutine thirstyCoroutine;

    PlayerStats myStats;

    void Start()
    {
        // get player stats
        myStats = GetComponent<PlayerStats>();

        // set health ui graphics with values

        // start survival damage
        hungerCoroutine = StartCoroutine(DoHungerDamage(hungerDelay));
        thirstyCoroutine = StartCoroutine(DoThirstyDamage(thirstyDelay));
    }

    void Update()
    {
        if (myStats.currentHunger <= 0)
        {
            Debug.Log("Hunger Death, stop coroutine");
            StopCoroutine(hungerCoroutine);
            StopCoroutine(thirstyCoroutine);
        }

        if (myStats.currentThirsty <= 0)
        {
            Debug.Log("Thirsty Death, stop coroutine");
            StopCoroutine(hungerCoroutine);
            StopCoroutine(thirstyCoroutine);
        }
    }

    IEnumerator DoHungerDamage(float delay)
    {
        Debug.Log("Start survival damage system");
        while (true)
        {      
            yield return new WaitForSeconds(delay);
            myStats.SurvivalDamage(hungerDamage, "Hunger");
        }   
    }

    IEnumerator DoThirstyDamage(float delay)
    {
        Debug.Log("Start survival damage system");
        while (true)
        {
            yield return new WaitForSeconds(delay);
            myStats.SurvivalDamage(thirstyDamage, "Thirsty");
        }  
    }
}
