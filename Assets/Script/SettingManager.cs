using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    [Header("設定UI")]
    [SerializeField] private GameObject settingObject;

    [Header("音樂")]
    [SerializeField] private Slider musicSlider;

    [Header("存檔")]
    [SerializeField] private Button dataStorageButton; //記得+onClick

    [Header("讀檔")]
    [SerializeField] private Button dataLoadButton;    //記得+onClick

    [Header("退出遊戲")]
    [SerializeField] private Button leaveGameButton;

    [Header("其他功能")]
    [SerializeField] private Button goBack;

    [Header("隱藏功能")]
    [SerializeField] private Button hiddenButton;
    [SerializeField] private InputField gameStatusInputField;
    [SerializeField] private Button sendHiddenButton;
    private int hiddenNum = 0;
    private string inputGameStatus;

    public static SettingManager Instance;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);

        musicSlider.value = 0.05f;
        musicSlider.onValueChanged.AddListener(OnChangeValue);
        goBack.onClick.AddListener(CloseSetting);

        leaveGameButton.onClick.AddListener(QuitGame);


        dataStorageButton.onClick.AddListener(() =>
        {
            SaveLoadSystem.Instance.Save();
        });

        dataLoadButton.onClick.AddListener(() =>
        {
            SaveLoadSystem.Instance.Load();
        });

        hiddenButton.onClick.AddListener(OnClickHiddenButton);
        sendHiddenButton.onClick.AddListener(ChangeGameStatus);
        hiddenButton.gameObject.SetActive(true);
        gameStatusInputField.gameObject.SetActive(false);
        sendHiddenButton.gameObject.SetActive(false);

    }


    private void OnClickHiddenButton()
    {
        hiddenNum++;
        if (hiddenNum > 5)
        {
            SiginalUI.Instance.SiginalText("進入開發系統");
            gameStatusInputField.gameObject.SetActive(true);
            sendHiddenButton.gameObject.SetActive(true);
        }
    }

    private void ChangeGameStatus()
    {
        inputGameStatus = gameStatusInputField.text;
        GDMananger.Instance.gameStatus = int.Parse(inputGameStatus);
        GDMananger.Instance.UpdateMap();
    }

    public void OpenSetting()
    {
        settingObject.SetActive(true);
    }


    private void OnChangeValue(float musicValue)
    {
        AudioManager.Instance.ChangeBgmVolume(musicValue);
    }

    private void CloseSetting()
    {
        settingObject.SetActive(false);

        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            CanvasManager.Instance.openCanvas("original");
        }

    }

    private void QuitGame()
    {
        GameCenter.Instance.QuitGame();
    }





}
