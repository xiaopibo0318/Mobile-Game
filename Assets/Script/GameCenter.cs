using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCenter : MonoBehaviour
{
    public static GameCenter Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }


    

    public void EndGame()
    {
        SceneManager.LoadScene(5);
        //postMethod.Instance.Settlement();
    }


    public void QuitGame()
    {
        Application.Quit();
    }


}
