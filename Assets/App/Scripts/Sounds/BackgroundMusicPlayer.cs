using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    //this should be saved
    bool isOn = true;
    float volume = 0.1f;//0-1
    //this should be saved

    AudioSource audioSource;

    public void Initialize()
    {
        audioSource = GetComponent<AudioSource>();

        if (isOn)
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
        audioSource.clip = Resources.Load<AudioClip>("Sounds/BackgroundMusic/Deep_In_Space");
        audioSource.loop = true;
        audioSource.Play();
        SetVolume(volume);
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
