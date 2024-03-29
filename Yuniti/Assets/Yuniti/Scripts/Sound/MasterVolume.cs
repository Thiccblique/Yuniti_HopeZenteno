using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MasterVolume : MonoBehaviour
{
    public Slider volumeSlider;
    //public AudioSource[] audioSources;

    void Start()
    {
        // Ensure volume slider value matches current master volume
        volumeSlider.value = AudioListener.volume;
        // Subscribe to slider's OnValueChanged event
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChanged(); });
    }

    void OnVolumeChanged()
    {
        // Update master volume based on slider value
        AudioListener.volume = volumeSlider.value;
    }
}
