using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _roomName;
    [SerializeField] private ListItem _itemPrefab;
    [SerializeField] private Transform _content;
    [SerializeField] private Button _createRoomButton;
    [SerializeField] private TextMeshProUGUI _textMeshPro;


    private Dictionary<string, ListItem> _roomItems = new Dictionary<string, ListItem>();
    private bool _isReconnecting = false;

    private void Start()
    {
        ConnectToPhoton();
    }

    private void ConnectToPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Connection();
        }

        if (PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
        {
            StartCoroutine(Disconection());
        }
    }

    public IEnumerator Disconection()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }
        Connection();
    }

    private void Connection()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        SetCreateRoomButtonState(false);
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        _textMeshPro.text = "Регион: " + PhotonNetwork.CloudRegion;
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SetCreateRoomButtonState(true);
        _isReconnecting = false;
    }

    public void CreateRoomButton()
    {
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            Debug.LogError("PhotonNetwork is not connected and ready.");
            return;
        }

        if (string.IsNullOrEmpty(_roomName.text))
        {
            _roomName.text = "Новая комната";
        }

        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 2
        };
        PhotonNetwork.CreateRoom(_roomName.text, roomOptions, TypedLobby.Default);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("CreateRoom failed: " + message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        List<string> roomsToRemove = new List<string>();
        foreach (var roomItem in _roomItems)
        {
            bool roomStillExists = roomList.Exists(r => r.Name == roomItem.Key);
            if (!roomStillExists)
            {
                roomsToRemove.Add(roomItem.Key);
            }
        }
        foreach (var roomName in roomsToRemove)
        {
            if (_roomItems.ContainsKey(roomName))
            {
                Destroy(_roomItems[roomName].gameObject);
                _roomItems.Remove(roomName);
            }
        }

        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                if (_roomItems.ContainsKey(info.Name))
                {
                    Destroy(_roomItems[info.Name].gameObject);
                    _roomItems.Remove(info.Name);
                }
            }
            else
            {
                if (_roomItems.ContainsKey(info.Name))
                {
                    _roomItems[info.Name].SetInfo(info);
                }
                else
                {
                    ListItem listItem = Instantiate(_itemPrefab, _content);
                    listItem.SetInfo(info);
                    _roomItems[info.Name] = listItem;
                }
            }
        }
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
            PhotonNetwork.LoadLevel(4);
        }
        else
        {
            Debug.LogError("CurrentRoom is null in OnJoinedRoom");
        }
    }

    private void SetCreateRoomButtonState(bool state)
    {
        if (_createRoomButton != null)
        {
            _createRoomButton.interactable = state;
        }
    }

    public void LeaveButton()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
            Debug.Log("Leaving Lobby...");
        }
        else
        {
            Debug.LogWarning("Not in lobby. Returning to menu...");
            PhotonNetwork.LoadLevel("Menu");
        }
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Left Lobby");
        PhotonNetwork.LoadLevel("Menu");
    }
}