using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class SurvivalSystem : MonoBehaviour
{
    // main canvas
    GameObject canvas;

    // hunger variables
    [Header("[Hunger Settings]")]
    public float hungerDelay = 60f;
    public int hungerDamage = 1;
    Coroutine hungerCoroutine;

    // hunger levels (use for messages)
    bool hungerLevel1;
    bool hungerLevel2; 
    bool hungerLevel3;

    [Header("[Thirsty Settings]")]
    public float thirstyDelay = 90f;
    public int thirstyDamage = 1;
    Coroutine thirstyCoroutine;

    // thirsty levels (use for messages)
    bool thirstyLevel1;
    bool thirstyLevel2;
    bool thirstyLevel3;

    // player stats
    PlayerStats myStats;

    void Start()
    {
        // get main canvas
        canvas = GameObject.Find("Canvas");

        // get player stats
        myStats = GetComponent<PlayerStats>();

        // set health and survival ui graphics with values
        canvas.transform.Find("VitalslUI").Find("HealthSlider").GetComponent<Slider>().maxValue = myStats.maxHealth.GetValue();
        canvas.transform.Find("VitalslUI").Find("HealthSlider").GetComponent<Slider>().value = myStats.currentHealth;

        canvas.transform.Find("VitalslUI").Find("HungerSlider").GetComponent<Slider>().maxValue = myStats.maxHunger.GetValue();
        canvas.transform.Find("VitalslUI").Find("HungerSlider").GetComponent<Slider>().value = myStats.currentHunger;

        canvas.transform.Find("VitalslUI").Find("ThirstySlider").GetComponent<Slider>().maxValue = myStats.maxThirsty.GetValue();
        canvas.transform.Find("VitalslUI").Find("ThirstySlider").GetComponent<Slider>().value = myStats.currentThirsty;


        // start survival damage
        hungerCoroutine = StartCoroutine(DoHungerDamage(hungerDelay));
        thirstyCoroutine = StartCoroutine(DoThirstyDamage(thirstyDelay));
    }

    void Update()
    {
        // Update UI's
        canvas.transform.Find("VitalslUI").Find("HealthSlider").GetComponent<Slider>().value = myStats.currentHealth;
        canvas.transform.Find("VitalslUI").Find("HungerSlider").GetComponent<Slider>().value = myStats.currentHunger;
        canvas.transform.Find("VitalslUI").Find("ThirstySlider").GetComponent<Slider>().value = myStats.currentThirsty;

        // Hunger System
        if (myStats.currentHunger <= 75 && myStats.currentHunger > 50 && !hungerLevel1)
        {
            // array of messages
            string[] messages = { "I want to eat something", "I'm feeling hungry", "I feel hungry", "My stomach grumbles"};

            // pick random message
            int index = Random.Range(0, messages.Length - 1);
            string getMessage = messages[index];

            // show message
            canvas.transform.Find("Notifications").Find("HungerPopNot").Find("Description").GetComponent<Text>().text = getMessage;
            canvas.transform.Find("Notifications").Find("HungerPopNot").gameObject.SetActive(true);     
            
            // log
            Debug.Log(getMessage);

            // pick one message
            hungerLevel1 = true;
        }
        else if(myStats.currentHunger <= 50 && myStats.currentHunger > 25 && !hungerLevel2)
        {
            // array of messages
            string[] messages = { "I'm extremely hungry", "My stomach grumbled violently", "I'm starving"};

            // pick random message
            int index = Random.Range(0, messages.Length - 1);
            string getMessage = messages[index];

            // show message
            canvas.transform.Find("Notifications").Find("HungerPopNot").Find("Description").GetComponent<Text>().text = getMessage;

            // reactive notification
            canvas.transform.Find("Notifications").Find("HungerPopNot").gameObject.SetActive(false);
            canvas.transform.Find("Notifications").Find("HungerPopNot").gameObject.SetActive(true);

            // log
            Debug.Log(getMessage);

            // pick one message
            hungerLevel2 = true;
        }
        else if(myStats.currentHunger <= 25 && myStats.currentHunger > 0 && !hungerLevel3)
        {
            // array of messages
            string[] messages = { "I'm dying of starvation"};

            // pick random message
            int index = Random.Range(0, messages.Length - 1);
            string getMessage = messages[index];

            // show message
            canvas.transform.Find("Notifications").Find("HungerPopNot").Find("Description").GetComponent<Text>().color = Color.red;
            canvas.transform.Find("Notifications").Find("HungerPopNot").Find("Description").GetComponent<Text>().text = getMessage;

            // reactive notification
            canvas.transform.Find("Notifications").Find("HungerPopNot").gameObject.SetActive(false);
            canvas.transform.Find("Notifications").Find("HungerPopNot").gameObject.SetActive(true);

            // log
            Debug.Log(getMessage);

            // pick one message
            hungerLevel3 = true;
        }
        else if (myStats.currentHunger <= 0)
        {
            // death message
            string message = "Hunger Death";

            // log
            Debug.Log(message);

            // stop coroutines
            StopCoroutine(hungerCoroutine);
            StopCoroutine(thirstyCoroutine);
        }

        // Thirsty System
        if (myStats.currentThirsty <= 75 && myStats.currentThirsty > 50 && !thirstyLevel1)
        {
            // array of messages
            string[] messages = { "I feel thirsty", "I'm thirsty", "I need a drink", "I feel like having a drink" };

            // pick random message
            int index = Random.Range(0, messages.Length - 1);
            string getMessage = messages[index];

            // show message
            canvas.transform.Find("Notifications").Find("ThirstyPopNot").Find("Description").GetComponent<Text>().text = getMessage;
            canvas.transform.Find("Notifications").Find("ThirstyPopNot").gameObject.SetActive(true);

            // log
            Debug.Log(getMessage);

            // pick one message
            thirstyLevel1 = true;
        }
        else if (myStats.currentThirsty <= 50 && myStats.currentThirsty > 25 && !thirstyLevel2)
        {
            // array of messages
            string[] messages = { "I want to drink something", "I really need to drink"};

            // pick random message
            int index = Random.Range(0, messages.Length - 1);
            string getMessage = messages[index];

            // show message
            canvas.transform.Find("Notifications").Find("ThirstyPopNot").Find("Description").GetComponent<Text>().text = getMessage;

            // reacttive notification
            canvas.transform.Find("Notifications").Find("ThirstyPopNot").gameObject.SetActive(false);
            canvas.transform.Find("Notifications").Find("ThirstyPopNot").gameObject.SetActive(true);

            // log
            Debug.Log(getMessage);

            // pick one message
            thirstyLevel2 = true;
        }
        else if (myStats.currentThirsty <= 25 && myStats.currentThirsty > 0 && !thirstyLevel3)
        {
            // array of messages
            string[] messages = { "I'm dying of dehydration" };

            // pick random message
            int index = Random.Range(0, messages.Length - 1);
            string getMessage = messages[index];

            // show message
            canvas.transform.Find("Notifications").Find("ThirstyPopNot").Find("Description").GetComponent<Text>().color = Color.red;
            canvas.transform.Find("Notifications").Find("ThirstyPopNot").Find("Description").GetComponent<Text>().text = getMessage;

            // reacttive notification
            canvas.transform.Find("Notifications").Find("ThirstyPopNot").gameObject.SetActive(false);
            canvas.transform.Find("Notifications").Find("ThirstyPopNot").gameObject.SetActive(true);

            // log
            Debug.Log(getMessage);

            // pick one message
            thirstyLevel3 = true;
        }
        else if (myStats.currentThirsty <= 0)
        {
            // death message
            string message = "Thirsty Death";

            // log
            Debug.Log(message);

            // stop coroutines
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
