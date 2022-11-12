using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class MotorSetManager : MonoBehaviour, IPointerDownHandler
{
    [Header("Operate Item")]
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private List<Transform> allMotorTransform;
    [SerializeField] private Button goBackButton;

    [Header("內部變數")]
    private int currentID = -99;

    public void OnPointerDown(PointerEventData eventData)
    {
        for (int i = 0; i < allMotorTransform.Count; i++)
        {
            if (eventData.pointerCurrentRaycast.gameObject == allMotorTransform[i].gameObject)
            {
                currentID = i;
                allMotorTransform[currentID].DOScale(1.2f, 0.8f);

            }
            else
            {
                allMotorTransform[i].DOScale(1, 0.8f);
            }

        }
    }

    private void Start()
    {
        ButtonInit();
    }


    private void ButtonInit()
    {
        leftButton.onClick.AddListener(delegate { RotateMotor(270); });
        rightButton.onClick.AddListener(delegate { RotateMotor(90); });
        goBackButton.onClick.AddListener(delegate { CanvasManager.Instance.openCanvas("original"); });
    }


    private void RotateMotor(float dir)
    {
        if (currentID == -99)
        {
            SiginalUI.Instance.SiginalText("還未選擇馬達呦！", 1, 40);
        }
        if (((int)((allMotorTransform[currentID].rotation.z) / 90)) % 90 == 0)
        {
            dir += ((int)((allMotorTransform[currentID].rotation.z) / 90)) * 90;
        }
        else
        {
            dir += (((int)((allMotorTransform[currentID].rotation.z) / 90)) + 1) * 90;
        }
        Debug.Log((((int)((allMotorTransform[currentID].rotation.z) / 90)) + 1) * 90);
        //if (allMotorTransform[currentID].rotation.z % 90 > 1 || allMotorTransform[currentID].rotation.z < -1) return;
        dir += (((int)((allMotorTransform[currentID].rotation.z) / 90)) + 1) * 90;
        while (dir > 360) dir -= 360;
        Vector3 rotateValue = new Vector3(0, 0, dir);
        allMotorTransform[currentID].DORotate(rotateValue, .5f);
    }
}
