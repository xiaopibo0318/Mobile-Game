using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public InputField EmailInput;
    public Button SubmitButton;

    [System.Obsolete]
    void Start()
    {
        SubmitButton.onClick.AddListener(() =>
        {
            //AudioManager.Instance.clickButton();
            StartCoroutine(Main.Instance.Web.RegisterUser(UsernameInput.text, PasswordInput.text , EmailInput.text));
        });
    }


}
