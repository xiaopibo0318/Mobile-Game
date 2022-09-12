﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;
    public Inventory myBag;
    private int currentSlotID;



    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentSlotID = originalParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent);
        //獲取點級位置
        originalParent.position = transform.position;
        //Debug.Log($"transform position:{transform.position} localPosiotion:{transform.localPosition}");
        //Debug.Log($"anchored:{((RectTransform)transform).anchoredPosition}");
        //Debug.Log($"eventData position:{eventData.position} press:{eventData.pressPosition}");
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //transform.localPosition = origPosition + (Vector3)(eventData.position - eventData.pressPosition);
        transform.position = eventData.position;
        //Debug.Log("移動座標：" + transform.position);
        //Debug.Log("雷射座標：" + eventData.position);

        //Debug.Log("目前移過去底下的名字" + eventData.pointerCurrentRaycast);
        //Debug.Log("目前移過去底下的名字" + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            //transform.localPosition = origPosition;
            if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage")
            {
                Debug.Log("可換換");
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;

                var temp = myBag.itemList[currentSlotID];
                myBag.itemList[currentSlotID] = myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;


                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }

            if (eventData.pointerCurrentRaycast.gameObject.name == "Slot(Clone)")
            {
                //如果沒東西就直接放上去
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;

                //若為空的數據轉換
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = myBag.itemList[currentSlotID];

                //防止自己移動到自己的格子裡。
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID != currentSlotID)
                    myBag.itemList[currentSlotID] = null;


                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }

            ///<summary>
            ///考慮拉到其他東西。
            /// </summary>
            DeleteItem();

        }
        else
        {
            DeleteItem();
        }

    }

    private void DeleteItem()
    {
        //丟棄系統 還沒寫好
        if (myBag.itemList[currentSlotID] != null)
        {
            //不可丟棄道具，如西王母的愛
            if (myBag.itemList[currentSlotID].missioable)
            {
                InventoryManager.Instance.CouldNotCrash();
                transform.position = originalParent.position;
                transform.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;

                return;
            }
            InventoryManager.Instance.SiginalText(myBag.itemList[currentSlotID]);
        }
        transform.position = originalParent.position;
        transform.SetParent(originalParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

}
