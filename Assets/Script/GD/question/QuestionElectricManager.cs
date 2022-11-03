using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TouchEvent_Handler;

public class QuestionElectricManager : EventDetect
{
    private float gaps = 450;
    private float reduceSize = 0.2f;
    public RectTransform prefab;
    public Transform prefabParent;

    [Header("圖片素材")]
    private Sprite[] allPic;
    private QuesInfo[] allQuesInfo;
    private List<Transform> allPicTransform = new List<Transform>();
    private Dictionary<int, List<Vector3>> leftPos = new Dictionary<int, List<Vector3>>();
    private Dictionary<int, List<Vector3>> rightPos = new Dictionary<int, List<Vector3>>();

    [Header("按鈕")]
    [SerializeField] private Button enterButton;
    [SerializeField] private List<Button> answerButton;
    [SerializeField] private Button goBackButton;

    [Header("判定區域")]
    private int currentID;
    private bool[] answer = new bool[4];

    public int CurrentID
    {
        get => currentID;
        set { currentID = value; }
    }


    private void Start()
    {
        
    }

    private void OnEnable()
    {
        LoadPicture();
        InitScreen();
        ButtonInit();
        for (int i = 0; i < answer.Length; i++)
        {
            answer[i] = false;
        }
    }

    private void LoadPicture()
    {
        allPic = Resources.LoadAll<Sprite>("ElectricQues");

        allQuesInfo = new QuesInfo[allPic.Length];
        for (int i = 0; i < allPic.Length; i++)
        {
            var index = i;
            allQuesInfo[index] = new QuesInfo("問題 " + index.ToString(), "內容");
        }
    }


    /// <summary>
    /// 像左滑、實際是往右邊切
    /// </summary>
    private void LeftSlide()
    {
        if (currentID < allPic.Length - 1)
        {
            currentID += 1;
            Slide();
        }
    }

    private void RightSlide()
    {
        if (currentID > 0)
        {
            currentID -= 1;
            Slide();
        }
    }

    private void Slide()
    {
        for (int i = 0; i < allPicTransform.Count; i++)
        {
            if (i - currentID < 0)
            {
                allPicTransform[i].DOMove(leftPos[Mathf.Abs(currentID - i)][0], .9f);
                allPicTransform[i].DOScale(leftPos[Mathf.Abs(currentID - i)][1], .9f);
            }
            else
            {
                allPicTransform[i].DOMove(rightPos[i - currentID][0], .9f);
                allPicTransform[i].DOScale(rightPos[i - currentID][1], .9f);
            }

        }

    }

    private void InitScreen()
    {
        for (int i = 0; i < allPic.Length; i++)
        {
            var index = i;
            RectTransform tempCreate = Instantiate(prefab, prefabParent);
            //定位0,0 1,1 拉滿版塊
            tempCreate.anchorMin = Vector2.zero;
            tempCreate.anchorMax = Vector2.one;
            tempCreate.localScale = Vector3.one * (1 - (index * reduceSize));
            tempCreate.anchoredPosition = new Vector2(index * (prefab.sizeDelta.x + gaps -
                ((index * reduceSize)) * tempCreate.sizeDelta.x / 2), 0);
            tempCreate.Find("Image").GetComponent<Image>().sprite = allPic[index];
            tempCreate.Find("TitleText").GetComponent<Text>().text = allQuesInfo[0].title;
            rightPos.Add(index, new List<Vector3>() { tempCreate.transform.position, tempCreate.localScale });
            allPicTransform.Add(tempCreate);
        }

        for (int i = 0; i < rightPos.Count; i++)
        {
            leftPos.Add(i, new List<Vector3>() {
                allPicTransform[0].position - (allPicTransform[i].position - allPicTransform[0].position), rightPos[i][1] });
        }
    }

    private void ButtonInit()
    {
        for (int i = 0; i < answerButton.Count; i++)
        {
            int index = i;
            answerButton[i].onClick.AddListener(delegate { AnswerQues(index); });
            answerButton[i].gameObject.SetActive(false);
        }
        enterButton.onClick.AddListener(GoToAnswer);
        goBackButton.onClick.AddListener(GoBackToChoose);
        goBackButton.gameObject.SetActive(false);
    }

    public override void Hold()
    {
        Debug.Log("移動的位置差" + moveDirection.x);
        if (moveDirection.x > 0)
        {
            RightSlide();
        }
        else
        {
            LeftSlide();
        }
        Debug.Log("觸發移動");

    }


    private void GoToAnswer()
    {
        Vector3 endPos = new Vector3(400, 450, 0);
        for (int i = 0; i < allPicTransform.Count; i++)
        {
            if (i == currentID)
            {
                allPicTransform[currentID].DOMove(endPos, .8f);
            }
            else
            {
                allPicTransform[i].gameObject.SetActive(false);
            }
        }
        enterButton.gameObject.SetActive(false);

        for (int i = 0; i < answerButton.Count; i++)
        {
            if (!answer[i])
                answerButton[i].gameObject.SetActive(true);
            else
                answerButton[i].gameObject.SetActive(false);
        }
        goBackButton.gameObject.SetActive(true);

    }

    /// <summary>
    /// index:
    /// 0 可以亮
    /// 1 搭配程式會亮
    /// 2 不能亮
    /// 3 短路
    /// 
    /// curentID : 1 可以亮; 2 短路; 3 不會亮; 4 搭配程式可以亮
    /// </summary>
    /// <param name="index"></param>
    private void AnswerQues(int index)
    {
        switch (currentID)
        {
            case 1:
                if (index == 0)
                {
                    SuccessButton(index);
                }
                break;
            case 2:
                if (index == 3)
                {
                    SuccessButton(index);
                }
                break;
            case 3:
                if (index == 2)
                {
                    SuccessButton(index);
                }
                break;
            case 4:
                if (index == 1)
                {
                    SuccessButton(index);
                }
                break;
        }
    }

    private void GoBackToChoose()
    {
        for (int i = 0; i < allPicTransform.Count; i++)
        {
            allPicTransform[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < answerButton.Count; i++)
        {
            answerButton[i].gameObject.SetActive(false);
        }
        enterButton.gameObject.SetActive(true);
        goBackButton.gameObject.SetActive(false);
        Slide();
    }

    private void SuccessButton(int index)
    {
        answerButton[index].gameObject.SetActive(false);
        answer[index] = true;
        GoBackToChoose();
        SiginalUI.Instance.SiginalText("正確");

        for (int i = 0; i < answer.Length; i++)
        {
            if (!answer[i]) return;
        }
        SiginalUI.Instance.SiginalText("全數答對\n棒棒");

    }


}


public class QuesInfo
{
    public string title { get; set; }
    public string content { get; set; }


    public QuesInfo(string _title, string _content)
    {
        this.title = _title;
        this.content = _content;
    }

}
