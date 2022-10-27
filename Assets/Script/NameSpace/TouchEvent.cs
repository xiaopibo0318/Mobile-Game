using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TouchEvent_Handler
{
    public class EventDetect : MonoBehaviour, IPointerDownHandler
    {
        public float beginTime = 0;
        public float intervals;
        private float lastTouchTime;
        public float holdTime = 3;
        private Vector2 startPos = Vector2.zero;
        private Vector2 endPos = Vector2.zero;
        public Vector2 moveDirection = Vector2.zero;
        private Coroutine coroutine = null;

        public void OnPointerDown(PointerEventData eventData)
        {
            coroutine = StartCoroutine(StartDetect(eventData));
            TouchDetect(eventData);
        }

        public void TouchDetect(PointerEventData eventData)
        {
            Debug.Log("有典籍到物品A");



            if (Input.touchCount >= 1)
            {
                Touch touch = Input.GetTouch(0);
                Debug.Log(touch.phase.ToString());
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        moveDirection = eventData.position - startPos;
                        intervals = Time.realtimeSinceStartup - beginTime;

                        Hold();
                        break;
                    case TouchPhase.Ended://手離開螢幕時的狀態
                        intervals = Time.realtimeSinceStartup - beginTime;
                        lastTouchTime = Time.realtimeSinceStartup;
                        endPos = startPos + moveDirection;
                        Swipe(intervals, moveDirection);
                        break;

                }
            }
        }

        IEnumerator StartDetect(PointerEventData eventData)
        {
            float detectTime = 0.1f;
            while (detectTime > 0)
            {
                detectTime -= Time.deltaTime;
                TouchDetect(eventData);
                yield return null;
            }
        }


        public virtual void Hold()
        {

        }

        public virtual void Swipe(float intervalTime, Vector2 moveDirect)
        {

        }


    }
}
