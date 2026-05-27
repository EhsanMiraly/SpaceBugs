using System;
using UnityEngine;

public class SoundsEventManager
{
    public static event EventHandler<SoundData_EventArgs> OnBackgroundMusicChanged_Event;
    public static void InvokeOnBackgroundMusicChanged(GameObject sender, SoundData_EventArgs soundData_EventArgs)
    {
        OnBackgroundMusicChanged_Event?.Invoke(sender, soundData_EventArgs);
    }

    public static event EventHandler<SoundData_EventArgs> OnSoundEffectsChanged_Event;
    public static void InvokeOnSoundEffectsChanged(GameObject sender, SoundData_EventArgs soundData_EventArgs)
    {
        OnSoundEffectsChanged_Event?.Invoke(sender, soundData_EventArgs);
    }
}
