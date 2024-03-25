using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using TMPro;

public class YMMScript : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volText = null;
    [SerializeField] private TextMeshProUGUI brightText = null;

    public Light sceneLight;
    public Slider brightnessSlider;
    
    public GameObject settingsPannel;

    public GameObject levelMenu;
    public GameObject optionsMenu;
    public GameObject mainMenu;

    [Header("CircleFade")]
    public GameObject circleFade;

    [Header("Site")]
    private string website;
    private void Start()
    {
        CirclePlay();
        Invoke("CircleDeactive", 1.1f);
        FindAnyObjectByType<AudioManager>().Play("MainMenu");

        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }

        else
        {
            Load();
        }

        brightnessSlider.value = sceneLight.intensity;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    public void PercentChange(float value)
    {
        float localValue = value;
        volText.text = localValue.ToString("0" + "%");
    }

    public void BrightChange(float value)
    {
        float localValue = value;
        brightText.text = localValue.ToString("0" + "%");
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    public void AjustBrightness(float newBrightness)
    {
        sceneLight.intensity = newBrightness;
    }

    public void SetFullscreenMode(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
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

    public void OpenWebsite(string website)
    {
        Application.OpenURL(website);
    }
}
