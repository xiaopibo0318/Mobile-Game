using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scratchManager : MonoBehaviour
{

    public List<GameObject> blocks = new List<GameObject>();
    public GameObject blockGrid;
    public GameObject emptyBlockSlot;

    public blockList exeLists;

    public GameObject target;
    public GameObject direct;

    // Start is called before the first frame update
    public void OnEnable()
    {
        setUpBlockSlot();
    }


    public void setUpBlockSlot()
    {
        for (int i = 0; i < 7; i++)
        {
            blocks.Add(Instantiate(emptyBlockSlot));
            emptyBlockSlot.name = "blockSlot";
            blocks[i].transform.SetParent(blockGrid.transform);

            blocks[i].GetComponent<blockSlot>().slotID= i;
            
        }
    }


    public void startGame()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
 
        for(int i = 0; i < exeLists.exeList.Count; i++)
        {
            yield return new WaitForSeconds(0.5f);
            switch (exeLists.exeList[i])
            {
                case 0:
                    break;
                case 1:
                    target.transform.position = new Vector3(target.transform.position.x + 100, target.transform.position.y, target.transform.position.z);
                    break;
                case 11:
                    break;
                case 12:
                    direct.transform.position = new Vector3(target.transform.position.x, target.transform.position.y - 35, direct.transform.position.z);
                    direct.transform.rotation = Quaternion.Euler(0, 0, direct.transform.rotation.z -900);
                    break;
                    
            }
        }
    }

    public void ResetBlock()
    {
        for (int i = 0; i < blockGrid.transform.childCount; i++)
        {
            GameObject go = blockGrid.transform.GetChild(i).gameObject;
            Destroy(go);
        }

        setUpBlockSlot();
    }

}
