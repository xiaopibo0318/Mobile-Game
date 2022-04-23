using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStyleManager : MonoBehaviour
{
    [SerializeField] GameObject[] PlayerStyle;

    public static PlayerStyleManager Instance;

    int mystyle;
    // Start is called before the first frame update
    void Start()
    {
        mystyle = PlayerPrefs.GetInt("myStyle");
        Instance = this;
        Debug.Log(mystyle);
        WhichAmI(mystyle);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void WhichAmI(int n)
    {
        Debug.Log(n);
        
        for(int i=0; i< 2; i++)
        {
            if(PlayerStyle[i].gameObject == true)
            {
                PlayerStyle[i].SetActive(false);
            }
            
        }
        
        switch (n)
        {
            case 0:
                PlayerStyle[0].SetActive(true);
                break;

            case 1:
                PlayerStyle[1].SetActive(true);
                break;

            case 2:
                PlayerStyle[2].SetActive(true);
                break;
        }
    }
}
