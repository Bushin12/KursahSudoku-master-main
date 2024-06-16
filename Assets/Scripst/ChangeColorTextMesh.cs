using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorTextMesh : ChangeColor
{
    private TextMesh _text;

    public override void Start()
    {
        _text = GetComponent<TextMesh>();
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
