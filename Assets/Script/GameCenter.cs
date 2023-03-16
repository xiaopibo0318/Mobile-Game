using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameCenter : MonoBehaviour
{
    public static GameCenter Instance;

    public int tempIndex { get; set; }
    public string tempText
    {
        get; set;
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
        tempIndex = -99;
        tempText = "";
    }

    private void OnEnable()
    {
        SceneManager.LoadScene(1);
        ResetAll();

    }



    public void EndGame()
    {
        SceneManager.LoadScene(5);

    }


    public void QuitGame()
    {
        Application.Quit();
    }

    private void ResetAll()
    {
        TimeCounter.Instance.CloseClockObject();
    }


    public void UpdateInfo()
    {
    }

    public void ChangeRoomState(int _roomIndex, string time)
    {
        tempIndex = _roomIndex;
        tempText = time;
    }

}
