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

    private Sprite[] allPic;
    private QuesInfo[] allQuesInfo;
    private List<Transform> allPicTransform = new List<Transform>();
    private Dictionary<int, List<Vector3>> leftPos = new Dictionary<int, List<Vector3>>();
    private Dictionary<int, List<Vector3>> rightPos = new Dictionary<int, List<Vector3>>();
    [SerializeField] private Button enterButton;

    private int currentID;

    public int CurrentID
    {
        get => currentID;
        set { currentID = value; }
    }


    private void Start()
    {
        LoadPicture();
        InitScreen();
        enterButton.onClick.AddListener(GoToAnswer);
    }


    private void LoadPicture()
    {
        allPic = Resources.LoadAll<Sprite>("ElectricQues");

        allQuesInfo = new QuesInfo[allPic.Length];
        for (int i = 0; i < allPic.Length; i++)
        {
            var index = i;
            allQuesInfo[index] = new QuesInfo("問題 " + index.ToString(), "000");
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
            RectTransform tempCreate = Instantiate(prefab, prefabParent);
            //定位0,0 1,1 拉滿版塊
            tempCreate.anchorMin = Vector2.zero;
            tempCreate.anchorMax = Vector2.one;
            tempCreate.localScale = Vector3.one * (1 - (i * reduceSize));
            tempCreate.anchoredPosition = new Vector2(i * (prefab.sizeDelta.x + gaps -
                ((i * reduceSize)) * tempCreate.sizeDelta.x / 2), 0);
            tempCreate.Find("Image").GetComponent<Image>().sprite = allPic[i];
            tempCreate.Find("TitleText").GetComponent<Text>().text = allQuesInfo[0].title;
            rightPos.Add(i, new List<Vector3>() { tempCreate.transform.position, tempCreate.localScale });
            allPicTransform.Add(tempCreate);
        }

        for (int i = 0; i < rightPos.Count; i++)
        {
            leftPos.Add(i, new List<Vector3>() {
                allPicTransform[0].position - (allPicTransform[i].position - allPicTransform[0].position), rightPos[i][1] });
        }
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
