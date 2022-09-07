using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("音樂素材庫")]
    private AudioResource audioResource;

    [Header("崑崙山音效")]
    private AudioClip rainAudio;
    private AudioSource walkOnGrass;

    [Header("BGM")]
    private AudioSource nowBGM;


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
        if (isWalk) walkOnGrass.Play();
        else walkOnGrass.Stop();

    }

    private void InitMTKL()
    {
        nowBGM.clip = audioResource.bgmMTKL;
        nowBGM.loop = true;
        nowBGM.volume = 0.05f;

        walkOnGrass = gameObject.AddComponent<AudioSource>();
        walkOnGrass.clip = audioResource.walkOnGrass;
        walkOnGrass.loop = false;
        walkOnGrass.volume = 0.1f;
    }

    public void ChangeBgmVolume( float volumeSize)
    {
        nowBGM.volume = volumeSize;
    }

}
