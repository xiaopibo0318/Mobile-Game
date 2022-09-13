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
    public string GetUserName;
    public string GetUserEmail;

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

            if (www.isNetworkError || www.isHttpError)
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
                if (loginText == "Login Success.")
                {
                    iflogin = true;
                    StartCoroutine(GetUserInformaton(username));
                }
                else
                {
                    iflogin = false;
                }
            }
        }
    }
    [System.Obsolete]
    IEnumerator GetUserInformaton(string username)
    {

        string URL = "https://h85522.000webhostapp.com/userSelect.php";
        string[] usersData;
        WWW users = new WWW(URL);
        yield return users;
        string usersDataString = users.text;
        usersData = usersDataString.Split(';');

        int i = 0;
        //print(usersData.Length);
        while (i <= usersData.Length - 2 && usersData[i] != null)
        {
            if (GetValueData(usersData[i], "username:") == username)
            {
                /*print(GetValueData(usersData[i], "username:"));//設定輸出第i位資料的指定內容(username等)
                print(GetValueData(usersData[i], "email:"));
                print(GetValueData(usersData[i], "password:"));*/
                GetUserName = GetValueData(usersData[i], "username:");
                GetUserEmail = GetValueData(usersData[i], "email:");
                Debug.Log(GetUserName);
                Debug.Log(GetUserEmail);

            }
            i += 1;
        }
    }
    string GetValueData(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }


    public IEnumerator RegisterUser(string username, string password, string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPassword", password);
        form.AddField("addEmail", email);

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
