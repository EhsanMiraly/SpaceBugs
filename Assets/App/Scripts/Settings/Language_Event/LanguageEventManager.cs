using System;
using UnityEngine;

public class LanguageEventManager
{
    public static event OnNotify OnLanguageChanged_Event;
    public static void InvokeOnLanguageChanged()
    {
        OnLanguageChanged_Event?.Invoke();
    }
}
