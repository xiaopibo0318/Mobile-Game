using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("音樂素材庫")]
    private AudioResource audioResource;

    [Header("崑崙山音效")]
    private AudioClip rainAudio;
    private AudioSource WalkOnGrass;

    [Header("BGM")]
    private AudioSource nowBGM;

    [Header("環境音效")]
    private AudioSource environmentMusic;


    public static AudioManager Instance;

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
    }


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
        if (isWalk) WalkOnGrass.Play();
        else WalkOnGrass.Stop();

    }

    private void InitMTKL()        //
    {
        nowBGM.clip = audioResource.bgmMTKL;
        nowBGM.loop = true;
        nowBGM.volume = 0.05f;

        WalkOnGrass = gameObject.AddComponent<AudioSource>();
        WalkOnGrass.clip = audioResource.walkOnGrass;
        WalkOnGrass.loop = false;
        WalkOnGrass.volume = 0.1f;
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


    //若不需要這個音效了可以把它關掉
    private void DestroyMusic(AudioSource audiosource)
    {
        Destroy(audioResource);
    }
    
}
