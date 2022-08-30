using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("音樂素材庫")]
    private AudioResource audioResource;

    [Header("崑崙山音效")]
    private AudioSource bgmMTKL;
    private AudioClip rainAudio;
    private AudioSource walkOnGrass;


    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
        InitMTKL();
        StartMTKLAudio();
    }

    public void StartMTKLAudio()
    {
        bgmMTKL.Play();
    }

    public void Walk(bool isWalk = false)
    {
        if (isWalk) walkOnGrass.Play();
        else walkOnGrass.Stop();

    }

    private void InitMTKL()
    {
        audioResource = GetComponent<AudioResource>();

        bgmMTKL = gameObject.AddComponent<AudioSource>();
        bgmMTKL.clip = audioResource.bgmMTKL;
        bgmMTKL.loop = true;
        bgmMTKL.volume = 0.05f;

        walkOnGrass = gameObject.AddComponent<AudioSource>();
        walkOnGrass.clip = audioResource.walkOnGrass;
        walkOnGrass.loop = false;
        walkOnGrass.volume = 0.1f;
    }


}
