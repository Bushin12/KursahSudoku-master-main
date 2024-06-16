using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorText : ChangeColor
{
    private Text _text;

     public override  void Start()
     {
        _text = GetComponent<Text>();
        base.Start();
     }

    public override void Change()
    {
        if (_theme._isLight)
        {
            _text.color = _light;
        }
        else
        {
            _text.color = _dark;
        }
    }
}
