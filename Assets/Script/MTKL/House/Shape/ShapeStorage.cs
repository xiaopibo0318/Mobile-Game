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

            switch (shape.shapeID)
            {
                case 0:
                    shape.shapeClass = 301;
                    break;
                case 1:
                    shape.shapeClass = 302;
                    break;
                case 2:
                    shape.shapeClass = 303;
                    break;
                case 3:
                case 4:
                    shape.shapeClass = 311;
                    break;
                case 5:
                case 6:
                    shape.shapeClass = 321;
                    break;
                case 8:
                case 9:
                case 10:
                case 7:
                    shape.shapeClass = 331;
                    break;
                case 12:
                case 13:
                case 14:
                case 11:
                    shape.shapeClass = 341;
                    break;
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 15:
                    shape.shapeClass = 351;
                    break;
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 23:
                    shape.shapeClass = 361;
                    break;
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 31:
                    shape.shapeClass = 371;
                    break;
                default:
                    shape.shapeClass = -99;
                    break;

            }
        }


    }

    public Shape GetCurrentSelectedSquare()
    {
        foreach (var shape in shapeList)
        {

            if (shape.IsOnStartPosition() == false && shape.IsAnyOfShapeSquareActive())
            {
                Debug.Log(shape.IsOnStartPosition());
                return shape;
            }
        }
        Debug.Log("沒有方塊被選");
        return null;
    }

}
