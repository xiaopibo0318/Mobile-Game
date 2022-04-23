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
            return;



    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
