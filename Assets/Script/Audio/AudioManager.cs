using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("音樂素材庫")]
    private AudioResource audioResource;


    private AudioSource bgmMTKL;
    private AudioClip rainAudio;

    private void Awake()
    {
        audioResource = GetComponent<AudioResource>();

        bgmMTKL = gameObject.AddComponent<AudioSource>();
        bgmMTKL.volume = 0.05f;

        StartMTKLAudio();
    }

    public void StartMTKLAudio()
    {
        bgmMTKL.clip = audioResource.bgmMTKL;
        bgmMTKL.loop = true;
        bgmMTKL.Play();
    }


}
