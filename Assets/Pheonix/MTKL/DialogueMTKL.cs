using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogueMTKL : MonoBehaviour
{
    public Text mainText;
    public Button[] options;
    public TextAsset inkAsset;
    Story story = null;

    public static DialogueMTKL Instance;

    private void Awake()
    {
        Instance = this;

        StartDialogue();
    }

    public bool StartDialogue()
    {
        if (story != null) return false;
        story = new Story(inkAsset.text);
        story.variablesState["gameStatus"] = Player.Instance.myStatus.GetGameStatus();
        story.variablesState["chatStatus"] = 1;
        return true;
    }

    public void ChangeChatStatus()
    {
        var relationshipStrength = (int)story.variablesState["gameStatus"];
        var mentalHealth = (int)story.variablesState["chatStatus"];

        Debug.Log($"Logging ink variables. 遊戲狀態: {relationshipStrength}, 聊天狀態: {mentalHealth}");
        //Debug.Log(Player.Instance.myStatus.GetGameStatus());
        if (Player.Instance.myStatus.GetGameStatus() == 1)
            story.variablesState["chatStatus"] = 1;
        else if ((Player.Instance.myStatus.GetGameStatus() == 12))
        {
            story.variablesState["gameStatus"] = 12;
            story.variablesState["chatStatus"] = 11;
        }

        else if ((Player.Instance.myStatus.GetGameStatus() == 13))
        {
            story.variablesState["gameStatus"] = 13;
            story.variablesState["chatStatus"] = 21;
        }
        else
        {
            story.variablesState["gameStatus"] = 999;
            story.variablesState["chatStatus"] = 999;
        }
        relationshipStrength = (int)story.variablesState["gameStatus"];
        mentalHealth = (int)story.variablesState["chatStatus"];
        Debug.Log($"Logging ink variables. 遊戲狀態: {relationshipStrength}, 聊天狀態: {mentalHealth}");

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
        story.ChooseChoiceIndex(index); //使用 ChooseChoiceIndex 選擇當前選項
        for (int i = 0; i < options.Length; i++)
        { //選擇完，將按鈕隱藏
            options[i].gameObject.SetActive(false);
        }
        NextDialog();
    }

}
