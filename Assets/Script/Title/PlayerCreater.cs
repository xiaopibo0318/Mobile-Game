using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreater : MonoBehaviour
{
    [SerializeField] GameObject Haircolor_Purple;
    [SerializeField] GameObject Haircolor_Brown;
    [SerializeField] GameObject Haircolor_Black;
    

    public static PlayerCreater Instance;

    int a;
    
    public GameObject[] PlayerStyle;
    void Awake()
    {
        a = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClickRightButton_HairColor(int k)
    {
        k = a +1;
        //Debug.Log(k);
        HairColorSwitchter(k);
 
    }

    public void OnClickLeftButton_HairColor(int k)
    {
        k = a - 1;
        //Debug.Log(k);
        HairColorSwitchter(k);

    }

    void HairColorSwitchter(int m)
    {
        Debug.Log(m);
        if (m % 3 == 0)
        {
            Haircolor_Brown.SetActive(true);
            Haircolor_Black.SetActive(false);
            Haircolor_Purple.SetActive(false);

            PlayerStyle[0].SetActive(true);
            PlayerStyle[1].SetActive(false);
            PlayerStyle[2].SetActive(false);


        }
        else if (m % 3 == 1)
        {
            Haircolor_Brown.SetActive(false);
            Haircolor_Black.SetActive(true);
            Haircolor_Purple.SetActive(false);

            PlayerStyle[0].SetActive(false);
            PlayerStyle[1].SetActive(true);
            PlayerStyle[2].SetActive(false);

 
        }
        else
        {
            Haircolor_Brown.SetActive(false);
            Haircolor_Black.SetActive(false);
            Haircolor_Purple.SetActive(true);

            PlayerStyle[0].SetActive(false);
            PlayerStyle[1].SetActive(false);
            PlayerStyle[2].SetActive(true);

            
        }
        a = m;
    }

    public void ConfirmWhichAmI(int k)
    {
        
        k = a % 3;
        Debug.Log(k);
        PlayerPrefs.SetInt("myStyle", k);
        TitleManager.Instance.EnteringRoomList();
    }

}
