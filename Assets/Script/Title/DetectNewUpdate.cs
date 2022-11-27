using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace UpgradeSystem
{
    struct GameData
    {
        public string Description;
        public string Version;
        public string Url;
    }

    public class DetectNewUpdate : Singleton<DetectNewUpdate>
    {
        [Header("UI介面")]
        [SerializeField] private Button notNowButton;
        [SerializeField] private Button updateButton;
        [SerializeField] private Text mainText;

        [Header("數據網址")]
        private string jsonDataURL = "https://drive.google.com/uc?export=download&id=1NT6QyCfeu-0HQmYtGqit6rgCch1qeMm0";

        public static bool isAlreadyCheckForNewUpdate = false;

        GameData lastGameData;




        private void Start()
        {
            ButtonInit();
        }

        public void StartDetect()
        {
            if (!isAlreadyCheckForNewUpdate)
            {
                StartCoroutine(CheckForUpdate());
            }
            
        }




        IEnumerator CheckForUpdate()
        {
            Debug.Log("開始傳網路");
            UnityWebRequest request = UnityWebRequest.Get(jsonDataURL);
            request.chunkedTransfer = false;
            request.disposeDownloadHandlerOnDispose = true;
            request.timeout = 60;
            Debug.Log(request.isNetworkError);
            yield return request.Send();
            Debug.Log($"是否請求成功：{request.isDone}");
            if (request.isDone)
            {
                isAlreadyCheckForNewUpdate = true;
                if (!request.isNetworkError)
                {
                    Debug.Log("文字"+request.downloadHandler.text);
                    lastGameData = JsonUtility.FromJson<GameData>(request.downloadHandler.text);
                    Debug.Log($"讀取到的版本號為{lastGameData.Version}");
                    if (string.IsNullOrEmpty(lastGameData.Version) || !Application.version.Equals(lastGameData.Version))
                    {
                        //
                        mainText.text = lastGameData.Description +"\n" + lastGameData.Version +"\n當前版本為：\n" +Application.version;
                        MenuManager.Instance.OpenMenu("CheckForUpdate");
                        //showPop
                    }
                }
            }

            request.Dispose();



        }



        private void ButtonInit()
        {
            notNowButton.onClick.AddListener(() =>
           {
               MenuManager.Instance.OpenMenu("title");
           });


            updateButton.onClick.AddListener(() =>
            {
                Application.OpenURL(lastGameData.Url);
                MenuManager.Instance.OpenMenu("title");
            });
        }

    }

}
