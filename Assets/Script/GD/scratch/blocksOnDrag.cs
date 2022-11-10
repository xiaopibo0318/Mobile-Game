using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class blocksOnDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Transform originalParent;
    public blockList exeList;
    [Header("前進方格")]
    private InputField walkStep;

    [Header("蜂鳴器")]
    private InputField frequencyNum;
    private Dropdown buzzerPin;

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
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (eventData.pointerCurrentRaycast.gameObject.name == "blockSlot(Clone)")
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            exeList.exeList[eventData.pointerCurrentRaycast.gameObject.GetComponent<blockSlot>().slotID] = nowBlock;
            if (nowBlock == 1)
            {
                int temp;
                walkStep = this.GetComponentInChildren<InputField>();
                if (int.TryParse(walkStep.text, out temp) == true)
                {

                    int.TryParse(walkStep.text, out temp);
                    ScratchManager.Instance.AddMoveStepToList(temp);
                }
                else
                {
                    Debug.Log("不是數字");
                }
            }
            else if (nowBlock == 21)
            {
                frequencyNum = this.GetComponentInChildren<InputField>();
                buzzerPin = this.GetComponentInChildren<Dropdown>();
                CodingManager.Instance.frequencyBuzzer = frequencyNum.text;
                CodingManager.Instance.pinBuzzer = buzzerPin.value;

            }


        }
        else
        {
            Destroy(gameObject);
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }


    public int getBlockID(string name)
    {
        if (name.Contains("goStraight"))
        {
            return 1;
        }
        else if (name.Contains("turnLeft"))
        {
            return 11;
        }
        else if (name.Contains("turnRight")) return 12;
        else if (name.Contains("buzzer")) return 21;
        else
        {
            return 0;
        }
    }


}
