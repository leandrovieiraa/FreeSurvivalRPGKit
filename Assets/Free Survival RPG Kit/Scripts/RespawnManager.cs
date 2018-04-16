using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnManager : MonoBehaviour 
{
    [Header("[Player Respawn Settings]")]
    public GameObject playerPrefab;
    public GameObject DeathUI;
    public Transform respawnSpot;

    public void LateUpdate()
    {
        // get progress bar time
        float time = DeathUI.GetComponentInChildren<ProgressBar>().currentPercent;

        // check if reach time for respawn
        if (time >= 100f)
        {
            // fix time
            time = 100f;

            // enable respawn button
            DeathUI.transform.Find("RespawnButton").GetComponent<Button>().interactable = true;
        }
    }

    public void DoPlayerRespawn()
    {
        // Create a new player
        GameObject newPlayer = Instantiate(playerPrefab, respawnSpot.position, Quaternion.identity) as GameObject;

        // Set camera dependence
        Camera.main.GetComponent<CameraController>().target = newPlayer.transform;

        // Destroy old player
        Destroy(GameObject.FindGameObjectWithTag("Player"));

        // Reset Death UI and disable
        DeathUI.transform.Find("RespawnButton").GetComponent<Button>().interactable = false;
        DeathUI.GetComponentInChildren<ProgressBar>().currentPercent = 0;
        DeathUI.SetActive(false);

        // Log
        Debug.LogFormat("Respawn Player {0}", newPlayer.name);
    }

    public void DoEnemyRespawn(GameObject enemyPrefab, Vector3 respawnSpot, Quaternion respawnRotation, GameObject oldEnemy)    
    {
        // Create a new player
        GameObject newEnemy = Instantiate(enemyPrefab, respawnSpot, respawnRotation) as GameObject;

        // Destroy old enemy
        Destroy(oldEnemy);

        // Log
        Debug.LogFormat("Respawn Enemy {0}", newEnemy.name);
    }
}
