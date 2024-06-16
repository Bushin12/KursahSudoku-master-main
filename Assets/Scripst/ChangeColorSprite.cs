using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorSprite : ChangeColor
{
    private SpriteRenderer _spriteRenderer;

    public override void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        base.Start();
    }
    public override void Change()
    {
        if (_theme._isLight)
        {
            _spriteRenderer.color = _light;
        }
        else
        {
            _spriteRenderer.color = _dark;
        }
    }
}
