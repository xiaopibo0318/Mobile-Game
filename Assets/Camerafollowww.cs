using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollowww : MonoBehaviour
{
    public Transform target;
    public float smoothing;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        //防止玩家消失時鏡頭跑掉
        if (target != null)
        {
            if(transform.position != target.position)
            {
                Vector3 targetPos = target.position;
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
