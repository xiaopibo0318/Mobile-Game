using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject startbutton;
    [SerializeField] GameObject waitingcontent;
    [SerializeField] string[] roomName;
    int playerCount;
    public static RoomManager Instance;

    public RoomInfo info;
    
    public void Awake()
    {
        /*
        roomName = new string[5];
        roomName[0] = "MapKL";
        roomName[1] = "MapLZ";
        roomName[2] = "MapHC";
        roomName[3] = "MapGJ";
        roomName[4] = "MapGD";
        */
        Instance = this;
    }

    public void SetUp(RoomInfo _info)
    {
        info = _info;
        /*
        for(int i=0; i < 4; i++)
        {
            roomName[i] = _info.Name;
            PhotonNetwork.CreateRoom(roomName[i]);
        }
        */
    }

    public void OnclickKL()
    {
        //PhotonNetwork.JoinRoom(roomName[0]);
        //PhotonNetwork.JoinRoom("333");
        Debug.Log("Entering Mt. KT success");
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        waitingcontent.SetActive(true);
    }

    public void CreateKLroom()
    {
        Debug.Log("Entering Mt. KT success");
        /*
        if (PhotonNetwork.IsConnected)
        {
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinRandomOrCreateRoom(null);
        }
        */
        //Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        waitingcontent.SetActive(true);
        startbutton.SetActive(true);
    }

    void UpdatePlayerCount(bool AddTocount)
    {
        if (AddTocount)
        {
            playerCount += 1;
        }
        else
        {
            playerCount -= 1;
        }
    }

}
