using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DraggableKnife : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private Vector3 initialPosition;
    private Vector3 EndPosition;
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] GameObject topzone;
    [SerializeField] GameObject underzone;

    bool knifeOn;


    public static DraggableKnife Instance;

    private void Awake()
    {
        Instance = this;

        rectTransform = gameObject.GetComponent<RectTransform>();
        initialPosition = new Vector3(23, 238, 0);
        EndPosition = new Vector3 (-300, 200, 0);

        knifeOn = false;
        goToOriginal();

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("點下去了");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if( knifeOn == false)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = .8f;
            topzone.SetActive(false);
            underzone.SetActive(false);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        if(knifeOn == false)
        {
            goToOriginal();
        }
        else if ( knifeOn == true)
        {
            rectTransform.anchoredPosition = EndPosition;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {

    }

    public void goToOriginal()
    {
        knifeOn = false;
        gameObject.GetComponent<RectTransform>().localPosition = initialPosition;
    }

    public void addKnifeToWiresaw()
    {
        knifeOn = true;
        //wiresawManager.Instance.textBridge(10);
        gameObject.GetComponent<RectTransform>().localPosition = EndPosition;
    }

}
