using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    [SerializeField] TMP_Text errorText;
    [SerializeField] Transform roomlistContent;
    [SerializeField] GameObject roomlistitemPrefab;
    [SerializeField] Transform playerlistContent;
    [SerializeField] GameObject playserlistitemPrefab;
    [SerializeField] GameObject startGameButton;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connecting to Master Server");
        //連線到Photon的Master Server ...PhotonServerSetting那一塊
        PhotonNetwork.ConnectUsingSettings();
    }

    //當我們成功連到Server會藉由Photon來進行回傳
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected Master Server");
        //我們必須得在Lobby進行創建房間或進入放間
        PhotonNetwork.JoinLobby();
        //讓所有人都相同場景
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("Joinned Lobby");
        //PhotonNetwork.NickName = "Player" + Random.Range(0, 1000).ToString("0000");
    }

    
    public void CreateRoom()
    {
        /*
        //判斷是否有這個房間
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        */
        //PhotonNetwork.CreateRoom("333");
        //MenuManager.Instance.OpenMenu("loading");
        //RoomManager.Instance.CreateKLroom();
    }
    

    /*
    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        //從Photon伺服器那邊讀取房間名稱
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        //這個是要把Playerlist上已經退出的地方砍掉，即使他Leave Room了，還會存在在Playerlist上的問題。

        foreach (Transform child in playerlistContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(playserlistitemPrefab, playerlistContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        //讓只有創立房間的人看的到
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    *


    /*
    //如果換人當Host的時候
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    */

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Createion failed:" + message;
        Debug.LogError("Room Creation Failed:" + message);
        MenuManager.Instance.OpenMenu("error");
    }

    public void StartGame()
    {
        //這個數字1是指初始的Game Sceane，我們在Build Setting那邊設置的。
        //這個會讓所有人都到Level1
        PhotonNetwork.LoadLevel(1);
    }

    public void EnteringRoomList()
    {
        MenuManager.Instance.OpenMenu("roomlist");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }
    public void UserLogin()
    {
        MenuManager.Instance.OpenMenu("loginsystem");
        Debug.Log("Login");
    }
    public void CreateCharacter()
    {

        MenuManager.Instance.OpenMenu("createcharacter");
        Debug.Log("Entering Characre_create Menu");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");


    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");
    }

    /*
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomlistContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomlistitemPrefab, roomlistContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }
    */

    /*
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //生成playerlist當新的玩家加入
        Instantiate(playserlistitemPrefab, playerlistContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
    */
}
