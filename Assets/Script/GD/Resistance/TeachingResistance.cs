using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class TeachingResistance : MonoBehaviour, IPointerDownHandler
{
    private bool isFirst = true;
    private int teachStep = 0;
    [SerializeField] private Image resistanceImage;
    [SerializeField] private Image teachImage;
    [SerializeField] private Image colorImage;
    [SerializeField] private List<Text> mainText;

    public void OnPointerDown(PointerEventData eventData)
    {
        teachStep += 1;
        TeachResistance(teachStep);
    }

    private void OnEnable()
    {
        if (isFirst) TeachResistance(0);
    }

    private void TeachResistance(int index)
    {
        if (index > 0 && index < mainText.Count-1)
        {
            mainText[index - 1].gameObject.SetActive(false);
            mainText[index].gameObject.SetActive(true);
        }
        switch (index)
        {
            case 0:
                ResetTeach();
                mainText[0].gameObject.SetActive(true);
                resistanceImage.gameObject.SetActive(true);
                break;
            case 1:
                Vector3 temp = new Vector3(1100, 450, 0);
                resistanceImage.transform.DOMove(temp, .9f);
                Vector3 temp2 = new Vector3(0, 0, -90);
                resistanceImage.transform.DORotate(temp2, .9f);
                break;
            case 2:
                ///無
                break;
            case 3:
                resistanceImage.gameObject.SetActive(false);
                teachImage.gameObject.SetActive(true);
                break;
            case 4:
                teachImage.gameObject.SetActive(false);
                resistanceImage.gameObject.SetActive(true);
                colorImage.gameObject.SetActive(true);
                break;
            case 5:
                mainText[4].gameObject.SetActive(false);
                break;
            case 6:
                colorImage.gameObject.SetActive(false);
                SiginalUI.Instance.TextInterectvie("是否還要再看一次", agianTeach);
                break;
            default:
                break;


        }

    }

    private void ResetTeach()
    {
        for (int i = 0; i < mainText.Count; i++)
        {
            mainText[i].gameObject.SetActive(false);
        }
        resistanceImage.gameObject.SetActive(false);
        colorImage.gameObject.SetActive(false);
        teachImage.gameObject.SetActive(false);
        teachStep = 0;
    }


    private void agianTeach() =>  TeachResistance(0);
}
