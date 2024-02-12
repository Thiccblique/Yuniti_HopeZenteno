using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;

    public GameObject loadingScreen;
    public GameObject loadingScreenPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(StartLoadingScreen(sceneName));
    }
   
    IEnumerator StartLoadingScreen(string sceneName)
    {
        Time.timeScale = 1f;
        loadingScreenPrefab.SetActive(true);
        MuteAudio();
        //LoadingChecker();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);

    }

    private void LoadingChecker()
    {
        if (loadingScreen != null)
        {
            // Loop through all game objects in the scene
            foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
            {
                // Check if the current object is not the one with active time
                if (obj != loadingScreen)
                {
                    // Stop the time scale for all objects except the one with active time
                    Time.timeScale = 0f;
                }
            }
        }
        else
        {
            Debug.LogError("No object with active time specified!");
        }
    }

    void MuteAudio()
    {
        // Find all audio sources in the scene
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        // Iterate through each audio source and mute it
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = 0f;
        }
    }


}
