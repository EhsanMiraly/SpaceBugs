using System;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    AudioSource audioSource;

    public void Initialize()
    {
        SoundsEventManager.OnBackgroundMusicChanged_Event += OnBackgroundMusicChanged;

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
        SoundsEventManager.OnBackgroundMusicChanged_Event -= OnBackgroundMusicChanged;
    }


    public void OnBackgroundMusicChanged(object sender, SoundData_EventArgs soundData_EventArgs)
    {
        SetVolume(soundData_EventArgs.Volume);

        if (soundData_EventArgs.IsOn)
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
        audioSource.clip = null;
        audioSource.loop = false;
        audioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
