using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class TitleManager : MonoBehaviour
{
    public static TitleManager Instance;

    [Header("分房列表")]
    [SerializeField] List<Button> roomListButton;
    [SerializeField] Button startGameButton;
    [SerializeField] GameObject intoRoom;
    [SerializeField] Text confirmText;

    [Header("標題介面")]
    [SerializeField] Button settingBegin;

    [Header("影片管理")]
    [SerializeField] VideoPlayer videoPlayer;
    private static string storyVideoPath = Application.streamingAssetsPath + "Video/Westpath Initial";
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);

        Init();
    }

    private void Start()
    {
        //videoPlayer.gameObject.SetActive(true);
        //videoPlayer.url = storyVideoPath;

    }

    private void Init()
    {
        settingBegin.onClick.AddListener(() => SettingManager.Instance.OpenSetting());

        for (int i = 0; i < roomListButton.Count; i++)
        {
            var nowIndex = i;
            roomListButton[i].onClick.AddListener(delegate { SelectDifferentRoom(nowIndex); });
        }
        intoRoom.SetActive(false);
        startGameButton.gameObject.SetActive(false);
    }


    // 0 => 崑崙山, 1 => 雷澤, 2 => 宮殿
    private void SelectDifferentRoom(int nowRoom)
    {
        intoRoom.SetActive(true);
        startGameButton.gameObject.SetActive(true);
        startGameButton.onClick.RemoveAllListeners();
        switch (nowRoom)
        {
            case 0:
                confirmText.text = "確定要進入\n 「崑崙山」 嗎";
                startGameButton.onClick.AddListener(StartMTKL);
                break;
            case 1:
                confirmText.text = "確定要進入\n 「雷澤」 嗎";
                break;
            case 2:
                confirmText.text = "確定要進入\n 「宮殿」 嗎";
                startGameButton.onClick.AddListener(StartGD);
                break;
        }
    }


    private void StartMTKL()
    {
        AudioManager.Instance.clickButton("c");
        TransiitionManager.Instance.GoToMTKL();
        
    }

    private void StartGD()
    {
        AudioManager.Instance.clickButton("c");
        TransiitionManager.Instance.GoToGD();
       
    }

    public void EnteringRoomList()
    {
        MenuManager.Instance.OpenMenu("roomlist");
    }



}
