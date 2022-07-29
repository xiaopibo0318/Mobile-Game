using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIntroduceMananger : MonoBehaviour
{
    [SerializeField] IamItem[] myItem;


    public static ItemIntroduceMananger Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            //Destroy(gameObject);
        }


        for (int i = 0; i < myItem.Length; i++)
        {
            myItem[i].Close();
        }
    }

    public void openWhich(int myID)
    {
        for (int i = 0; i < myItem.Length; i++)
        {
            if(myItem[i].itemID == myID)
            {
                OpenIntroduce(myItem[i]);
            }else if (myItem[i].open)
            {
                CloseIntrodice(myItem[i]);
            }
        }
    }

    void OpenIntroduce(IamItem _name)
    {
        for (int i = 0; i < myItem.Length; i++)
        {
            if (myItem[i].open)
            {
                CloseIntrodice(_name);
            }
        }
        _name.Open();
    }

    void CloseIntrodice(IamItem _name)
    {
        _name.Close();
    }


}
