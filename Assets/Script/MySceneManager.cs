using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : Singleton<MySceneManager>
{

    [SerializeField] Transform myPlayer;

    public int GetNowSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void OnLoadNewScene()
    {
        int nowSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (nowSceneIndex == 0 || nowSceneIndex == 5)
        {
            myPlayer.position = new Vector3(0, 0, -20);
        }
        else myPlayer.position = new Vector3(0, 0, 0);

    }
}
