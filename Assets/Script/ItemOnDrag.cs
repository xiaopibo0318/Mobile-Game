using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;
    private Vector3 origPosition;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        //transform.SetParent(transform.parent.parent);
        //獲取點級位置
        origPosition = transform.localPosition;
        Debug.Log($"transform position:{transform.position} localPosiotion:{transform.localPosition}");
        Debug.Log($"anchored:{((RectTransform)transform).anchoredPosition}");
        Debug.Log($"eventData position:{eventData.position} press:{eventData.pressPosition}");
        //transform.position = eventData.position;
        //GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition = origPosition + (Vector3)(eventData.position - eventData.pressPosition);
        //transform.position = eventData.position;
        //Debug.Log("移動座標：" + transform.position);
        //Debug.Log("雷射座標：" + eventData.position);

        //Debug.Log("目前移過去底下的名字" + eventData.pointerCurrentRaycast);
        //Debug.Log("目前移過去底下的名字" + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = origPosition;
        //if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage")
        //{
        //    transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
        //    transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;

        //    eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
        //    eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
        //    GetComponent<CanvasGroup>().blocksRaycasts = true;
        //    return;
        //}

        //if (eventData.pointerCurrentRaycast.gameObject == null || !eventData.pointerCurrentRaycast.gameObject.CompareTag("Slot"))
        //{
        //    transform.position = originalParent.position;
        //    transform.SetParent(originalParent);
        //    GetComponent<CanvasGroup>().blocksRaycasts = true;

        //    return;
        //}


        //transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
        //transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
        //GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
