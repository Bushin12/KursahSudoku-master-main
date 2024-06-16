using System;
using UnityEngine;

public class Theme : MonoBehaviour
{
    public static Theme Global { get; private set; }
    public bool _isLight { private set; get; }
    public static event Action ChangedColors;


    private void Awake()
    {
        ChangedColors = null;
        Global = this;
        _isLight = PlayerPrefs.GetInt("Theme", 1) == 1 ? true : false ;
    }
    public void SetTheme(bool newValueTheme)
    {
         
        _isLight = newValueTheme;
        ChangedColors?.Invoke();
    }
}
