using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MtKL1AudioEffect : MonoBehaviour
{
    [Header("遊戲主介面")]
    public Button backPackButton;
    public Button bookButton;
    public Button exitBook;
    

    [Header("蓮花")]
    public Button topButton;
    public Button rightTop;
    public Button rightButton;
    public Button rightDown;
    public Button downButton;
    public Button leftDown;
    public Button leftButton;
    public Button leftTop;
    public Button exitLotus;







    [System.Obsolete]
    void Start()
    {
        //一般UI音效----------------------------------
        backPackButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.clickButton("g");
        });
        bookButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.clickButton("g");
        });
        exitBook.onClick.AddListener(() =>
        {
            AudioManager.Instance.clickButton("g");
        });
       
        //------------------------------------------------

        //蓮花相關音效
        topButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.lotus("button");
        });
        rightTop.onClick.AddListener(() =>
        {
            AudioManager.Instance.lotus("button");
        });
        rightButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.lotus("button");
        });
        rightDown.onClick.AddListener(() =>
        {
            AudioManager.Instance.lotus("button");
        });
        downButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.lotus("button");
        });
        leftDown.onClick.AddListener(() =>
        {
            AudioManager.Instance.lotus("button");
        });
        leftButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.lotus("button");
        });
        leftTop.onClick.AddListener(() =>
        {
            AudioManager.Instance.lotus("button");
        });
        exitLotus.onClick.AddListener(() =>
        {
            AudioManager.Instance.lotus("exit");
        });
    }

}