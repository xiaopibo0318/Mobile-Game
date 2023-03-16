using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : Singleton<EndGameManager>
{
    [SerializeField] private GameObject KLObject;
    [SerializeField] private GameObject GDObject;
    [SerializeField] private Text KLText;
    [SerializeField] private Text GDText;

    private int roomIndex = -99;
    private string nowTime = "";

    private void Start()
    {
        KLObject.SetActive(false);
        GDObject.SetActive(false);
    }


    public void ChangeRoomState(int _roomIndex, string time)
    {
        roomIndex = _roomIndex;
        nowTime = time;
    }

    public void UpdateRoomState()
    {
        if (roomIndex == 2)
        {
            KLObject.SetActive(true);
            KLText.text = "已破關\n" + nowTime;
        }
        else if (roomIndex == 4)
        {
            GDObject.SetActive(true);
            GDText.text = "已破關\n" + nowTime;
        }
    }

}
