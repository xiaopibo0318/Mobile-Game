using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquare : MonoBehaviour
{
    public Image hooverImage;
    public Image activeImage;
    public Image normalImage;

    public bool Selected { get; set; }
    public int SquareIndex { get; set; }
    public bool SquareOccupied { get; set; }

    public void Start()
    {
        Selected = false;
        SquareOccupied = false;
    }

    public bool CanWeUseThisSquare()
    {
        return hooverImage.gameObject.activeSelf;
    }

    public void PlaceShapeOnBoard()
    {
        ActivateSquare();

    }


    public void ActivateSquare()
    {
        hooverImage.gameObject.SetActive(false);
        activeImage.gameObject.SetActive(true);
        Selected = true;
        SquareOccupied = true;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(SquareOccupied == false)
        {
            Selected = true;
            hooverImage.gameObject.SetActive(true);
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Selected = true;
        if(SquareOccupied == false)
        {
            hooverImage.gameObject.SetActive(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(SquareOccupied == false)
        {
            Selected = false;
            hooverImage.gameObject.SetActive(false);
        }
       
    }


}
