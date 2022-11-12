using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchEvent_Handler;
using DG.Tweening;
using UnityEngine.UI;

public class StoryManager : EventDetect
{
    private Sprite[] allStoryBamboo;
    private Sprite[] allStoryPoster;
    [SerializeField] private Image currentImage;
    private RectTransform originalTransform;
    [SerializeField] private Button goBackButton;

    public static StoryManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    [HideInInspector] public int currentID { get; set; }

    private void Start()
    {
        allStoryBamboo = Resources.LoadAll<Sprite>("BambooSlips");
        allStoryPoster = Resources.LoadAll<Sprite>("Poster");
        originalTransform = currentImage.rectTransform;
        goBackButton.onClick.AddListener(delegate { CanvasManager.Instance.openCanvas("original"); });
    }

    public void SwitchImage()
    {
        currentImage.sprite = allStoryBamboo[currentID];
    }

    public void SwitchPoster()
    {
        currentImage.sprite = allStoryPoster[currentID];
        currentImage.preserveAspect = true;
    }

    public override void ChangeScale(Touch touch1, Touch touch2)
    {
        currentImage.rectTransform.anchorMin = Vector2.zero;
        currentImage.rectTransform.anchorMax = Vector2.zero;
        currentImage.transform.DOScale(scaleOffset, .2f);
        Vector3 newPivotPos = (touch1.position + touch2.position) / 2;
        currentImage.rectTransform.anchoredPosition = newPivotPos;
    }


    public override void ResetImage()
    {
        //currentImage.rectTransform.localPosition = originalTransform.localPosition;
        //currentImage.rectTransform.anchoredPosition = originalTransform.anchoredPosition;
        currentImage.transform.position = new Vector3(Screen.width / 2, Screen.height / 2);
        currentImage.transform.localScale = Vector3.one;

    }
}
