using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [Header("CircleFade")]
    public GameObject circleFade;
    
    [Header("PauseMenu")]
    public GameObject pauseMenu;
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
        pauseMenu.SetActive(false);
        circleFade.SetActive(true);
        SceneTransition();
        yield return new WaitForSeconds(2f);
        circleFade.SetActive(false);
        loadingScreenPrefab.SetActive(true);
        MuteAudio();
        //LoadingChecker();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);

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

    private void SceneTransition()
    {
        Animator animator = circleFade.GetComponent<Animator>();
        animator.Play("FadeIn");
    }


}
