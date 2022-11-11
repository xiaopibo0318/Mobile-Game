using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SiginalUI : MonoBehaviour
{
    public GameObject siginalContent;
    public Text siginalText;
    public Button confirm;
    public Button dontDo;
    public GameObject panel;
    private UnityAction nextAction;
    private Coroutine coroutine;

    public static SiginalUI Instance;
    public void Awake()
    {
        Instance = this;
        ResetSiginal();

    }


    public void SiginalText(string myText, int signalTime = 3, int textFontSize = 50)
    {
        if (coroutine != null) StopCoroutine(coroutine);
        siginalContent.SetActive(true);
        siginalText.text = myText;
        siginalText.fontSize = textFontSize;
        panel.SetActive(true);
        coroutine = StartCoroutine(CloseSignal(signalTime));
    }

    public void TextInterectvie(string myText, UnityAction unityAction1 = null, UnityAction unityAction2 = null)
    {
        siginalContent.SetActive(true);
        siginalText.text = myText;
        confirm.gameObject.SetActive(true);
        dontDo.gameObject.SetActive(true);
        nextAction = unityAction1;
        confirm.onClick.AddListener(delegate { ExecuteOptionYes(nextAction); });
        UnityAction temp = unityAction2;
        dontDo.onClick.AddListener(delegate { ExecuteOptionNo(temp); });
    }


    private void ExecuteOptionYes(UnityAction unityAction = null)
    {
        ResetSiginal();
        unityAction?.Invoke();
        nextAction = null;
    }
    private void ExecuteOptionNo(UnityAction unityAction = null)
    {
        ResetSiginal();
        unityAction?.Invoke();
        nextAction = null;
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
        StopCoroutine(coroutine);
        coroutine = null;
        siginalContent.SetActive(false);
        siginalText.text = "";
        confirm.gameObject.SetActive(false);
        dontDo.gameObject.SetActive(false);
        panel.SetActive(false);
        
    }

}
