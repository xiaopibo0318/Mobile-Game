using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TitleManager : MonoBehaviour
{
    public static TitleManager Instance;

    [SerializeField] List<Button> roomListButton;

    [SerializeField] Button startGameButton;
    [SerializeField] GameObject intoRoom;

    [SerializeField] Text confirmText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);

        Init();
    }

    private void Init()
    {
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
        TransiitionManager.Instance.GoToMTKL();
        
    }

    private void StartGD()
    {
        TransiitionManager.Instance.GoToGD();
    }

    public void EnteringRoomList()
    {
        MenuManager.Instance.OpenMenu("roomlist");
    }

    /// <summary>
    /// 結算畫面
    /// </summary>
    public void EndGame()
    {
        SceneManager.LoadScene(5);
        //postMethod.Instance.Settlement();
    }



}
