using System;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    AudioSource audioSource;

    public void Initialize()
    {
        EventsManager.OnBackgroundMusicChanged_Event += OnBackgroundMusicChanged;

        audioSource = GetComponent<AudioSource>();

        SetVolume(SettingsData.backgroundMusicVolume);
        if (SettingsData.isBackgroundMusicOn)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    private void OnDisable()
    {
        EventsManager.OnBackgroundMusicChanged_Event -= OnBackgroundMusicChanged;
    }


    public void OnBackgroundMusicChanged()
    {
        SetVolume(SettingsData.backgroundMusicVolume);

        if (SettingsData.isBackgroundMusicOn)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    public void TurnOn()
    {
        if (audioSource.clip == null)
            audioSource.clip = Resources.Load<AudioClip>("Sounds/BackgroundMusic/Deep_In_Space");

        audioSource.loop = true;

        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void TurnOff()
    {
        audioSource.Stop();
        audioSource.clip = null;
        audioSource.loop = false;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
