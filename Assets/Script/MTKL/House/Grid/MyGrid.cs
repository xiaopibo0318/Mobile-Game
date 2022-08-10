using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    public ShapeStorage shapeStorage;

    public List<GameObject> _girdSquares;


    private int[] answer;
    private int[] myAnswer;


    private void Awake()
    {
        answer = new int[9] { 0, 1, 1, 1, 1, 0, 0, 1, 0 };
        myAnswer = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //幫每個格子上編號
        for (int i=0; i < _girdSquares.Count; i++)
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

    public void checkAnswer()
    {
        var myCorrect = 0;
        for (int i =0; i < answer.Length; i++)
        {
            if (_girdSquares[i].GetComponent<GridSquare>().SquareOccupied)
            {
                myAnswer[i] = 1;
            }
            if (myAnswer[i] == answer[i])
            {
                myCorrect += 1;
            }
            Debug.Log(myAnswer[i]);

        }
        Debug.Log("放對幾個" + myCorrect);
        
        if (myCorrect == 9)
        {
            KLMTmanager.Instance.thirdStepKL();
            cacheVisable.Instance.siginalSomething("好像有哪裡產生了一些變化");
        }
        else
        {
            
            Debug.Log("好像哪裡怪怪的");
        }

    }


    public void ClearBoard()
    {
        for(int i= 0 ;i < _girdSquares.Count; i++) 
        {
            var comp = _girdSquares[i].GetComponent<GridSquare>();
            myAnswer[i] = 0;
            comp.DeActivateSquare();
        }
        
    }

}
