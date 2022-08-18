using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    public bool iflogin = false;
    public GameObject MessagePanel;
    public Text messageText;

    [System.Obsolete]
    void Start()
    {
        MessagePanel.SetActive(false);
        StartCoroutine(GetText());       
    }

    void openMessage(string a)
    {
        MessagePanel.SetActive(true);
        messageText.text = a;
        Invoke("closeMessage", 3f);
    }
    void closeMessage()
    {
        MessagePanel.SetActive(false);
        if (iflogin == true)
        {
            MenuManager.Instance.OpenMenu("createcharacter");
        }
    }
    [System.Obsolete]
    IEnumerator GetText()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://h85522.000webhostapp.com/connection.php"))
        //using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/cc/connection.php"))
        {
            yield return www.Send();

            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                openMessage(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);//show results as text
                openMessage("Connected successfully.");
                byte[] results = www.downloadHandler.data;//or retrieve results as binary data  
            }
            

        }
        
    }
    [System.Obsolete]//舊版寫法需要加這個才不會有錯誤訊息
    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPassword", password);
        
        using (UnityWebRequest www = UnityWebRequest.Post("https://h85522.000webhostapp.com/Login.php", form))
        //using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/cc/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                openMessage(www.error);
            }
            else
            {
                string loginText = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);
                openMessage(www.downloadHandler.text);
                if (loginText== "Login Success.")
                {
                    iflogin = true;
                }
                else
                {
                    iflogin = false;
                }
                
            }
            
            
        }

    }
    public IEnumerator RegisterUser(string username, string password,string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPassword", password);
        form.AddField("addEmail",email);

        using (UnityWebRequest www = UnityWebRequest.Post("https://h85522.000webhostapp.com/RegisterUser.php", form))
        //using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/cc/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();


            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                openMessage(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                openMessage(www.downloadHandler.text);
            }
            
        }

    }
}
