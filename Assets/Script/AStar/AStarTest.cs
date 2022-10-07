using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AStarTest : MonoBehaviour, IPointerClickHandler
{
    public int mapW = 8;
    public int mapH = 10;

    public GameObject mySlot;
    public Transform slotParent;

    //開始為負的
    private Vector2 beginPos = Vector2.right * -1;


    private Dictionary<string, GameObject> images = new Dictionary<string, GameObject>();



    private void Start()
    {
        AStarManager.Instance.InitMapInfo(mapW, mapH);

        for (int i = 0; i < mapW; i++)
        {
            for (int j = 0; j < mapH; j++)
            {
                GameObject nowSlot = Instantiate(mySlot, slotParent);
                nowSlot.name = i + "_" + j;
                images.Add(nowSlot.name, nowSlot);

                AStarNode node = AStarManager.Instance.nodes[i, j];

                if (node.type == Node_Type.Stop)
                {
                    nowSlot.GetComponent<Image>().color = new Vector4(255, 0, 0, 255);
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject info = eventData.pointerCurrentRaycast.gameObject;
        //尚未有起點
        if (beginPos == Vector2.right * -1)
        {
            string[] strs = info.name.Split('_');
            beginPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));
            info.GetComponent<Image>().color = new Vector4(0, 255, 0, 255);
        }
        else //有起點了
        {
            //得到終點
            string[] strs = info.name.Split('_');
            Vector2 endPos = new Vector2(int.Parse(strs[0]), int.Parse(strs[1]));

            //尋路
            List<AStarNode> list = AStarManager.Instance.FindPath(beginPos, endPos);
            if (list != null)
            {
                Debug.Log("成功");
                for (int i = 0; i < list.Count; i++)
                {
                    images[list[i].x + "_" + list[i].y].GetComponent<Image>().color = new Vector4(0, 255,0, 255);
                }
            }


        }
    }

}
