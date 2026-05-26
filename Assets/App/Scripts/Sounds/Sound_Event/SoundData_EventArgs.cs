using System;
using UnityEngine;

public class SoundData_EventArgs : EventArgs
{
    public bool IsOn { get; }
    public float Volume { get; }

    public SoundData_EventArgs(bool isOn, float volume)
    {
        IsOn = isOn;
        Volume = volume;
    }
}
