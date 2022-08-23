using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CutWoodManager : MonoBehaviour
{
    public GameObject mySelf;

    [Header("储存路径信息")]
    public List<GameObject> pathStorage;

    [Header("木頭種類信息")]
    public List<Item> woodList;

    [Header("分镜")]
    public GameObject woodChoose;
    public GameObject cutWood;

    [Header("选择要切的木头")]
    public List<GameObject> woodSelectStorage;
    public List<GameObject> woodSimpleStorage;
    List<int> woodIDList = new List<int> {301,302,303,311,321,331,341,351,361,371,601 };
    Button buttonCutWoodYes;
    Button buttonCutWoodNo;
    public GameObject siginal;
    int nowWoodID;
    public Item woodBoard;
    public Inventory myBag;

    [Header("切木頭畫面")]
    Button buttonGoBack;
    float cutTime;
    bool readyToCut;

    [Header("時間倒數")]
    public Text timerText;
    float totalTime;
    public float needMin;
    public float needSec;
    bool timeFinished;
    Coroutine timeCoroutine = null;

    [Header("顯示信息")]
    public SiginalUI siginalUI;


    [Header("事件")]
    private UnityAction<PointerEventData> onPointerDown;
    

    public void Awake()
    {
        Init();

        ResetWoodUI();
        GiveWoodID();
    }

    private void GiveWoodID()
    {
        var i = 0;
        foreach(var myWood in woodSelectStorage)
        {
            myWood.GetComponent<WoodWantToCut>().woodID = woodIDList[i];
            myWood.GetComponent<WoodWantToCut>().slotID = i;
            i += 1;
        }

        var j = 0;
        foreach (var woodPath in pathStorage)
        {
            woodPath.GetComponent<WoodPath>().woodID = woodIDList[j];
            j += 1;
        }
    }


    public void OpenWoodSimple(int openID)
    {
        siginal.SetActive(true);
        for (int i = 0; i < woodSimpleStorage.Count; i++)
        {
            if(openID == i)
            {
                woodSimpleStorage[i].SetActive(true);
                
            }
            else
            {
                woodSimpleStorage[i].SetActive(false);
            }   
        }
        nowWoodID = woodSelectStorage[openID].GetComponent<WoodWantToCut>().getWoodID();
    }

    private void CloseAllWoodSimple()
    {
        for (int i = 0; i < woodSimpleStorage.Count; i++)
        {
            woodSimpleStorage[i].SetActive(false);
        }
        siginal.SetActive(false);
    }

    private void CutWood()
    {
        if (!myBag.itemList.Contains(woodBoard)) return;

        siginal.SetActive(false);
        woodChoose.SetActive(false);
        cutWood.SetActive(true);
        foreach (var myWood in pathStorage)
        {
            if (myWood.GetComponent<WoodPath>().woodID == nowWoodID)
            {
                myWood.SetActive(true);
            }
            else myWood.SetActive(false);
        }
        InventoryManager.Instance.SubItem(woodBoard);
    }

    private void GoBackPage()
    {
        ResetWoodUI();
    }

    private void Init()
    {
        mySelf.SetActive(true);
        woodChoose.SetActive(true);
        cutWood.SetActive(true);
        siginal.SetActive(true);
        buttonCutWoodYes = GameObject.Find("CutWoodYes").GetComponent<Button>();
        buttonCutWoodYes.onClick.AddListener(CutWood);
        buttonCutWoodNo = GameObject.Find("CutWoodNo").GetComponent<Button>();
        buttonCutWoodNo.onClick.AddListener(CloseAllWoodSimple);
        buttonGoBack = GameObject.Find("GoBack").GetComponent<Button>();
        buttonGoBack.onClick.AddListener(GoBackPage);
        woodChoose.SetActive(false);
        cutWood.SetActive(false);
        siginal.SetActive(false);
        mySelf.SetActive(false);
    }


    public void ResetWoodUI()
    {
        woodChoose.SetActive(true);
        CloseAllWoodSimple();
        cutWood.SetActive(false);
        siginal.SetActive(false);
    }

    public void CutSucced()
    {
        siginalUI.SiginalText("切割成功，木頭已放入背包");
        StopCoroutine(Countdown());
        foreach (var myWood in woodList)
        {
            Debug.Log("現在的ID:"+nowWoodID);
            Debug.Log("切的木頭ID"+myWood.itemID);
            if(myWood.itemID == nowWoodID)
            {
                InventoryManager.Instance.AddNewItem(myWood);
                break;
            }
        }
    }

    public void CutFail()
    {
        StopCoroutine(timeCoroutine);
        needSec = 0;
        timerText.text = string.Format("{0}", needSec.ToString("f2")).Replace(".", ":");
        siginalUI.SiginalText("切割失敗");
    }

    public void StartCutWood()
    {
        needSec = 20;
        timeCoroutine = StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        //var temp = string.Format("{0}", needSec.ToString("f2")).Replace(".",":");
        timerText.text = string.Format("{0}", needSec.ToString("f2")).Replace(".", ":");
        totalTime =  needSec;
        timeFinished = false;
        while (totalTime > 0)
        {
            //等待一秒後執行
            yield return new WaitForSeconds(0.01f);

            totalTime -= 0.01f;
            needSec -= 0.01f;

            if (needSec < 0)
            {
                needSec = 0;
                Debug.Log("結束");
                timeFinished = true;
            }
            timerText.text = string.Format("{0}", needSec.ToString("f2")).Replace(".", ":");
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.N)){
            StartCutWood();
        }
    }

    public bool getTimeStatus()
    {
        return timeFinished;
    }

    //public void AddPointerDownListener(UnityAction<PointerEventData> _onPointerDown)
    //{
    //    onPointerDown = _onPointerDown;
    //}

    //void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    //{
    //    onPointerDown?.Invoke(eventData);
    //}

}
