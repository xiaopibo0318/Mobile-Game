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
        Debug.Log("cc");
        Debug.Log(nowPos);
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
        Debug.Log("即將傳送");
        Debug.Log(nowPos);
        nowPos = Camerafollowww.Instance.GetMyPos();
        if (nowPos == 1)
        {
            ParticleManager.Instance.addSpeedTeleport(0);
        }
        else
        {
            ParticleManager.Instance.addSpeedTeleport(1);
        }
        StartCoroutine(teleport());


        //Camerafollowww.Instance.changeMyPos();



    }

    public void onTriggerExit2D(Collider2D collision)
    {
        StopCoroutine(teleport());
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
