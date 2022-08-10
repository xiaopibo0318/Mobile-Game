using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SandPaperOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;

    int nowPaper;

    float stayTime;


    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        Instantiate(gameObject, originalParent);
        transform.position = eventData.position;
        nowPaper = getPaperNum(gameObject.name);
        stayTime = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        if (eventData.pointerCurrentRaycast.gameObject.name == "Circle")
        {
            stayTime += 0.1f;
        }
        Debug.Log(stayTime);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        if (eventData.pointerCurrentRaycast.gameObject.name == "Circle")
        {
            Debug.Log(1);
            //transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            //transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            if(stayTime >= 5)
            {
                Debug.Log("成功砂磨");
                SandPaperManager.Instance.IfCrackSucess();
                Destroy(gameObject);
                stayTime = 0;
            }
 
        }
        else
        {
            Destroy(gameObject);
            stayTime = 0;
        }
        Destroy(gameObject);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }


    public int getPaperNum(string name)
    {
        Debug.Log(name);
        Debug.Log(name.Length);
        if (name.Contains("SandPaper150"))
        {
            return 150;
        }
        else if (name.Contains("SandPaper240"))
        {
            return 240;
        }
        else if (name.Contains("SandPaper400"))
        {
            return 400;
        }
        else
        {
            return 0;
        }
    }
}
