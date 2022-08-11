using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class CutWoodUI : MonoBehaviour
{
    private UnityAction lineClickDown;
    private UnityAction<PointerEventData> lineOnClick;
    private UnityAction lineClickUp;

    public void AddOperateLineListener(UnityAction _lineClickDown, UnityAction<PointerEventData> _lineOnClick, UnityAction _lineClickUp)
    {
        lineClickDown = _lineClickDown;
        lineOnClick = _lineOnClick;
        lineClickUp = _lineClickUp;
    }

    public void FixedUpdate()
    {
        
    }

    public void DrawLine()
    {
        
    }

}
