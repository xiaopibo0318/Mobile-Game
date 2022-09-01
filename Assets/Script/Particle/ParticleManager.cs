using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [Header("木屑粒子效果")]
    [SerializeField] private List<ParticleSystem> woodParticle = new List<ParticleSystem>();
    [Header("傳送陣粒子效果")]
    [SerializeField] private ParticleSystem teleportParticle ;

    //整個場景只有一個腳本掛在物件上
    public static ParticleManager Instance;

    private void Awake()
    {
        Instance = this;
        Init();
    }

    private void Init()
    {
        teleportParticle.gameObject.SetActive(true);
    }

    public void DisplayTeleport()
    {
        //加速動作
    }

    public void DisplayWoodParticle(Vector3 nowPos)
    {
        for(int i = 0; i < woodParticle.Count; i++)
        {
            woodParticle[i].transform.position = nowPos;
        }
    }

}
