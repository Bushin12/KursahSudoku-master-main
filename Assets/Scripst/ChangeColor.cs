using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    protected Theme _theme { private set; get; }
    [SerializeField] protected Color _light, _dark;

    public virtual void Start()
    {
        _theme = Theme.Global;
        Theme.ChangedColors += Change;
        Change();
    }

    public virtual void Change()
    {

    }

}
