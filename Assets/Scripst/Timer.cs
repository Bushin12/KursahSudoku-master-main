using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour
{
    public static Timer Instance { private set; get; }
    [SerializeField] private Text _textTime;
    [SerializeField] private bool _startField;
    private float _currentTime;
    private float _startTime;

    private void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        if (_startField)
            StartTImer();
    }

    public void StartTImer()
    {
        _startTime = Time.time;
        StartCoroutine(CounterTime());

    }
    public void PauceTimer()
    {

    }
    public float CurrentTime()
    {
        return _currentTime;
    }
    public void OutputTimeInText(Text textOutput)
    {
        string minutes = ((int)_currentTime / 60).ToString();
        string seconds = (_currentTime % 60).ToString("00");
        textOutput.text = minutes + ":" + seconds;
    }
    private IEnumerator CounterTime()
    {
        while (true)
        {
            _currentTime = Time.time - _startTime;

            string minutes = ((int)_currentTime / 60).ToString();
            string seconds = (_currentTime % 60).ToString("00");

            _textTime.text = minutes + ":" + seconds;
            yield return new WaitForEndOfFrame();
        }
    }
    public void StopTimer()
    {
        StopAllCoroutines();
    }
}
