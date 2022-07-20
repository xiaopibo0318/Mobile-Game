using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class blocksOnDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform originalParent;
    public blockList exeList;

    int nowBlock;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        Instantiate(gameObject, originalParent);
        transform.position = eventData.position;
        nowBlock = getBlockID(gameObject.name);
        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (eventData.pointerCurrentRaycast.gameObject.name == "blockSlot(Clone)")
        {
            Debug.Log(1);
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            exeList.exeList[eventData.pointerCurrentRaycast.gameObject.GetComponent<blockSlot>().slotID] = nowBlock;
        }
        else
        {
            Destroy(gameObject);
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }


    public int getBlockID(string name)
    {
        Debug.Log(name);
        Debug.Log(name.Length);
        if (name.Contains("goStraight"))
        {
            return 1;
        }
        else if (name.Contains("turnLeft"))
        {
            return 11;
        }
        else if (name.Contains("turnRight"))
        {
            return 12;
        }
        else
        {
            return 0;
        }
    }


}
