using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camerafollowww : Singleton<Camerafollowww>
{

    private Transform target;
    public float smoothing;

    public int myPos;

    private int nowSceneIndex;


    // Start is called before the first frame update
    void Start()
    {
        target = Player.Instance.transform;

        nowSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(nowSceneIndex + "AAAA");
        myPos = 1;
    }




    private void LateUpdate()
    {
        switch (nowSceneIndex)
        {
            case 2:
                CameraInMTKL();
                break;
            case 3:
                CameraInGD();
                break;

        }

    }


    private void CameraInMTKL()
    {
        if (target.position.x > -25)
        {
            if (target.position.x >= -9.367892 && target.position.x <= 9.01498)
            {
                if (target.position.y >= -5.801526 && target.position.y <= 6.294383)
                {
                    if (transform.position != target.position)
                    {
                        Vector3 targetPos = target.position;
                        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
                    }
                }
            }
            else if (target.position.x >= -9.367892 && target.position.x <= 9.01498)
            {
                transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
            }
            else if (target.position.y >= -5.801526 && target.position.y <= 6.294383)
            {
                transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
            }
            else
            {
                return;
            }
        }
        else if (target.position.x < 35)// x -113,9.5,-9.5,-
        {

            if (target.position.y < 7.5f && target.position.y > -7.5f)
            {
                transform.position = new Vector3(-50, target.position.y, target.position.z);
            }

        }
        else
        {
            Debug.Log("找不到人");
        }
    }

    /// <summary>
    /// x, -13, 13
    /// y. -9.5, 9.5
    /// </summary>
    private void CameraInGD()
    {
        if (target.position.x > -25)
        {
            if (target.position.x >= -13 && target.position.x <= 15)
            {
                if (target.position.y >= -9.5 || target.position.y <= 9.5)
                {
                    if (transform.position != target.position)
                    {
                        Vector3 targetPos = target.position;
                        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
                    }
                }
            }
            else if (target.position.x >= -13 && target.position.x <= 15)
            {
                transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
            }
            else if (target.position.y >= -9.5 || target.position.y <= 9.5)
            {
                transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
            }
            else
            {
                return;
            }
        }
        else if (target.position.x < -35)
        {
            Vector3 targetPos = target.position;
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);

            if (target.position.x > -113 && target.position.x < -87)
            {
                if (target.position.y >= -9.5 && target.position.y <= 9.5)
                {
                    if (transform.position != target.position)
                    {
                        targetPos = target.position;
                        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
                    }
                }

            }
            else if (target.position.x > -113 && target.position.x < -87)
            {
                transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
            }
            else if (target.position.y >= -9.5 && target.position.y <= 9.5)
            {
                transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
            }
            else
            {
                return;
            }

        }
        else
        {
            Debug.Log("找不到人");
        }
    }
    public int GetMyPos()
    {
        return myPos;
    }
    public void changeMyPos()
    {
        if (myPos == 1)
        {
            myPos = 2;
            Player.Instance.myRigid.position = new Vector2(-50, 0);
        }
        else if (myPos == 2)
        {
            myPos = 1;
            Player.Instance.myRigid.position = new Vector2(0, 0);
        }

    }

    public void UpdateNowSceneBuildIndex()
    {
        nowSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }


}
