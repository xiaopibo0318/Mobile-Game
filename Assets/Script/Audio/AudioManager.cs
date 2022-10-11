using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //音效素材宣告區--------------------------------------
    [Header("音樂素材庫")]
    private AudioResource audioResource;

    [Header("崑崙山音效")]
    private AudioClip rainAudio;
    private AudioSource walkOnGrass;

    [Header("BGM")]
    private AudioSource nowBGM;

    [Header("環境音效")]
    private AudioSource environmentMusic;

    [Header("按鍵音效")]
    private AudioSource generalButton;
    private AudioSource confirmButton;



    public static AudioManager Instance;
    //音效素材宣告區--------------------------------------


    //程式開始執行區--------------------------------------
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        InitAuudio();
        InitMTKL();
        StartMTKLAudio();
        ButtonEffect();
    }
    //程式開始執行區--------------------------------------

    //初始化函數編輯區------------------------------------
    private void InitAuudio()
    {
        audioResource = GetComponent<AudioResource>();
        nowBGM = gameObject.AddComponent<AudioSource>();
    }
    public void StartMTKLAudio()
    {
        nowBGM.Play();
    }

    public void Walk(bool isWalk = false)
    {
        if (isWalk) walkOnGrass.Play();
        else walkOnGrass.Stop();
    }

    private void InitMTKL()        //走路(草地)音效初始化
    {
        nowBGM.clip = audioResource.bgmMTKL;
        nowBGM.loop = true;
        nowBGM.volume = 0.05f;

        walkOnGrass = gameObject.AddComponent<AudioSource>();
        walkOnGrass.clip = audioResource.walkOnGrass;
        walkOnGrass.loop = false;
        walkOnGrass.volume = 0.1f;
    }

    public void ChangeBgmVolume(float volumeSize)
    {
        nowBGM.volume = volumeSize;
    }


    private void InitEnvironmentMusic() //初始化某音效，此處是環境音效
    {
        environmentMusic = gameObject.AddComponent<AudioSource>();  //先成立一個AudioSource
        environmentMusic.clip = audioResource.NightForest;  //這個AudioSource要用的檔案在這(從已經掛在AudioResource的音樂裡面去選)
        environmentMusic.loop = true; //是否重複播放
        environmentMusic.volume = 0.1f;  //聲音初始值都設為0.1f

        // ChangeEnviormentMusic(audioResource.Forest); //若要變更某環境音效(或其他音效)，則在該腳本呼叫改變的函數
    }
    //已環境音效為例，若今天進入不同階段則要換背景音樂，那就在切換階段的那個腳本裡呼叫下面的ChangeEnviormentMusic函數

    private void ChangeEnviormentMusic(AudioClip nowAudioClip)
    {
        environmentMusic.clip = nowAudioClip;
    }
    //ChangeEnviormentMusic(audioResource.Forest);
    private void ButtonEffect() //初始化登入系統按鍵音效
    {
        generalButton = gameObject.AddComponent<AudioSource>();
        generalButton.clip = audioResource.GeneralButton;
        generalButton.loop = false;
        generalButton.volume = 0.1f;

        confirmButton = gameObject.AddComponent<AudioSource>();
        confirmButton.clip = audioResource.ConfirmButton;
        confirmButton.loop = false;
        confirmButton.volume = 1.0f;
    }

    public void clickButton(string buttonName)
    {
        if (buttonName == "g")
        {
            generalButton.Play();
        }
        else if (buttonName == "c")
        {
            confirmButton.Play();
        }
    }


    //若不需要這個音效了可以把它關掉
    private void DestroyMusic(AudioSource audiosource)
    {
        Destroy(audioResource);
    }

}
