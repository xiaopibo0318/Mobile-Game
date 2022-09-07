using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [Header("木屑粒子效果")]
    [SerializeField] private List<ParticleSystem> woodParticle = new List<ParticleSystem>();
    [Header("傳送陣粒子效果")]
    [SerializeField] private List<ParticleSystem> teleportParticle = new List<ParticleSystem>();
    /*[SerializeField] private ParticleSystem teleportParticle ;
    [Header("房間二傳送陣粒子效果")]
    [SerializeField] private ParticleSystem teleportParticle2;*/
    [Header("協程")]

    Coroutine nowCoroutine;

    [Header("Singleton")]
    //整個場景只有一個腳本掛在物件上
    public static ParticleManager Instance;

    private void Awake()
    {
        nowCoroutine = null;
        Instance = this;
        displayTeleport();
    }

    
    public void displayTeleport () //傳送陣初始狀態
    {
        for (int i = 0; i < teleportParticle.Count; i++)
        {
            teleportParticle[i].gameObject.SetActive(true);
        }
        
    }

    public void addSpeedTeleport(int i)
    {
        //粒子特效加速
        nowCoroutine =  StartCoroutine(teleportAddspeed(i));
    }

    public void StopTeleport()
    {
        StopCoroutine(nowCoroutine);
        nowCoroutine = null;
    }


    IEnumerator teleportAddspeed(int i)
    {
        var emission = teleportParticle[i].emission;
        for (int a = 0; a < 10; a++)
        {
            yield return new WaitForSeconds(0.5f);
            teleportParticle[i].startSpeed += 1f;
            emission.rateOverTime = a*10;
        }

        Camerafollowww.Instance.changeMyPos();
        //傳送完畢後回到初始狀態
        teleportParticle[i].startSpeed = 0.2f;
        emission.rateOverTime = 1;
        yield return null;
    }



    public void DisplayWoodParticle(Vector3 nowPos)
    {
        for(int i = 0; i < woodParticle.Count; i++)
        {
            woodParticle[i].transform.position = nowPos;
        }
    }

}
