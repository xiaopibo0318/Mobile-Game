using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogueManageGD : Singleton<DialogueManageGD>
{
    public Button backButton;
    public Text mainText;
    public Button[] options;
    public TextAsset inkAsset;
    Story story = null;
    private string nowEvent = "";


    private void Start()
    {
        backButton.onClick.AddListener(delegate { CanvasManager.Instance.openCanvas("original"); });
        for (int i = 0; i < options.Length; i++)
        {
            int index = i;
            options[i].onClick.AddListener(delegate () { MakeChoice(index); });
        }
        //options[0].onClick.AddListener(delegate () { MakeChoice(0); });
        //options[1].onClick.AddListener(delegate () { MakeChoice(1); });
        //options[2].onClick.AddListener(delegate () { MakeChoice(2); });
        StartDialogue();
    }

    public bool StartDialogue()
    {
        if (story != null) return false;
        story = new Story(inkAsset.text);
        story.variablesState["gameStatus"] = Player.Instance.myStatus.GetGameStatus();
        //story.variablesState["chatStatus"] = 1;
        NextDialog();
        return true;
    }

    public void ChangeChatStatus()
    {
        if (story == null) return;
        var gameStatus = (int)story.variablesState["gameStatus"];
        //var chatStatus = (int)story.variablesState["chatStatus"];

        Debug.Log($"Logging ink variables. 遊戲狀態: {gameStatus}");
        //Debug.Log(Player.Instance.myStatus.GetGameStatus());
        if (Player.Instance.myStatus.GetGameStatus() ==1)
        {
            story.variablesState["gameStatus"] = 1;
        }

        else if (Player.Instance.myStatus.GetGameStatus() == 2)
        {
            story.variablesState["gameStatus"] = 2;

        }
        else if (Player.Instance.myStatus.GetGameStatus() == 4)
        {
            story.variablesState["gameStatus"] = 3;

        }
        else
        {
            story.variablesState["gameStatus"] = 999;
        }
        gameStatus = (int)story.variablesState["gameStatus"];
        //chatStatus = (int)story.variablesState["chatStatus"];
        Debug.Log($"Logging ink variables. 遊戲狀態: {gameStatus}");

    }


    public void NextDialog()
    {
        if (story == null) return;
        if (!story.canContinue && story.currentChoices.Count == 0)
        { //如果story不能繼續 && 沒有選項，則代表對話結束
            Debug.Log("Dialog End");
            story = null;
            return;
        }
        if (story.currentChoices.Count > 0) SetChoices(); //取得目前對話選項數量，如果 > 0 則設定選項按鈕
        if (story.canContinue) mainText.text = story.Continue(); //如果可以繼續下一句對話，執行 story.Continue()
    }

    private void SetChoices()
    { //依照選項數量，設置按鈕 文字 及 Active
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            options[i].gameObject.SetActive(true);
            options[i].GetComponentInChildren<Text>().text = story.currentChoices[i].text;
        }
    }

    public void MakeChoice(int index)
    {
        nowEvent = "";
        Debug.Log("現在的Index:" + index);
        story.ChooseChoiceIndex(index); //使用 ChooseChoiceIndex 選擇當前選項
        for (int i = 0; i < options.Length; i++)
        { //選擇完，將按鈕隱藏
            options[i].gameObject.SetActive(false);
        }
        NextDialog();
        nowEvent = (string)story.variablesState["nowEvent"];
        MakeSomeEvent(nowEvent);
    }

    private void MakeSomeEvent(string nowEvent)
    {
        switch (nowEvent)
        {
            case "lotus1":
                SiginalUI.Instance.SiginalText("某顆石頭上或許\n會有你想要的訊息", 5);
                break;
            default:
                break;
        }
    }
}
