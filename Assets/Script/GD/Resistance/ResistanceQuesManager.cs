using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TouchEvent_Handler;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ResistanceQuesManager : EventDetect
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
    private bool[] answer = new bool[3];

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
        ButtonInit();
        for (int i = 0; i < answer.Length; i++)
        {
            answer[i] = false;
        }
    }





    private void ButtonInit()
    {
        for (int i = 0; i < answerButton.Count; i++)
        {
            int index = i;
            answerButton[i].gameObject.SetActive(false);
        }
        enterButton.onClick.AddListener(GoToAnswer);
        goBackButton.onClick.AddListener(GoBackToChoose);
        goBackButton.gameObject.SetActive(false);
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
    }



    public override void PointerDown(PointerEventData eventData)
    {
        base.PointerDown(eventData);
        for (int i = 0; i < allPicTransform.Count; i++)
        {
            if (eventData.pointerCurrentRaycast.gameObject == allPicTransform[i].gameObject)
            {
                currentID = i;
            }
        }
        
    }

}


