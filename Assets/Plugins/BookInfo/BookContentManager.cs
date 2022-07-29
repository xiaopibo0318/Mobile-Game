using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookContentManager : MonoBehaviour
{

    public List<GameObject> myKnowledge = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < myKnowledge.Count; i++)
        {
                myKnowledge[i].SetActive(false);
        }
    }

    public void Onclick(string name)
    {
        for (int i = 0; i< myKnowledge.Count; i++)
        {
            if(name == myKnowledge[i].name)
            {
                myKnowledge[i].SetActive(true);
            }
            else
            {
                myKnowledge[i].SetActive(false);
            }
        }
    }

}
