using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggablePlier : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private Vector3 initialPosition;
    [SerializeField] private CanvasGroup canvasGroup;

    public static DraggablePlier Instance;

    [SerializeField] GameObject topzone;
    [SerializeField] GameObject underzone;

    private void Awake()
    {
        Instance = this;

        rectTransform = GetComponent<RectTransform>();
        initialPosition = GetComponent<RectTransform>().anchoredPosition;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("點下去了");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (WiresawManager.Instance.isFinished)
        {
            SiginalUI.Instance.SiginalText("已組裝完成，想重新組裝請重新進入此頁面");
            return;
        }
        
        canvasGroup.blocksRaycasts = false;
        //canvasGroup.alpha = .6f;
        topzone.SetActive(true);
        underzone.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        //canvasGroup.alpha = 1f;
        //rectTransform.anchoredPosition = initialPosition;
        goToOriginal();
    }

    public void goToOriginal()
    {
        rectTransform.anchoredPosition = initialPosition;
    }

}
