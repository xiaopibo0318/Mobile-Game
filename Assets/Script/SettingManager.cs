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
        
        if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            CanvasManager.Instance.openCanvas("original");
        }

    }

    private void QuitGame()
    {
        GameCenter.Instance.QuitGame();
    }





}
