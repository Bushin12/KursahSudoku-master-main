using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorImage : ChangeColor
{
    private Image _image;

    public override void Start()
    {
        _image = GetComponent<Image>();
        base.Start();
    }
    public override void Change()
    {
        if (_theme._isLight)
        {
            _image.color = _light;
        }
        else
        {
            _image.color = _dark;
        }
    }
}
