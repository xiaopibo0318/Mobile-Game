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
        myPos = 1;
        Instance = this;
            
    }

    // Start is called before the first frame update
    void Start()
    {
        target = Player.Instance.transform;
        
        
    }
   

    private void LateUpdate()
    {
        if( target.position.x > -25)
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
        }else  if (target.position.x < 35)
        {
     
            if (target.position.y <7.5f && target.position.y > -7.5f)
            {
                transform.position = new Vector3(-50, target.position.y, target.position.z);
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
        if(myPos == 1)
        {
            myPos = 2;
            Player.Instance.myRigid.position = new Vector2(-50,0);
        }
        else if (myPos == 2)
        {
            myPos = 1;
            Player.Instance.myRigid.position = new Vector2(0,0); 
        }
                
    }


    
}
