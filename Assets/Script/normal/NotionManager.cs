using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotionManager : MonoBehaviour
{
    [Header("文字組件")]
    [SerializeField] TextAsset textFile;

    [Header("UI組件")]
    public Text textLabel;
    int index;

    [Header("八卦圖")]
    public GameObject ques;

    List<string> textList = new List<string>();

    public static NotionManager Instance;

    private void Awake()
    {
        Instance = this;
        getTextFromFile(textFile);
        index = 0;
    }


    void getTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        //自動定義類型var
        var lineData = file.text.Split('\n');

        //讀取文件
        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }

    public void setText(int index)
    {
        Debug.Log(textList[index]);
        textLabel.text = "";
        for (int i = index; i < index+10; i++)
        {
            textLabel.text += textList[i];
        }
    }

    public void openQues()
    {
        ques.SetActive(true);
    }

    public void closeQues()
    {
        ques.SetActive(false);
    }

}
