using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camerafollowww : MonoBehaviour
{
   
    Transform target;
    public float smoothing;

    public int myPos;

    public static Camerafollowww Instance;

    private void Awake()
    {
        Instance = this;
        myPos = 1;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        target = Player.Instance.transform;
        
        
    }
   

    private void LateUpdate()
    {
        if( SceneManager.GetActiveScene().buildIndex == 1)
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
        }else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (target.position.y >= -7.5 && target.position.y <= 7.5)
            {
                transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
            }

        }
        else
        {
            Debug.Log("找不到人");
        }
        
    }

    public void changeMyPos()
    {
        if(myPos == 1)
        {
            myPos = 2;
            Player.Instance.myRigid.position = new Vector2(-49.05f,2.39f);
        }else if (myPos == 2)
        {
            myPos = 1;
            Player.Instance.myRigid.position = new Vector2(0,0);
        }
    }


    
}
