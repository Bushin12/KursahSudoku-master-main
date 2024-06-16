using UnityEngine;

public class Cell : MonoBehaviour
{
    public enum stateCell{Choose, NotChoose, Error, Values}
    public bool IsHade { private set; get; }
    public int X { private set; get; }
    public int Y { private set; get; }
    public int Value;
    [SerializeField] private TextMesh _valueText;
    [SerializeField] private Color _colorChooise, _colorError, _colorValues;
    [SerializeField] private SpriteRenderer _backGround;
    [SerializeField] private AudioSource _filingCell, _error;
    private bool _isError = false;
    public Cell[] _cellsThisValues { private set; get; }
    private fillingUser _filling;


    public void SetCellsValue(Cell[] newValues)
    {
        _cellsThisValues = newValues;
    }
    private void Start()
    {
        _filling = fillingUser.Instance;
    }

    public virtual void SetValue(int value, int x, int y)
    {
        X = x;
        Y = y;
        Value = value;
        LogValue();
    }

    public void Hide()
    {
        IsHade = true;
        _valueText.gameObject.SetActive(false);
    }

    public void Active()
    {
        IsHade = false;
        _valueText.gameObject.SetActive(true);
        _valueText.text = Value.ToString();
        _isError = false;
        _filingCell.Play();
        ChangeBackGround(stateCell.Choose);
    }

    public void WrongNumber(int value)
    {
        _valueText.gameObject.SetActive(true);
        _valueText.text = value.ToString();
        _isError = true;
        ChangeBackGround(stateCell.Error);
    }

    public void ChangeBackGround(stateCell state)
    {
        if (state == stateCell.Choose && !_isError)
        {
            _backGround.color = _colorChooise;
        }
        else if (state == stateCell.NotChoose && !_isError)
            _backGround.color = Color.clear;
        else if (state == stateCell.Values && IsHade == false && !_isError)
        {
            _backGround.color = _colorValues;
            _valueText.gameObject.SetActive(true);
        }
        else if(state == stateCell.Error)
        {
            _backGround.color = _colorError;
            _error.Play();
        }
    }

    public void OnMouseDown()
    {
        if (_filling.GameActive)
        {
            _filling.ChooseCell(this);
        }
    }

    private void LogValue()
    {
        _valueText.text = Value.ToString();
    }
}
