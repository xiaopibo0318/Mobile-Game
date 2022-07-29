using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerOrgan : MonoBehaviour
{
    //Color colorBegin;
    //Color colorEnd;
    public bool _click;
    //float scaleChangeSize;
    Image originalImage;
    Image endImage;


    FlowerOrgan myStatus;

    void Awake()
    {
        _click = false;
        //scaleChangeSize = .4f;
        //colorBegin = GetComponent<SpriteRenderer>().color;
        //colorEnd = new Color(colorBegin.r, colorBegin.g, colorBegin.b, .30f);
        //originalImage.color = gameObject.GetComponent<Image>().color;
        //endImage.color = new Color(originalImage.color.r, originalImage.color.g, originalImage.color.b, .2f);
    }

    private void Start()
    {
        
    }

    public void Update()
    {
        
    }

    public void OnClick()
    {
        if(_click == false)
        {
            statusChange(true);
        }else if (_click == true)
        {
            statusChange(false);
        }
    }

    public void statusChange(bool down)
    {
        Debug.Log(down);
        if(down == true)
        {
            //gameObject.GetComponent<SpriteRenderer>().color = colorEnd;
            this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(70F, 70F);
            //gameObject.GetComponent<Image>().color = endImage.color;
            _click = true;
        }else if (down == false)
        {
            //gameObject.GetComponent<SpriteRenderer>().color = colorBegin;
            this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(100F, 100F);
            //gameObject.GetComponent<Image>().color = originalImage.color;
            _click = false;
        }
        Debug.Log("我在這");
    }


}
