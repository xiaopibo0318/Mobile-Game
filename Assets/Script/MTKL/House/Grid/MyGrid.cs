using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    public ShapeStorage shapeStorage;

    public List<GameObject> _girdSquares;


    private int[] answer;
    private int[] myAnswer;


    List<Shape> closeList = new List<Shape>();

    private void Awake()
    {
        answer = new int[9] { 0, 1, 1, 1, 1, 0, 0, 1, 0 };
        myAnswer = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //幫每個格子上編號
        for (int i = 0; i < _girdSquares.Count; i++)
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

            if (gridSquare.Selected && !gridSquare.SquareOccupied)
            {
                equalIndexs.Add(gridSquare.SquareIndex);
                gridSquare.Selected = false;
                //gridSquare.ActivateSquare();
            }
        }
        //檢測
        //for (int i = 0; i < equalIndexs.Count; i++)
        //{
        //    Debug.Log(equalIndexs[i]);
        //}

        var currentSelectedShape = shapeStorage.GetCurrentSelectedSquare();
        if (currentSelectedShape == null) return;

        //檢測
        Debug.Log("選擇的方塊有幾格" + currentSelectedShape.totalSquareNumber);
        //Debug.Log("選擇的方塊有X格" + currentSelectedShape.shapeNum);
        //Debug.Log("選擇的方塊有編號" + currentSelectedShape.shapeID);
        //Debug.Log("接觸到了幾個方塊" + equalIndexs.Count);

        if (currentSelectedShape.totalSquareNumber == equalIndexs.Count)
        {

            foreach (var squareIndex in equalIndexs)
            {
                Debug.Log("我的" + squareIndex);
                _girdSquares[squareIndex].GetComponent<GridSquare>().PlaceShapeOnBoard();
            }
            currentSelectedShape.DeactiveShape();
            closeList.Add(currentSelectedShape);
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
        for (int i = 0; i < answer.Length; i++)
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
            SiginalUI.Instance.SiginalText("好像有哪裡產生了一些變化");
        }
        else
        {
            SiginalUI.Instance.SiginalText("好像哪裡怪怪的");
            Debug.Log("好像哪裡怪怪的");
        }

    }


    public void ClearBoard()
    {
        for (int i = 0; i < _girdSquares.Count; i++)
        {
            var comp = _girdSquares[i].GetComponent<GridSquare>();
            myAnswer[i] = 0;
            comp.DeActivateSquare();
        }
        for (int i = 0; i < closeList.Count; i++)
        {
            closeList[i].ActivateShape();
            GameEvent.MoveShapeToStartPosition();
        }
        closeList.Clear();

    }

}
