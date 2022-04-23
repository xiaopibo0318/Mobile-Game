using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float myspeed;
    public static Player Instance;
    
    Animator myAnime;
    public Rigidbody2D myRigid;
    Camera viewCamera;
    Vector3 velocity;
    InputAction playerMove;
    float cache_time;

    public void Awake()
    {
        playerMove = GetComponent<PlayerInput>().currentActionMap["Move"];
        myAnime = GetComponent<Animator>();
        myRigid = GetComponent<Rigidbody2D>();
        //this.enabled = false;
        Instance = this;
        cache_time = 2;
    }
    
    public void FixedUpdate()
    {
        float a = playerMove.ReadValue<Vector2>().x;
        float b = playerMove.ReadValue<Vector2>().y;
        //float a = Input.GetAxisRaw("Horizontal");
        //float b = Input.GetAxisRaw("Vertical");

        velocity = new Vector3(a, 0, b);


        if (a < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (a > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (Mathf.Abs(a) >= 0.1f && b == 0)
        {
            myAnime.SetFloat("Run", Mathf.Abs(a));
        }
        else if (Mathf.Abs(b) >= 0.1f && a == 0)
        {
            myAnime.SetFloat("Run", Mathf.Abs(b));
        }
        else if (Mathf.Abs(a) >= 0.1f || Mathf.Abs(b) >= 0.1f)
        {
            myAnime.SetFloat("Run", Mathf.Abs(a));
        }
        else
        {
            myAnime.SetFloat("Run", 0);
        }




        float change_x = myRigid.position.x + a * myspeed * Time.fixedDeltaTime;
        float change_y = myRigid.position.y + b * myspeed * Time.fixedDeltaTime;
        myRigid.position = new Vector2(change_x, change_y);
    }

    private void Start()
    {
        
    }

    public void PlayerCache()
    {
        myAnime.SetBool("Cache", true);
        cache_time -= Time.deltaTime;
        if (cache_time <= 0)
            myAnime.SetBool("Cache", false);
            cache_time = 2;
    }

}
