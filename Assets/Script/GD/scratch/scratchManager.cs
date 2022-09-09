using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scratchManager : MonoBehaviour
{

    public List<GameObject> blocks = new List<GameObject>();
    public GameObject blockGrid;
    public GameObject emptyBlockSlot;

    [Header("執行列表")]
    public blockList exeLists;

    [Header("主角、方向")]
    public GameObject target;
    public GameObject direct;
    private int nowDirect;

    [Header("UI組件")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button ClearButton;
    [SerializeField] private InputField goAheadStep;


    private void Awake()
    {
        startButton.onClick.AddListener(StartGame);
    }

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

            blocks[i].GetComponent<blockSlot>().slotID = i;

        }
    }


    public void StartGame()
    {
        StartCoroutine(GoStartGame());
    }

    IEnumerator GoStartGame()
    {
        ResetObject();
        for (int i = 0; i < exeLists.exeList.Count; i++)
        {
            yield return new WaitForSeconds(0.5f);
            switch (exeLists.exeList[i])
            {
                case 0:
                    break;
                case 1:
                    GoAhead(nowDirect);
                    break;
                case 11:
                    nowDirect += 5; //等於+1
                    nowDirect %= 4;
                    direct.transform.position = DirectPoint.GetArrowPos(nowDirect, target.transform.position);
                    direct.transform.rotation = Quaternion.Euler(0, 0, DirectPoint.GetArrowRotate(nowDirect));
                    break;
                case 12:
                    nowDirect += 3; //等於-1
                    nowDirect %= 4;
                    direct.transform.position = DirectPoint.GetArrowPos(nowDirect, target.transform.position);
                    direct.transform.rotation = Quaternion.Euler(0, 0, DirectPoint.GetArrowRotate(nowDirect));

                    break;
                default:
                    break;


            }
        }
    }

    private void GoAhead(int _nowDirect, int moveStep = 1)
    {
        switch (_nowDirect)
        {
            case 0:
                target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 100 * moveStep, target.transform.position.z);
                break;
            case 1:
                target.transform.position = new Vector3(target.transform.position.x - 100 * moveStep, target.transform.position.y, target.transform.position.z);
                break;
            case 2:
                target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y - 100 * moveStep, target.transform.position.z);
                break;
            case 3:
                target.transform.position = new Vector3(target.transform.position.x + 100 * moveStep, target.transform.position.y, target.transform.position.z);
                break;
            default:
                break;


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

    private void ResetObject()
    {
        target.transform.position = new Vector3(1150, 170, 0);
        nowDirect = 3;
        direct.transform.position = DirectPoint.GetArrowPos(nowDirect, target.transform.position);

    }


}


public static class DirectPoint
{
    /// <summary>
    /// 方向對照表。
    /// 上 0  0
    /// 左 1  90
    /// 下 2  180
    /// 右 3  270
    /// </summary>
    private static Vector3 topPos = new Vector3(0, 60);
    private static Vector3 downPos = new Vector3(0, -60);
    private static Vector3 leftPos = new Vector3(-60, 0);
    private static Vector3 rightPos = new Vector3(60, 0);

    private static Dictionary<int, Vector3> arrowDict = new Dictionary<int, Vector3>()
    {
        {0,topPos }, {1,leftPos},{2,downPos},{3,rightPos}
    };

    private static Dictionary<int, float> arrowRotate = new Dictionary<int, float>()
    {
        {0,0 },{1,90},{2,180},{3,270}
    };

    public static Vector3 GetArrowPos(int myDirect, Vector3 targetPos)
    {
        foreach (var everyArrow in arrowDict)
        {
            if (everyArrow.Key == myDirect)
            {
                return everyArrow.Value + targetPos;
            }
        }
        return new Vector3(0, 0, 0);
    }

    public static float GetArrowRotate(int nowDirect)
    {
        Debug.Log("%360後的數字為" + nowDirect);
        foreach (var nowDegree in arrowRotate)
        {
            if (nowDegree.Key == nowDirect)
            {
                return nowDegree.Value;
            }
        }
        return 0;
    }



}
