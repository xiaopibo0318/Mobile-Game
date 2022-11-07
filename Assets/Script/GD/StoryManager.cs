using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchEvent_Handler;
using DG.Tweening;
using UnityEngine.UI;

public class StoryManager : EventDetect
{
    
    //private Sprite[] allStory;
    //[SerializeField] private Image currentImage;
    //private RectTransform originalTransform;

    //[HideInInspector] public int currentID { get; set; }

    //private void Start()
    //{
    //    allStory = Resources.LoadAll<Sprite>("");
    //    originalTransform = currentImage.rectTransform;
    //}

    //private void SwitchImage()
    //{
    //    currentImage.sprite = allStory[currentID];
    //}

    //public override void ChangeScale(Touch touch1, Touch touch2)
    //{
    //    currentImage.rectTransform.anchorMin = Vector2.zero;
    //    currentImage.rectTransform.anchorMax = Vector2.zero;
    //    currentImage.transform.DOScale(scaleOffset, .2f);
    //    Vector3 newPivotPos = (touch1.position + touch2.position) / 2;
    //    currentImage.rectTransform.anchoredPosition = newPivotPos;
    //}


    //public override void ResetImage()
    //{
    //    currentImage.rectTransform.localPosition = originalTransform.localPosition;
    //    currentImage.rectTransform.anchoredPosition = originalTransform.anchoredPosition;

    //}
}
