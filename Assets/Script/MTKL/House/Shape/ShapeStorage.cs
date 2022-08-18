using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeStorage : MonoBehaviour
{
    public List<ShapeData> shapeData;
    public List<Shape> shapeList;

    private void Start()
    {
        var shapeIndex = 0;
        //int i=0
        foreach (var shape in shapeList)
        { 
            //var shapeIndex = UnityEngine.Random.Range(0, shapeData.Count); //隨機挑選
            shape.CreateShape(shapeData[shapeIndex]);
            shape.shapeID = shapeIndex;
            shapeIndex++;
        }
        
    }

    public Shape GetCurrentSelectedSquare()
    {
        foreach (var shape in shapeList)
        {
            
            if(shape.IsOnStartPosition() == false && shape.IsAnyOfShapeSquareActive())
            {
                Debug.Log(shape.IsOnStartPosition());
                return shape;
            }
        }
        Debug.Log("沒有方塊被選");
        return null;
    }

}
