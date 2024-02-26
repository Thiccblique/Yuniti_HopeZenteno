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
}
