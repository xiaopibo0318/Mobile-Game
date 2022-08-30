using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;


public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    [SerializeField] Transform roomlistContent;
    [SerializeField] GameObject roomlistitemPrefab;
    [SerializeField] Transform playerlistContent;
    [SerializeField] GameObject playserlistitemPrefab;
    [SerializeField] GameObject startGameButton;

    void Awake()
    {
        Instance = this;
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

    


    
}
