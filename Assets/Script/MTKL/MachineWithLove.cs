using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MachineWithLove : MonoBehaviour
{
    bool isElectric;

    float offset_x;
    float offset_y;
    float radius;
    float pos_x;
    float pos_y;

    GameObject love;

    public static MachineWithLove Instance;

    private void Awake()
    {
        isElectric = false;
        Instance = this;
        offset_x = gameObject.GetComponent<CircleCollider2D>().offset.x;
        offset_y = gameObject.GetComponent<CircleCollider2D>().offset.y;
        radius = gameObject.GetComponent<CircleCollider2D>().radius;
        pos_x = gameObject.GetComponent<Transform>().position.x;
        pos_y = gameObject.GetComponent<Transform>().position.y;

    }

    private void FixedUpdate()
    {
        love = GameObject.FindWithTag("love");
        if (love != null)
        {
            float a = love.transform.position.x;
            float b = love.transform.position.y;
            if (Mathf.Abs(a - pos_x) + Mathf.Abs(b - pos_y) < radius / 10)
            {
                isElectric = true;
            }
            else isElectric = false;
            //Debug.Log("目前為" + Mathf.Abs(a - pos_x) + Mathf.Abs(b - pos_y));
            //Debug.Log("半徑" + radius);
        }
        else
        {
            isElectric = false;
            //Debug.Log("場上沒有Love");
        }

        //Debug.Log("電力狀態：" + isElectric);
    }



    private void Update()
    {
        //Debug.Log(isElectric);
    }

    public bool GetIsElectric()
    {
        return isElectric;
    }

}
