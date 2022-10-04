using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapKLtp : MonoBehaviour
{
    int nowPos = 1;


    public static MapKLtp Instance;
    public void Awake()
    {
        Debug.Log("cc");
        Debug.Log(nowPos);
        Instance = this;
    }

    [System.Obsolete]
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
        Debug.Log("即將傳送");
        
        nowPos = Camerafollowww.Instance.GetMyPos();
        Debug.Log(nowPos);
        if (nowPos == 1) 
        {
            ParticleManager.Instance.addSpeedTeleport(0);
        }
        else
        {
            ParticleManager.Instance.addSpeedTeleport(1);
        }
        //StartCoroutine(teleport());


        //Camerafollowww.Instance.changeMyPos();



    }

    [System.Obsolete]
    private void OnTriggerExit2D(Collider2D collision)
    {
        nowPos = Camerafollowww.Instance.GetMyPos();
        Debug.Log(nowPos);
        if(nowPos == 1)
        {
            ParticleManager.Instance.StopTeleport(0);
        }
        else
        {
            ParticleManager.Instance.StopTeleport(1);
        }
        
        Debug.Log("取消傳送");
    }



    public int GetNowPos()
    {
        return nowPos;
    }

    IEnumerator teleport()
    {
        yield return new WaitForSeconds(5);
        Camerafollowww.Instance.changeMyPos();
        //Debug.Log(nowPos);
        yield return null;

    }

}
