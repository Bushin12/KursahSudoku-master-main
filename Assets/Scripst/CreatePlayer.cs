using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class CreatePlayer : MonoBehaviourPunCallbacks
{
    [SerializeField] private Miltiplayer _playerPrefab;
    [SerializeField] private SpriteRenderer _test;
    private Miltiplayer _thisPlayer;

    private void Awake()
    {
       _thisPlayer = PhotonNetwork.Instantiate(_playerPrefab.gameObject.name, Vector3.zero, Quaternion.identity).GetComponent<Miltiplayer>();
        if(PhotonNetwork.IsMasterClient)
            _test.color = Color.red;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _thisPlayer.InitClient();
            _thisPlayer.GetPhotonView().RPC(nameof(_thisPlayer.StartGame), RpcTarget.All);

        }
    }
}
