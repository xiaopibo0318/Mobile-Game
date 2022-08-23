using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodManager : MonoBehaviour
{
    private float SpawnTime;
    [SerializeField] GameObject WoodPrefab;
    [SerializeField] float lifetime;
    private float time;
    private int woodIndex = 0;
    private int nowWoodID;

    [SerializeField] List<GameObject> woodOnGroundList = new List<GameObject>();

    public static WoodManager Instance;

    public void Awake()
    {
        SpawnTime = 1000;
        time = SpawnTime;
        
        lifetime = 40;
        Instance = this;
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
        //將Missing的全部刪除掉
        woodOnGroundList.RemoveAll(GameObject => GameObject == null); 
        GameObject wood = Instantiate(WoodPrefab, transform);
        wood.transform.position = new Vector3(Random.Range(-5, 20), Random.Range(0, 6), 0);
        woodOnGroundList.Add(wood);
        RefreshWoodID();
        Destroy(wood, lifetime);

    }

    public void RefreshWoodID()
    {
        //將Missing的全部刪除掉
        woodOnGroundList.RemoveAll(GameObject => GameObject == null);

        foreach (var wood in woodOnGroundList)
        {
            woodIndex = woodOnGroundList.IndexOf(wood);
            wood.GetComponent<Wood>().woodID = woodIndex;
        }
    }




    public void ChangeNowWood(int n)
    {
        nowWoodID = n;
    }

    public void DestroyNowWood()
    {
        foreach (var woodOnGround in woodOnGroundList)
        {
            if (woodOnGround == null) continue;

            if(woodOnGround.GetComponent<Wood>().woodID == nowWoodID)
            {
                Destroy(woodOnGround);
            }
        }
        RefreshWoodID();
    }


}
