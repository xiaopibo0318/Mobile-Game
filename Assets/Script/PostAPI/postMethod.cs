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
    public int lastMin;
    public int lastSec;
    private int bookClick;
    public int totalSeconds { get; set; }

    public static PostMethod Instance;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);


        MySceneManager.Instance.OnLoadNewScene();

    }



    private void OnEnable()
    {
        //totalSeconds = 1799;
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
        bookClick = Player.Instance.myStatus.bookClick;
        totalSeconds = 4500 - (lastMin * 60 + lastSec);
        postData();
    }



    IEnumerator PostData_Coroutine()
    {
        string url = "http://140.122.91.204:3000/api/woodWorking/";
        WWWForm form = new WWWForm();
        form.AddField("username", playerName);
        form.AddField("email", playerEmail);
        form.AddField("second", totalSeconds);
        form.AddField("clickCount", bookClick);

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError)
            {
                Debug.Log(1);
                //outPutArea.text = request.error;
            }
            else if (request.isHttpError)
            {
                Debug.Log(2);
                //outPutArea.text = request.error;
            }
            else
            {
                //outPutArea.text = request.downloadHandler.text;
            }
            Debug.Log(request.error);
            Debug.Log(request.downloadHandler);
        }

    }

}
