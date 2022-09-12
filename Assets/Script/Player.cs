using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour , ISaveable
{
    public float myspeed;
    public static Player Instance;
    
    Animator myAnime;
    public Rigidbody2D myRigid;
    Camera viewCamera;
    Vector3 velocity;
    InputAction playerMove;
    float cache_time;

    [SerializeField] GameObject[] PlayerStyle;

    public playerStatus myStatus;
    


    public void Awake()
    {
        playerMove = GetComponent<PlayerInput>().currentActionMap["Move"];
        //這邊先確保
        myAnime = PlayerStyle[1].GetComponent<Animator>();
        myRigid = GetComponent<Rigidbody2D>();
        //this.enabled = false;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            Destroy(gameObject);
        }
        cache_time = 2;
        myStatus = new playerStatus(SceneManager.GetActiveScene().buildIndex);
    }


    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log(myStatus.GetGameStatus());
        }
    }

    bool isWalk = false;

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
            if (!isWalk)
            {
                AudioManager.Instance.Walk(true);
                isWalk = true;
            }
        }
        else if (a > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            if (!isWalk)
            {
                AudioManager.Instance.Walk(true);
                isWalk = true;
            }
        }
        else
        {
            AudioManager.Instance.Walk(false);
            isWalk = false;
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



    public void PlayerCache()
    {
        StartCoroutine(playerCache());
 
    }

    IEnumerator playerCache()
    {
        myAnime.SetBool("Cache", true);
        while (cache_time >= 0)
        {
            yield return new WaitForSeconds(1);
            cache_time -= 1;
        }
        myAnime.SetBool("Cache", false);
        cache_time = 2;
    }

    //存檔系統
    public object SaveState()
    {
        return new SaveData()
        {
            gameStatus = myStatus.gameStatus
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        myStatus.gameStatus = saveData.gameStatus;
    }




    [Serializable]
    private struct SaveData
    {
        public int gameStatus;
    }


}


public class playerStatus 
{
    public string emailAddress;
    public string name;
    public int gameStatus;
    public int totalTime;
    

    public int timeMin;
    public int timeSec;

    public playerStatus(int gameStatus)
    {
        this.gameStatus = gameStatus;
    }

    public void ChangeGameStatus(int index)
    {
        gameStatus = index;
    }

    public int GetGameStatus()
    {
        return gameStatus;
    }


    /// <summary>
    /// 從計時器那邊Copy數據過來。
    /// </summary>
    public void UpdateNowTime()
    {
        timeMin = TimeCounter.Instance.GetNowTimeMin();
        timeSec = TimeCounter.Instance.GetNowTimeSec();
    }



}