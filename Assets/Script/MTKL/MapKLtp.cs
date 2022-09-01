using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapKLtp : MonoBehaviour
{
    int nowPos = 1;


    public static MapKLtp Instance;
    public void wake()
    {
        Instance = this;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("AAA");
        //Debug.Log(nowPos);
        //if (nowPos == 1)
        //{
        //    Debug.Log("BBA");
        //    Player.Instance.transform.position = new Vector3(-50, 0, 0);
        //    nowPos = 2;
        //}
        //else if (nowPos == 2)
        //{
        //    Debug.Log("CCA");
        //    Player.Instance.transform.position = new Vector3(0, 0, 0);
        //    nowPos = 1;

        //}
        Camerafollowww.Instance.changeMyPos();
        ParticleManager.Instance.DisplayTeleport();
    }

    
    public int GetNowPos()
    {
        return nowPos;
    }

}
