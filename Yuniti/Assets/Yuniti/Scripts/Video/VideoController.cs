using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Video;
public class VideoController : MonoBehaviour
{
    public GameObject videoObject;
    VideoPlayer videoPlayer;
    public float changetime;
    public float videotime;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        // Optionally, you can set up any additional configurations here
        StopVideo();
    }
    private void Update()
    {
        changetime -= Time.deltaTime;
        videotime -= Time.deltaTime;

        if (changetime <= 0)
        {
            PlayVideo();
        }

        if (videotime <= 0)
        {
            StopVideo();
        }
    }

    public void PlayVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
    }

    public void PauseVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Pause();
        }
    }

    public void StopVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }
    }
}
