using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;


public class GameCenter : MonoBehaviourPunCallbacks
{
    public static GameCenter Instance;

    [SerializeField] GameObject startGameButton;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// 掛按鈕上
    /// </summary>
    public void StartMTKL()
    {
        SceneManager.LoadScene(1);
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
        SceneManager.LoadScene(3);

    }


    
}
