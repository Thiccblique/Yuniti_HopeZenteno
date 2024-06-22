using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

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

    public GameObject darkPost;

    [Header("CircleFade")]
    public GameObject circleFade;

    [Header("Site")]
    private string website;

    [Header("Glitch Effect")]
    public PostProcessVolume postProcessVolume;
    ChromaticAberration chromaticAberration;
    public bool isMouseDown = false;

    private void Start()
    {
        GlitchStart();
        CirclePlay();
        Invoke("CircleDeactive", 2.5f);
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

        darkPost.SetActive(false);
    }

    private void Update()
    {
        GlitchUpdate();
    }

    /* Glitch effect Code */

    private void GlitchStart()
    {
        postProcessVolume.profile.TryGetSettings(out chromaticAberration);
    }

    private void GlitchUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            ToggleChromaticAberration(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            ToggleChromaticAberration(false);
        }
    }

    public void ToggleChromaticAberration(bool isEnabled)
    {
        if(chromaticAberration != null)
        {
            chromaticAberration.enabled.value = isEnabled;    
        }
    }

    /* End Glitch effect Code */
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

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void PlayAudio()
    {
        FindAnyObjectByType<AudioManager>().Play("Select");
    }

    public void DarkProcess()
    {
        if(darkPost.activeInHierarchy == false)
        {
            darkPost.SetActive(true);
        }
        else
        {
            darkPost.SetActive(false);
        }
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
