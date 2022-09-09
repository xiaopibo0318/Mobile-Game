using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class postMethod : MonoBehaviour
{

    InputField outPutArea;

    private void Awake()
    {
        outPutArea = GameObject.Find("OutPutArea").GetComponent<InputField>();
        outPutArea.text = "";
        GameObject.Find("PostButton").GetComponent<Button>().onClick.AddListener(postData);
        
    }


    void postData()=> StartCoroutine(PostData_Coroutine());


    IEnumerator PostData_Coroutine()
    {
       // List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
        string url = "http://140.122.91.142:3000/xxx@emai";
        //wwwForm.Add(new MultipartFormDataSection("name", "xiaopibo"));
        WWWForm form = new WWWForm();
        form.AddField("Name", "xiaopibo");
        form.AddField("Time", 50);
        form.AddField("Email", "xxxxx@gmail.com");
        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError)
            {
                Debug.Log(1);
                outPutArea.text = request.error;
            }else if (request.isHttpError)
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
