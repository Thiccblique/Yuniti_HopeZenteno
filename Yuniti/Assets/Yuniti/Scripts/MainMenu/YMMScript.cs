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
    }
    public void CloseSetting()
    {
        settingsPannel.SetActive(false);
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
}
