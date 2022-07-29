using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialouge_System : MonoBehaviour
{
    [Header("UI組件")]
    public Text textLabel;
    public Text optionA;
    public Text optionB;
    public Text optionC;

    [Header("文本文件")]
    public TextAsset textFile;
    int index;
    public float textSpeed;

    bool textFinished;

    List<string> textList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        textFinished = true;
        getTextFromFile(textFile);
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }

        //按下R鍵循環撥放文本
        if (Input.GetKeyDown(KeyCode.R) && textFinished == true)
        {
            //textLabel.text = textList[index];
            //index += 1;
            StartCoroutine(SetTextUI());
        }

        if (Input.GetKeyDown(KeyCode.A) && textFinished == true)
        {
            StartCoroutine(OptionA());
        }
        if (Input.GetKeyDown(KeyCode.B) && textFinished == true)
        {
            StartCoroutine(OptionB());
        }
        if (Input.GetKeyDown(KeyCode.C) && textFinished == true)
        {
            StartCoroutine(OptionC());
        }

    }

    void getTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        //自動定義類型var
        var lineData = file.text.Split('\n');

        //讀取文件
        foreach(var line in lineData)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";
        for (int i = 0; i < textList[index].Length; i++)
        {
            textLabel.text += textList[index][i];
            yield return new WaitForSeconds(textSpeed);
        }
        for (int i = 0; i < textList[2].Length; i++)
        {
            optionA.text += textList[2][i];
            yield return new WaitForSeconds(textSpeed);
        }
        for (int i = 0; i < textList[5].Length; i++)
        {
            optionB.text += textList[5][i];
            yield return new WaitForSeconds(textSpeed);
        }
        for (int i = 0; i < textList[8].Length; i++)
        {
            optionC.text += textList[8][i];
            yield return new WaitForSeconds(textSpeed);
        }

        textFinished = true;
    }

    IEnumerator OptionA()
    {
        textFinished = false;
        textLabel.text = "";
        optionA.text = "";
        optionB.text = "";
        optionC.text = "";
        for (int i = 0; i < textList[3].Length; i++)
        {
            textLabel.text += textList[3][i];
            yield return new WaitForSeconds(textSpeed);
        }
        for (int i = 0; i < textList[15].Length; i++)
        {
            optionA.text += textList[15][i];
            yield return new WaitForSeconds(textSpeed);
        }
        for (int i = 0; i < textList[20].Length; i++)
        {
            optionB.text += textList[20][i];
            yield return new WaitForSeconds(textSpeed);
        }
        for (int i = 0; i < textList[25].Length; i++)
        {
            optionC.text += textList[25][i];
            yield return new WaitForSeconds(textSpeed);
        }

        textFinished = true;
    }

    IEnumerator OptionB()
    {
        textFinished = false;
        textLabel.text = "";
        optionA.text = "";
        optionB.text = "";
        optionC.text = "";
        for (int i = 0; i < textList[6].Length; i++)
        {
            textLabel.text += textList[6][i];
            yield return new WaitForSeconds(textSpeed);
        }

        textFinished = true;
    }

    IEnumerator OptionC()
    {
        textFinished = false;
        textLabel.text = "";
        optionA.text = "";
        optionB.text = "";
        optionC.text = "";
        for (int i = 0; i < textList[9].Length; i++)
        {
            textLabel.text += textList[9][i];
            yield return new WaitForSeconds(textSpeed);
        }

        textFinished = true;
    }


}
