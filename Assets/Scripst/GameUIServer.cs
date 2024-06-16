using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIServer : GameUi
{
    [SerializeField] private GameObject _otherPlayerWindow;

    public override void Awake()
    {
        _otherPlayerWindow.SetActive(true);
        base.Awake();
    }

    public override void StartGame()
    {
        _otherPlayerWindow.SetActive(false);
        base.StartGame();
    }

    public override void Restart()
    {
        var localPlayer = Miltiplayer.Instance;
        localPlayer.GetPhotonView().RPC(nameof(localPlayer.RemoteLoading), RpcTarget.All, SceneManager.GetActiveScene().buildIndex);
    }

    public override void ExitMenu()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        base.ExitMenu();
    }

}