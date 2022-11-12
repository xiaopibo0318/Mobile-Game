﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TouchEvent_Handler
{
    public class EventDetect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public float beginTime = 0;
        public float intervals;
        private float lastTouchTime;
        public float holdTime = 3;
        private Vector2 startPos = Vector2.zero;
        private Vector2 endPos = Vector2.zero;
        public Vector2 moveDirection = Vector2.zero;
        private PointerEventData touch1Data = null;
        private Coroutine coroutine = null;
        private bool isTouch = false;
        private float delayDetectTime = 20f;
        public float scaleOffset;
        Touch oldTouch1;
        Touch oldTouch2;
        public void OnPointerDown(PointerEventData eventData)
        {
            isTouch = true;
            touch1Data = eventData;
            PointerDown(eventData);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            isTouch = false;
            ResetImage();
        }

        private void Update()
        {
            if (isTouch)
            {
                delayDetectTime -= 1;
                if (delayDetectTime < 0)
                {
                    delayDetectTime = 20;
                    TouchDetect(touch1Data);
                }
            }
        }

        public void TouchDetect(PointerEventData eventData)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                Debug.Log(touch.phase.ToString());
                startPos = eventData.position;
                switch (touch.phase)
                {
                    case TouchPhase.Moved:
                        moveDirection = touch.deltaPosition;
                        intervals = Time.realtimeSinceStartup - beginTime;
                        Move();
                        break;
                }
            }
            if (Input.touchCount == 2)
            {
                Touch newTouch1 = Input.GetTouch(0);
                Touch newTouch2 = Input.GetTouch(1);

                if (newTouch1.phase == TouchPhase.Ended || newTouch2.phase == TouchPhase.Ended)
                {
                    Debug.Log("觸發歸位");
                    ResetImage();
                }

                ///先記錄第一次點的位置
                if (newTouch2.phase == TouchPhase.Began)
                {
                    oldTouch1 = newTouch1;
                    oldTouch2 = newTouch2;
                    Init();
                    return;
                }

                float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
                float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);
                Debug.Log("老:" + oldDistance + "新" + newDistance);
                float offset = newDistance - oldDistance;
                scaleOffset = 1 + (offset / 500);

                ChangeScale(newTouch1, newTouch2);

                //oldTouch1 = newTouch1;
                //oldTouch2 = newTouch2;





            }
        }


        public virtual void Move() { }
        public virtual void PointerDown(PointerEventData eventData) { }
        public virtual void Init() { }
        public virtual void ChangeScale(Touch touch1, Touch touch2) { }
        public virtual void ResetImage() { }


    }

    public class WorldOffset
    {
        public Vector2 GetOriginPos(Vector2 nowPoint)
        {
            float w = nowPoint.x;
            float h = nowPoint.y;
            float changeScale_x = w / 1800;
            float changeScale_y = h / 900;

            Vector2 afterResetPos = new Vector2(nowPoint.x /= changeScale_x, nowPoint.y /= changeScale_y);
            return afterResetPos;
        }
    }

}
