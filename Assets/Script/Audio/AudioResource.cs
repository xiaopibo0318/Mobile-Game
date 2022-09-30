using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioResource : MonoBehaviour
{
    [Header("BGM"),SerializeField]
    public AudioClip bgmMTKL;

    [Header("環境音效"), SerializeField]
    public AudioClip rain;
    public AudioClip WaterDrop;
    public AudioClip NightForest;
    public AudioClip Forest;
    public AudioClip ChangeState;

    [Header("遊戲音效"), SerializeField]
    public AudioClip Salvage;
    public AudioClip Countdown1;
    public AudioClip Countdown2;
    public AudioClip OpenDoor;
    public AudioClip LotusButton;

    [Header("玩家音效"), SerializeField]
    public AudioClip walkOnGrass;
    public AudioClip WalkIndoor;
    public AudioClip WalkOnWater;

    [Header("UI音效"), SerializeField]
    public AudioClip GeneralButton;
    public AudioClip ConfirmButton;
    public AudioClip PopUp;
    
    
   
    
    
   
    

}
