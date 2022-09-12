using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("其他功能")]
    [SerializeField] private Button goBack;

    [Header("開啟設定的按鈕")]
    [SerializeField] private Button openSettingOnTitle;

    public static SettingManager Instance;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);

        musicSlider.value = 0.05f;
        musicSlider.onValueChanged.AddListener(OnChangeValue);
        goBack.onClick.AddListener(CloseSetting);
        
        //openSettingOnTitle.onClick.AddListener(OpenSetting);

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
        CanvasManager.Instance.openCanvas("original");

    }










}
