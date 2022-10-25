using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TouchEvent_Handler
{
    public class EventDetect : MonoBehaviour, IPointerDownHandler 
    {
        public static float beginTime = 0;
        public static float intervals;
        private static float lastTouchTime;
        public static float holdTime = 3;
        private static Vector2 startPos = Vector2.zero;
        private static Vector2 endPos = Vector2.zero;
        private static Vector2 moveDirection = Vector2.zero;

        public void OnPointerDown(PointerEventData eventData)
        {
            TouchDetect(eventData);
        }

        public void TouchDetect(PointerEventData eventData)
        {
            Debug.Log("有典籍到物品A");


            //switch (touch.phase)
            //{
            //    case TouchPhase.Moved:
            //        moveDirection = eventData.position - startPos;
            //        intervals = Time.realtimeSinceStartup - beginTime;

            //        Hold();
            //        break;
            //    case TouchPhase.Ended://手離開螢幕時的狀態
            //        intervals = Time.realtimeSinceStartup - beginTime;
            //        lastTouchTime = Time.realtimeSinceStartup;
            //        endPos = startPos + moveDirection;
            //        Swipe(intervals, moveDirection);
            //        break;
            //}


        }

        public virtual void Hold()
        {

        }

        public virtual void Swipe(float intervalTime, Vector2 moveDirect)
        {

        }


    }
}
