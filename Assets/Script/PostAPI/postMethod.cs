using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PostMethod : MonoBehaviour
{

    InputField outPutArea;

    [Header("數據儲存")]
    private string playerName;
    private string playerEmail;
    private int lastMin;
    private int lastSec;
    private int totalSeconds;

    public static PostMethod Instance;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);


        outPutArea = GameObject.Find("OutPutArea").GetComponent<InputField>();
        outPutArea.text = "";
        GameObject.Find("PostButton").GetComponent<Button>().onClick.AddListener(postData);
        

        MySceneManager.Instance.OnLoadNewScene();

    }



    private void OnEnable()
    {
        Settlement();
    }

    void postData() => StartCoroutine(PostData_Coroutine());


    public void Settlement()
    {
        Player.Instance.myStatus.UpdateNowTime();
        lastMin = Player.Instance.myStatus.timeMin;
        lastSec = Player.Instance.myStatus.timeSec;
        playerName = Player.Instance.myStatus.name;
        playerEmail = Player.Instance.myStatus.emailAddress;
        totalSeconds = 4500 - (lastMin * 60 + lastSec);
        outPutArea.text = "剩下時間：" + lastMin.ToString() + ":" + lastSec.ToString();
        outPutArea.text += "總花秒數" + totalSeconds.ToString();
    }


    IEnumerator PostData_Coroutine()
    {
        string url = "http://140.122.91.142:3000/";
        WWWForm form = new WWWForm();
        form.AddField("username", playerName);
        form.AddField("email", playerEmail);
        form.AddField("second", totalSeconds);
        
        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError)
            {
                Debug.Log(1);
                outPutArea.text = request.error;
            }
            else if (request.isHttpError)
            {
                Debug.Log(2);
                outPutArea.text = request.error;
            }
            else
            {
                outPutArea.text = request.downloadHandler.text;
            }
            Debug.Log(request.error);
            Debug.Log(request.downloadHandler);
        }

    }

}
