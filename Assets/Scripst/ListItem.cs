using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class ListItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textName;
    [SerializeField] TextMeshProUGUI _textPlayerCount;
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
