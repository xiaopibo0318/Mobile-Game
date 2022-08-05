using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    public ShapeStorage shapeStorage;

    public List<GameObject> _girdSquares;

    private void Awake()
    {
        //幫每個格子上編號
        for(int i=0; i < _girdSquares.Count; i++)
        {
            _girdSquares[i].GetComponent<GridSquare>().SquareIndex = i;
        }
        
    }

    private void OnEnable()
    {
        GameEvent.CheckIfShapeCanBePlaced += CheckIfShapeCanBePlaced;
    }


    private void OnDisable()
    {
        GameEvent.CheckIfShapeCanBePlaced -= CheckIfShapeCanBePlaced;
    }

    
    private void CheckIfShapeCanBePlaced()
    {
        var equalIndexs = new List<int>();

        foreach (var square in _girdSquares)
        {
            var gridSquare = square.GetComponent<GridSquare>();

            if(gridSquare.Selected && !gridSquare.SquareOccupied)
            {
                equalIndexs.Add(gridSquare.SquareIndex);
                gridSquare.Selected = false;
                //gridSquare.ActivateSquare();
            }
        }

        var currentSelectedShape = shapeStorage.GetCurrentSelectedSquare();
        if (currentSelectedShape == null) return;

        if(currentSelectedShape.totalSquareNumber == equalIndexs.Count)
        {
            
            foreach (var squareIndex in equalIndexs)
            {
                Debug.Log("我的" + squareIndex);
                _girdSquares[squareIndex].GetComponent<GridSquare>().PlaceShapeOnBoard();
            }
            currentSelectedShape.DeactiveShape();
        }
        else
        {
            GameEvent.MoveShapeToStartPosition();
        }
        


        //shapeStorage.GetCurrentSelectedSquare().DeactiveShape();




    }



}
