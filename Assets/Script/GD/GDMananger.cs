using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GDMananger : Singleton<GDMananger>
{
    public int gameStatus { get; set; }

    [Header("門設備")]
    [SerializeField] private Transform firstDoor;
    [SerializeField] private Transform secondDoor;
    [SerializeField] private Transform thirdDoor;
    [SerializeField] private Transform elevatorDoor;
    [SerializeField] private SpriteRenderer hiddenDoor;
    [SerializeField] private GameObject lastLevelObject;

    [Header("紅外線設備")]
    [SerializeField] private List<Transform> raycastList;
    [Header("傳送陣物件")]
    [SerializeField] private Transform teleportObject1;


    [SerializeField] private Inventory myBag;

    private Coroutine coroutine;

    private void OnEnable()
    {
        Camerafollowww.Instance.UpdateNowSceneBuildIndex();
        gameStatus = 1;
        LoadMap();
        TimeCounter.Instance.StartCountDown();
        //DialogueManageGD.Instance.ChangeChatStatus(); //這行隔壁在start才初始化，因此不能用
        //ClearBag();
    }


    //private void ClearBag()
    //{
    //    for (int i = 0; i < myBag.itemList.Count; i++)
    //    {
    //        myBag.itemList[i].itemHave = 0;
    //    }
    //    InventoryManager.Instance.RefreshFromExternal();
    //    myBag.itemList.Clear();
    //}


    public void UpdateMap()
    {
        Player.Instance.myStatus.gameStatus = gameStatus;
        DialogueManageGD.Instance.ChangeChatStatus();
        LoadMap();
    }


    public void TriggerHiddenDoor()
    {
        hiddenDoor.color = new Vector4(255, 255, 255, 1);
        lastLevelObject.SetActive(true);
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
                teleportObject1.gameObject.SetActive(false);
                hiddenDoor.color = new Vector4(255, 255, 255, 0);
                lastLevelObject.SetActive(false);
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
            case 9:
                hiddenDoor.gameObject.SetActive(false);
                break;

        }
    }



    public void LookStory() => coroutine = StartCoroutine(StartStory());



    IEnumerator StartStory()
    {
        float time = 12.1f;
        SiginalUI.Instance.SiginalText("在神農氏22年，女媧所在的宮殿，附近村落的村民懷疑女媧表裡不一、並不是個和藹可親的領導者，", 12, 45);
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        time = 15.1f;
        SiginalUI.Instance.SiginalText("反而背後暗藏著黑暗的秘密，身為探險者的你，將前往宮殿一探究竟，一路上會有許多難題，一一破解後帶著你們的發現回報鳳凰吧！", 15, 40);
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        time = 10.1f;
        SiginalUI.Instance.SiginalText("1.宮殿的主人女媧，據聞是個表裡不一的人，外表看起來和藹可親， 事實上是個最毒婦人心", 10, 45);
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        time = 7.1f;
        SiginalUI.Instance.SiginalText("2. 在宮殿中，你需要修復、解除一些裝置來達到某些目的", 7);
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        SiginalUI.Instance.SiginalText("3. 鳳凰為了協助探險者們探索，分裂了其中一個靈體到宮殿當中，在探索中如果遇到難以解決的情況，可以向祂尋求幫助！", 12, 40);
    }
}
