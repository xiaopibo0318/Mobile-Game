using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDMananger : Singleton<GDMananger>
{
    public int gameStatus { get; set; }

    [Header("門設備")]
    [SerializeField] private Transform firstDoor;
    [SerializeField] private Transform secondDoor;
    [SerializeField] private Transform thirdDoor;
    [SerializeField] private Transform elevatorDoor;

    [Header("紅外線設備")]
    [SerializeField] private List<Transform> raycastList;
    [Header("傳送陣物件")]
    [SerializeField] private Transform teleportObject1;

    private void OnEnable()
    {
        Camerafollowww.Instance.UpdateNowSceneBuildIndex();
        gameStatus = 1;
        LoadMap();
        TimeCounter.Instance.StartCountDown();
        //DialogueManageGD.Instance.ChangeChatStatus(); //這行隔壁在start才初始化，因此不能用
    }

    public void UpdateMap()
    {
        Player.Instance.myStatus.gameStatus = gameStatus;
        LoadMap();
    }


    private void LoadMap()
    {

        switch (gameStatus)
        {
            case 1:
                firstDoor.gameObject.SetActive(true);
                secondDoor.gameObject.SetActive(true);
                thirdDoor.gameObject.SetActive(true);
                elevatorDoor.gameObject.SetActive(true);
                for (int i = 0; i < raycastList.Count; i++)
                {
                    raycastList[i].gameObject.SetActive(true);
                }
                teleportObject1.gameObject.SetActive(true);
                break;
            case 2:
                firstDoor.gameObject.SetActive(false);
                secondDoor.gameObject.SetActive(true);
                thirdDoor.gameObject.SetActive(true);
                elevatorDoor.gameObject.SetActive(true);
                break;
            case 3:
                firstDoor.gameObject.SetActive(false);
                secondDoor.gameObject.SetActive(false);
                thirdDoor.gameObject.SetActive(true);
                elevatorDoor.gameObject.SetActive(true);
                break;
            case 4:
                firstDoor.gameObject.SetActive(false);
                secondDoor.gameObject.SetActive(false);
                thirdDoor.gameObject.SetActive(false);
                elevatorDoor.gameObject.SetActive(true);
                break;
            case 5:
                firstDoor.gameObject.SetActive(false);
                secondDoor.gameObject.SetActive(false);
                thirdDoor.gameObject.SetActive(false);
                elevatorDoor.gameObject.SetActive(false);
                break;
            case 6:
                teleportObject1.gameObject.SetActive(true);
                break;
            case 7:
                raycastList[0].gameObject.SetActive(false);
                raycastList[2].gameObject.SetActive(false);
                break;
            case 8:
                raycastList[1].gameObject.SetActive(false);
                raycastList[3].gameObject.SetActive(false);
                break;

        }
    }


}
