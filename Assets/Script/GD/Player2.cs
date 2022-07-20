using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{

    public Rigidbody2D myRigid;
    public float mySpeed;
    [SerializeField] GameObject mySelf;

    float a; //左右移動
    float b; //上下移動

    public Collider2D myCollider;



    public void Awake()
    {
        myRigid = mySelf.GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        Move();

    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            a = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            a = 1;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            b = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            b = -1;
        }


        if (a < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (a > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }





        float change_x = myRigid.position.x + a * mySpeed * Time.deltaTime;
        float change_y = myRigid.position.y + b * mySpeed * Time.deltaTime;

        //移動位置
        myRigid.position = new Vector2(change_x, change_y);
        a = 0; b = 0;


    }
}
