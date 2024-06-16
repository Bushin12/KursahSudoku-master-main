using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PutCell : MonoBehaviour
{
    public int Value { private set; get; }
    [SerializeField] private GameObject _elements;
    [SerializeField] private TextMesh _valueText;
    [SerializeField] private Color _valueZero;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TextMesh _countText;
    public static List<PutCell> AllPutCell = new List<PutCell>();
    private fillingUser _filling;
    private int _count;

    private void Start()
    {
        
        AllPutCell.Add(this);
    }
    
    private void OnDisable()
    {
        AllPutCell.Clear();
    }

    public virtual void Inicialize(int value, int count)
    {
        _filling = fillingUser.Instance;
        Value = value;
        _count = count;
        LogValue();
    }

    public static void STATICMETHOD(int id)
    {
        var putCell = AllPutCell.Where(p=>p.Value == id).FirstOrDefault();
        if (putCell == null)
            return;
        putCell.Decrease();
    }

    public void Decrease()
    {
        _count--;
        if(_count == 0)
        {
            _elements.SetActive(false);
        }
        _countText.text = _count.ToString();
    }
    
    public void OnMouseDown()
    {
        if (_filling.GameActive)
        {
            _filling.PutValue(Value);
        }
    }

    private void LogValue()
    {
        _countText.text = _count.ToString();
        _valueText.text = Value.ToString();
    }
}
