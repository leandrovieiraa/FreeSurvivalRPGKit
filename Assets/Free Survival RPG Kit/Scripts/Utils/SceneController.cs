using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    // scene name to change
    public string sceneName = "";

    // ui load bar object
    public GameObject loadBarObject;
    Slider slider;

    // async operation
    AsyncOperation async;

    // call load level async 
    public void CallLoadLevel()
    {
        slider = loadBarObject.GetComponent<Slider>();
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    // change scene with async function, with this we make a load scene with progress bar, it's a cool feature xD
    private IEnumerator LoadSceneAsync(string levelName)
    {
        loadBarObject.SetActive(true);
        async = SceneManager.LoadSceneAsync(levelName);
        async.allowSceneActivation = false;

        while(async.isDone == false)
        {
            slider.value = async.progress;
            if(async.progress == 0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    // function to handle quit game
    public void QuitGame()
    {
        Debug.Log("Exit game pressed, bye bye!.");
        Application.Quit();
    }
}
