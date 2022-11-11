using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioResource : MonoBehaviour
{
    [Header("BGM"),SerializeField]
    public AudioClip bgmMTKL;
    public AudioClip bgmGD1;
    public AudioClip bgmGD2;

    [Header("環境音效"), SerializeField]
    //public AudioClip rain;
    public AudioClip NightForest;          //夜晚森林環境音
    public AudioClip Forest;               //森林環境音
    

    [Header("遊戲音效"), SerializeField]
    public AudioClip Salvage;             //水中打撈音效
    public AudioClip Countdown1;          //倒計時音效(沉重)
    public AudioClip Countdown2;          //倒計時音效(輕盈)
    public AudioClip OpenDoor;            //偏房開啟音效
    public AudioClip LotusButton;         //蓮花按鍵音效
    public AudioClip ChangeState;         //階段切換音效
    public AudioClip WaterDrop;           //水滴音效(蓮花開花)
    public AudioClip LotusFailed;         //蓮花失敗音效(金屬撞擊聲)

    [Header("玩家音效"), SerializeField]
    public AudioClip walkOnGrass;         //草地走路音效
    public AudioClip WalkIndoor;          //室內走路音效
    public AudioClip WalkOnWater;         //水上走路音效

    [Header("UI音效"), SerializeField]
    public AudioClip GeneralButton;       //一般按鍵音效
    public AudioClip ConfirmButton;       //確認按鍵音效
    public AudioClip PopUp;               //彈出視窗音效 
    
    
   
    
    
   
    

}
