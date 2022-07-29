using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodManager : MonoBehaviour
{
    [SerializeField] float SpawnTime;
    [SerializeField] GameObject WoodPrefab;

    float time;

    public void Awake()
    {
        time = SpawnTime;
    }

    public void Update()
    {
        //Debug.Log("Update 上的 生成時間 : " + time);
        time -= Time.deltaTime*100;
        if(time < 0)
        {
            SpawnWood();
            time = SpawnTime;
        }
        
    }

    public void SpawnWood()
    {
        while (true)
        {
            GameObject wood = Instantiate(WoodPrefab, transform);
            wood.transform.position = new Vector3(Random.Range(-5, 20), Random.Range(0, 6), 0);
            break;
        }  
    }

    

}
