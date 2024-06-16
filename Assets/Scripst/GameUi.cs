using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUi : MonoBehaviourPunCallbacks
{
    public static GameUi Instance {  get; private set; }
    protected fillingUser _filling;
    [SerializeField] private GameObject _windowGameOver;
    [SerializeField] private GameObject _windowWin;
    [SerializeField] private GameObject _windowPause;
    [SerializeField] private Animator _animator;
    [SerializeField] private Text _finishTime;
    [SerializeField] private Text _countError;
    private Timer _timer;

    public virtual void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _timer = Timer.Instance;
        _filling = fillingUser.Instance;
    }

    public void UpdateErrorText(int countError)
    {
        _countError.text = $"Ошибки {countError}/3";
    }

    public void GameOver()
    {
        _timer.StopTimer();
        _windowGameOver.SetActive(true);
    }

    public virtual void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public virtual void StartGame()
    {
        _timer?.StartTImer();
    }

    public void ContinueGame()
    {
        _windowGameOver.SetActive(false);
        _filling.ResetGame();
    }

    public virtual void Pause(bool isActive)
    {
        _windowPause.SetActive(isActive);
    }

    public void Win()
    {
        _windowWin.SetActive(true);
        _timer.OutputTimeInText(_finishTime);
    }

    public virtual void ExitMenu()
    {
        SceneManager.LoadScene(0);
    }

}
