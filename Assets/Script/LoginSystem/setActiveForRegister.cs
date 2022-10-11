using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setActiveForRegister : MonoBehaviour
{
    //public bool activeSelf;
    public Button RegisterButton;
    public Button BackToLoginButton;
    public GameObject LoginPanel;
    public GameObject RegisterPanel;
    
    void Start()
    {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);

        RegisterButton.onClick.AddListener(() =>
        {
            //AudioManager.Instance.clickButton();
            LoginPanel.SetActive(false);
            RegisterPanel.SetActive(true);
        });
        BackToLoginButton.onClick.AddListener(() =>
        {
            //AudioManager.Instance.clickButton();
            LoginPanel.SetActive(true);
            RegisterPanel.SetActive(false);
        });
    }
    /*public void OnClick()
    {
        
        RegisterPanel.SetActive();
    }*/
}
