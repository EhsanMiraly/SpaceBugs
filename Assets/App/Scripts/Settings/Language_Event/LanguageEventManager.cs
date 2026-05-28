using System;
using UnityEngine;

public class LanguageEventManager
{
    public static event EventHandler<LanguageData_EventArgs> OnLanguageChanged_Event;
    public static void InvokeOnLanguageChanged(GameObject sender, LanguageData_EventArgs languageData_EventArgs)
    {
        OnLanguageChanged_Event?.Invoke(sender, languageData_EventArgs);
    }
}
