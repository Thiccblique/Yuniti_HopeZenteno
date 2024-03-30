using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MasterVolume : MonoBehaviour
{
    public Slider volumeSlider;
    //public AudioSource[] audioSources;

    public Slider brightnessSlider;
    public Light sceneLight;

    [SerializeField] private TextMeshProUGUI volText = null;
    [SerializeField] private TextMeshProUGUI brightText = null;


    void Start()
    {
        // Ensure volume slider value matches current master volume
        volumeSlider.value = AudioListener.volume;
        // Subscribe to slider's OnValueChanged event
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChanged(); });

        brightnessSlider.value = sceneLight.intensity;
    }

    void OnVolumeChanged()
    {
        // Update master volume based on slider value
        AudioListener.volume = volumeSlider.value;
    }

    public void VolumeChange(float value)
    {
        float localValue = value;
        volText.text = localValue.ToString("0" + "%");
    }

    public void BrightChange(float value)
    {
        float localValue = value;
        brightText.text = localValue.ToString("0" +"%");
    }

    public void AjustBrightness(float newBrightness)
    {
        sceneLight.intensity = newBrightness;
    }
}
