using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using TouchEvent_Handler;
using UnityEngine.EventSystems;

public class FourPillarManager : EventDetect
{
    [SerializeField] private Transform optionParent;
    [SerializeField] private GameObject optionPrefab;
    private List<Transform> allOption = new List<Transform>();
    private int currentID;
    [SerializeField] private Button confirmButton;
    private bool isError = false;
    private float lastTime;
    [SerializeField] private Text title;
    [SerializeField] private Text content;
    [SerializeField] private Button goBackButton;

    List<QuesInfo> quesInfos = new List<QuesInfo>();

    private Coroutine coroutine = null;
    public int nowQues { get; set; }

    public static FourPillarManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }


    private void Start()
    {
        CreateOption();
        confirmButton.onClick.AddListener(DetectAnswer);
        InitQues();

        goBackButton.onClick.AddListener(delegate { CanvasManager.Instance.openCanvas("original"); });


    }

    private void InitQues()
    {
        quesInfos.Clear();
        QuesInfo temp = new QuesInfo("問題A", "請問宮殿有幾根柱子？");
        quesInfos.Add(temp);
        temp = new QuesInfo("問題B", "與崑崙山的交易中幾成的絲綢品是有問題的？");
        quesInfos.Add(temp);
        temp = new QuesInfo("問題C", "宮殿自崑崙山購入了幾公斤的草藥？");
        quesInfos.Add(temp);
        temp = new QuesInfo("問題D", "數位訊號為不連續值，以二進位表示，代表低電位的數字是？");
        quesInfos.Add(temp);
    }

    public void TriggerQues()
    {
        title.text = quesInfos[nowQues].title;
        content.text = quesInfos[nowQues].content;
    }

    private void CreateOption()
    {
        for (int i = 0; i < 10; i++)
        {
            int index = i;
            GameObject tempOption = Instantiate(optionPrefab, optionParent);
            tempOption.name = index.ToString();
            var temp = tempOption.GetComponentInChildren<Text>();
            temp.gameObject.name = index.ToString();
            temp.text = index.ToString();
            allOption.Add(tempOption.transform);
        }
    }

    public override void PointerDown(PointerEventData eventData)
    {
        base.PointerDown(eventData);

        //if (eventData.pointerCurrentRaycast.gameObject == null) Debug.Log("沒料");
        //else Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        for (int i = 0; i < allOption.Count; i++)
        {
            if (eventData.pointerCurrentRaycast.gameObject.name == i.ToString())
            {
                currentID = i;
                Debug.Log(currentID);
            }
        }
        for (int i = 0; i < allOption.Count; i++)
        {
            if (i == currentID) allOption[currentID].DOScale(1.2f, 0.8f);
            else allOption[i].DOScale(1, 0.8f);
        }
    }

    private void DetectAnswer()
    {
        if (isError)
        {
            SiginalUI.Instance.SiginalText("發動機過熱中\n距離冷卻結束\n還有" + lastTime.ToString("f2") + "秒");
            return;
        }

        SiginalUI.Instance.TextInterectvie
            ("請問是否確定\"" + currentID.ToString() + "\"這個答案", SelectYesOption);
    }

    private void SelectYesOption()
    {
        switch (nowQues)
        {
            case 0:
                if (currentID == 7)
                {
                    SiginalUI.Instance.SiginalText("沒錯！\n宮殿有7根柱子呦！");
                    cacheVisable.Instance.siginalSomething("沒錯！\n宮殿有7根柱子呦！");
                }
                else { TriggerErrorOption(); }
                break;
            case 1:
                if (currentID == 1)
                {
                    SiginalUI.Instance.SiginalText("沒錯！\n絲綢有1個有問題呦！");
                    cacheVisable.Instance.siginalSomething("沒錯！\n絲綢有1個有問題呦！");
                }
                else { TriggerErrorOption(); }
                break;
            case 2:
                if (currentID == 4)
                {
                    SiginalUI.Instance.SiginalText("沒錯！\n購入了4公斤的草藥呦！");
                    cacheVisable.Instance.siginalSomething("沒錯！\n購入了4公斤的草藥呦！");
                }
                else { TriggerErrorOption(); }
                break;
            case 3:
                if (currentID == 0)
                {
                    SiginalUI.Instance.SiginalText("沒錯！\n低電位的代表數字是0呦！");
                    cacheVisable.Instance.siginalSomething("沒錯！\n低電位的代表數字是0呦！");
                }
                else { TriggerErrorOption(); }
                break;
            default:
                Debug.Log("無料ㄝ");
                break;
        }
    }
    private void TriggerErrorOption()
    {
        if (coroutine != null) StopCoroutine(coroutine);
        SiginalUI.Instance.SiginalText("答錯囉！");
        coroutine = StartCoroutine(ErrorPunish());
    }

    IEnumerator ErrorPunish()
    {
        isError = true;
        float delay = 30;
        while (delay > 0)
        {
            yield return null;
            delay -= Time.deltaTime;
            lastTime = delay;
        }
        isError = false;
    }



}
