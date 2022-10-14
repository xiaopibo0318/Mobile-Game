using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SiginalUI : MonoBehaviour
{
    public GameObject siginalContent;
    public Text siginalText;
    public GameObject confirm;
    public GameObject dontDo;
    public GameObject panel;

    public static SiginalUI Instance;
    public void Awake()
    {
        Instance = this;
        ResetSiginal();

    }


    public void SiginalText(string myText, int signalTime = 3, int textFontSize = 50)
    {
        siginalContent.SetActive(true);
        siginalText.text = myText;
        siginalText.fontSize = textFontSize;
        panel.SetActive(true);
        StartCoroutine(CloseSignal(signalTime));
    }

    public void TextInterectvie(string myText)
    {
        siginalContent.SetActive(true);
        siginalText.text = myText;
        confirm.SetActive(true);
        dontDo.SetActive(true);
    }


    IEnumerator CloseSignal(float delayTime)
    {
        while (delayTime > 0)
        {
            yield return new WaitForSeconds(1);
            delayTime -= 1;
        }
        ResetSiginal();
    }

    private void ResetSiginal()
    {
        siginalContent.SetActive(false);
        siginalText.text = "";
        confirm.SetActive(false);
        dontDo.SetActive(false);
        panel.SetActive(false);
    }

}
