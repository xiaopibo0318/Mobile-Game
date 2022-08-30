using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour,IPointerDownHandler, IDragHandler, IBeginDragHandler,IEndDragHandler
{
    private UnityAction<PointerEventData> onPointerDown;

    private UnityAction<PointerEventData> onDrag;
    private UnityAction<PointerEventData> onBeginDrag;
    private UnityAction<PointerEventData> onEndDrag;

    public void AddPointerDownListener(UnityAction<PointerEventData>_onPointerDown)
    {
        onPointerDown = _onPointerDown;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        onPointerDown?.Invoke(eventData);
    }


    public void AddOnDragListener(UnityAction<PointerEventData> _onDrag)
    {
        onDrag = _onDrag;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        onDrag?.Invoke(eventData);
    }


    public void AddBeginDragListener(UnityAction<PointerEventData> _onBeginDragListener)
    {
        onBeginDrag = _onBeginDragListener;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        onBeginDrag?.Invoke(eventData);
    }


    public void AddEndDragListener(UnityAction<PointerEventData> _onEndDrag)
    {
        onEndDrag = _onEndDrag;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        onEndDrag?.Invoke(eventData);
    }

}
