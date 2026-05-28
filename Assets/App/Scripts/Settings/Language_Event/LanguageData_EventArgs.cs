using System;
using UnityEngine.UIElements;

public class LanguageData_EventArgs : EventArgs
{
    public int CurrentLanguageIndex { get; }

    public LanguageData_EventArgs(int currentLanguageIndex)
    {
        CurrentLanguageIndex = currentLanguageIndex;
    }
}
