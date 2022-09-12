using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameCenter : MonoBehaviour
{
    public static GameCenter Instance;

    [SerializeField] Button startGameButton;

    void Awake()
    {
        Instance = this;
        startGameButton.onClick.AddListener(StartMTKL);
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
        //postMethod.Instance.Settlement();
    }


    
}
