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

    private void OnEnable()
    {
        //SceneManager.LoadScene(1);
        
    }



    public void EndGame()
    {
        SceneManager.LoadScene(5);

    }


    public void QuitGame()
    {
        Application.Quit();
    }




}
