using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class TitleManager : Singleton<TitleManager>
{
    [Header("分房列表")]
    [SerializeField] List<Button> roomListButton;
    [SerializeField] Button startGameButton;
    [SerializeField] GameObject intoRoom;
    [SerializeField] Text confirmText;

    [Header("標題介面")]
    [SerializeField] Button settingBegin;
    [SerializeField] Button openIntroduceButton;

    [Header("影片管理")]
    [SerializeField] VideoPlayer videoPlayer;
    private static string storyVideoPath = Application.streamingAssetsPath + "/Video/Westpath Initial.mp4";

    [Header("介紹列表")]
    [SerializeField] private Button storyButton;
    [SerializeField] private Button introduceButton;

    [Header("返回按鈕")]
    [SerializeField] private Button GoBackIntroduceButton;


    private void Start()
    {
        Init();
        IntroduceReset();
    }

    private void PlayStoryVideo()
    {
        videoPlayer.Play();
    }
    private void VideoEndEvent(VideoPlayer vp)
    {
        videoPlayer.targetTexture.Release();
    }

    private void OpenIntrodce()
    {

    }


    private void IntroduceReset()
    {
        storyButton.onClick.AddListener(PlayStoryVideo);
        introduceButton.onClick.AddListener(OpenIntrodce);
        GoBackIntroduceButton.onClick.AddListener(() => MenuManager.Instance.OpenMenu("title"));
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.targetTexture.Release();
        videoPlayer.url = storyVideoPath;
        videoPlayer.Prepare();
        videoPlayer.loopPointReached += VideoEndEvent;
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
        openIntroduceButton.onClick.AddListener(() => MenuManager.Instance.OpenMenu("introduce"));

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
