using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class titleAudioEffect : MonoBehaviour
{
    public Button startGameButton;
    public Button loginButton;
    public Button goToRegister;
    public Button submitButton;
    public Button bactToLogin;
    public Button nextHair;
    public Button lastHair;
    public Button confirmCharacter;
    public Button selectMapKL;
    public Button selectMapLZ;
    public Button selectMapGD;
    public Button startGame;



    [System.Obsolete]
    void Start()
    {
        startGameButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.clickButton("g");
        });
        loginButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.clickButton("g");
        });
        goToRegister.onClick.AddListener(() =>
        {
            AudioManager.Instance.clickButton("g");
        });
        submitButton.onClick.AddListener(() =>
         {
             AudioManager.Instance.clickButton("g");
         });
        bactToLogin.onClick.AddListener(() =>
         {
             AudioManager.Instance.clickButton("g");
         });
        lastHair.onClick.AddListener(() =>
        {
            AudioManager.Instance.clickButton("g");
        });
        nextHair.onClick.AddListener(() =>
        {
            AudioManager.Instance.clickButton("g");
        });
        confirmCharacter.onClick.AddListener(() =>
        {
            AudioManager.Instance.clickButton("g");
        });
        selectMapKL.onClick.AddListener(() =>
        {
            AudioManager.Instance.clickButton("g");
        });
        selectMapLZ.onClick.AddListener(() =>
        {
            AudioManager.Instance.clickButton("g");
        });
        selectMapGD.onClick.AddListener(() =>
        {
            AudioManager.Instance.clickButton("g");
        });
        /*startGame.onClick.AddListener(() =>
        {
            Debug.Log("123456");
            AudioManager.Instance.clickButton("c");
        });*/   //確認關卡的音效加在TitleManager裡面
    }
}
