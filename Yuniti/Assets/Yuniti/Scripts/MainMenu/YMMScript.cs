using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;

public class YMMScript : MonoBehaviour
{
    public AudioMixer mainMixer;
    public GameObject settingsPannel;

    public GameObject levelMenu;
    public GameObject optionsMenu;
    public GameObject mainMenu;

    [Header("CircleFade")]
    public GameObject circleFade;
    private void Start()
    {
        CirclePlay();
        Invoke("CircleDeactive", 1.1f);
        FindAnyObjectByType<AudioManager>().Play("MainMenu");
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void SetVolume (float volume)
    {
        mainMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }


    public void OpenSettings()
    {
        settingsPannel.SetActive(true);

        mainMenu.SetActive(false);
    }

    public void CloseSetting()
    {
        settingsPannel.SetActive(false);

        mainMenu.SetActive(true);
    }

    public void OpenLevels()
    {
        levelMenu.SetActive(true);

        mainMenu.SetActive(false);
    }

    public void CloseLevels()
    {
        levelMenu.SetActive(false);

        mainMenu.SetActive(true);
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);

        mainMenu.SetActive(false);
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);

        mainMenu.SetActive(true);
    }

    private void CirclePlay()
    {
        Animator animator = circleFade.GetComponent<Animator>();
        animator.Play("Fadeout");

    }
    private void CircleDeactive()
    {
        circleFade.SetActive(false);
    }

    public void OpenWebsite()
    {
        Application.OpenURL("https://sites.google.com/d/1jFRox7JWuGw-c72qpkPLCpFcnG0CGVqU/p/1ApVU7RV-Mj3U-V_XwYJ1T_VJqvN_Eho8/edit");
    }
}
