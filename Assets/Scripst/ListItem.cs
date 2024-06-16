using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ListItem : MonoBehaviour
{
    [SerializeField] Text _textName;
    [SerializeField] Text _textPlayerCount;
    public void SetInfo(RoomInfo info)
    {
        _textName.text = info.Name;
        _textPlayerCount.text = info.PlayerCount + "/" + info.MaxPlayers;
    }

    public void JoinToListRoom()
    {
        PhotonNetwork.JoinRoom(_textName.text);
    }
}
