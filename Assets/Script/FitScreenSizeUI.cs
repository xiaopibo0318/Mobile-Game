using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 將需要變更大小的UI套用
/// </summary>
public class FitScreenSizeUI : MonoBehaviour
{
    private static float w = Screen.width;
    private static float h = Screen.height;

    private float changeScale_x = w / 1800;
    private float changeScale_y = h / 900;


    ///實際位移量
    float x = w / 1800 * 900;
    float y = h / 900 * 450;

    private RectTransform rectTransform;


    private void Awake()
    {
        rectTransform = this.gameObject.GetComponent<RectTransform>();

        rectTransform.localPosition = new Vector3(this.rectTransform.localPosition.x * changeScale_x,
            this.rectTransform.localPosition.y * changeScale_y, this.rectTransform.localPosition.z);

        rectTransform.localScale = new Vector3(changeScale_x, changeScale_y, this.rectTransform.localScale.z);
    }

    //public float GetChangeScale_x()
    //{
    //    return changeScale_x;
    //}

    //public float GetChangeScale_y()
    //{
    //    return changeScale_y;
    //}

}
