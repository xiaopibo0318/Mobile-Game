using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CutWoodManager : MonoBehaviour
{

    [Header("储存路径信息")]
    public List<GameObject> pathStorage;

    [Header("木頭種類信息")]
    public List<Item> woodList;

    [Header("分镜")]
    [SerializeField] private GameObject cuttingWood;
    public GameObject woodChoose;
    public GameObject cutWood;

    [Header("选择要切的木头")]
    public List<GameObject> woodSelectStorage;
    public List<GameObject> woodSimpleStorage;
    List<int> woodIDList = new List<int> { 301, 302, 303, 311, 321, 331, 341, 351, 361, 371, 601 };
    Button buttonCutWoodYes;
    Button buttonCutWoodNo;
    public GameObject siginal;
    int nowWoodID;
    int nowWoodSlotID;
    public Item woodBoard;
    public Inventory myBag;

    [Header("切木頭畫面")]
    Button buttonGoBack;
    float cutTime;
    bool readyToCut;
    

    [Header("時間倒數")]
    public Text timerText;
    float totalTime;
    public float needMin;
    public float needSec;
    bool timeFinished;
    Coroutine timeCoroutine = null;

    [Header("顯示信息")]
    public SiginalUI siginalUI;


    [Header("事件")]
    private UnityAction<PointerEventData> onPointerDown;
    public static CutWoodManager Instance;


    [Header("拉線系統")]
    [SerializeField] private DragItem operateRange;
    [SerializeField] private RectTransform mouseFollower;
    [SerializeField] private UILineRenderer finishedLine;
    [SerializeField] private UILineRenderer previewLine;
    [SerializeField] private GameObject pointOnPath;
    [SerializeField] private RectTransform[] woodTrans;
    private Vector2[] targetPoints;
    private List<Vector2> finishedPoints;
    private Vector2[] previewPoints;

    private int mouseFollowerSize = 30;

    private bool isOperate = false;
    private bool isFinished = false;
    private int startIndex;
    private int currentIndex;
    private int nextIndex;
    private int nextValue;  //計算下一個點是往前還是往後


    public void Awake()
    {
        Instance = this;
        Init();
        InitDragLineSystem();
        ResetWoodUI();
        GiveWoodID();
    }

    private void InitDragLineSystem()
    {
        finishedPoints = new List<Vector2>();
        previewPoints = new Vector2[2];
        previewLine.Points = previewPoints;

        mouseFollower.sizeDelta = new Vector2(mouseFollowerSize * 2, mouseFollowerSize * 2);

        operateRange.AddPointerDownListener(OnOperateRangePointerDown);
        operateRange.AddOnDragListener(OnOperateRangeDrag);
        operateRange.AddBeginDragListener(OnOperateRangeBeginDrag);
        operateRange.AddEndDragListener(OnOperateRangeEndDrag);
    }
    public void CreatePointOnPath()
    {
        targetPoints = TargetPoint.GetDict(nowWoodID);

        for (int i = 0; i < targetPoints.Length; i++)
        {
            Vector3 nowPos = new Vector3(targetPoints[i].x, targetPoints[i].y, 0);
            Instantiate(pointOnPath, nowPos, Quaternion.Euler(0, 0, 0), woodTrans[nowWoodSlotID]);
        }
    }

    private void GiveWoodID()
    {
        var i = 0;
        foreach (var myWood in woodSelectStorage)
        {
            myWood.GetComponent<WoodWantToCut>().woodID = woodIDList[i];
            myWood.GetComponent<WoodWantToCut>().slotID = i;
            i += 1;
        }

        var j = 0;
        foreach (var woodPath in pathStorage)
        {
            woodPath.GetComponent<WoodPath>().woodID = woodIDList[j];
            j += 1;
        }
    }


    public void OpenWoodSimple(int openID)
    {
        siginal.SetActive(true);
        for (int i = 0; i < woodSimpleStorage.Count; i++)
        {
            if (openID == i)
            {
                woodSimpleStorage[i].SetActive(true);

            }
            else
            {
                woodSimpleStorage[i].SetActive(false);
            }
        }
        nowWoodID = woodSelectStorage[openID].GetComponent<WoodWantToCut>().getWoodID();
        nowWoodSlotID = openID;
    }

    private void CloseAllWoodSimple()
    {
        for (int i = 0; i < woodSimpleStorage.Count; i++)
        {
            woodSimpleStorage[i].SetActive(false);
        }
        siginal.SetActive(false);
    }

    private void CutWood()
    {
        if (!myBag.itemList.Contains(woodBoard)) return;

        siginal.SetActive(false);
        woodChoose.SetActive(false);
        cutWood.SetActive(true);
        foreach (var myWood in pathStorage)
        {
            if (myWood.GetComponent<WoodPath>().woodID == nowWoodID)
            {
                myWood.SetActive(true);
            }
            else myWood.SetActive(false);
        }
        CreatePointOnPath();
        InventoryManager.Instance.SubItem(woodBoard);
    }

    private void GoBackPage()
    {
        ResetWoodUI();
    }

    private void Init()
    {
        buttonCutWoodYes = transform.Find("cuttingWood/WoodChoose/Simple/Signal/Interective/CutWoodYes").GetComponent<Button>();
        buttonCutWoodYes.onClick.AddListener(CutWood);
        buttonCutWoodNo = transform.Find("cuttingWood/WoodChoose/Simple/Signal/Interective/CutWoodNo").GetComponent<Button>();
        buttonCutWoodNo.onClick.AddListener(CloseAllWoodSimple);
        buttonGoBack = transform.Find("cuttingWood/Cut/GoBack").GetComponent<Button>();
        buttonGoBack.onClick.AddListener(GoBackPage);

        TargetPoint.GoUpdatePoints();
        //GameObject myObject = transform.Find("cuttingWood").gameObject;
        //Debug.Log(myObject.name);
        woodChoose.SetActive(false);
        cutWood.SetActive(false);
        siginal.SetActive(false);
        cuttingWood.SetActive(false);


    }


    public void ResetWoodUI()
    {
        woodChoose.SetActive(true);
        CloseAllWoodSimple();
        cutWood.SetActive(false);
        siginal.SetActive(false);
    }

    public void CutSucceed()
    {
        siginalUI.SiginalText("切割成功，木頭已放入背包");
        StopCoroutine(timeCoroutine);
        foreach (var myWood in woodList)
        {
            Debug.Log("現在的ID:" + nowWoodID);
            Debug.Log("切的木頭ID" + myWood.itemID);
            if (myWood.itemID == nowWoodID)
            {
                InventoryManager.Instance.AddNewItem(myWood);
                break;
            }
        }
        ClearLine();
        ResetWoodUI();
        //DrawWoodLine.Insatnce.ClearLine();
    }

    public void CutFail()
    {
        StopCoroutine(timeCoroutine);
        needSec = 0;
        timerText.text = string.Format("{0}", needSec.ToString("f2")).Replace(".", ":");
        siginalUI.SiginalText("切割失敗");
        ClearLine();
        ResetWoodUI();
    }

    public void StartCutWood()
    {
        needSec = 10;
        ClearLine();
        timeCoroutine = StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        //var temp = string.Format("{0}", needSec.ToString("f2")).Replace(".",":");
        timerText.text = string.Format("{0}", needSec.ToString("f2")).Replace(".", ":");
        totalTime = needSec;
        timeFinished = false;
        while (totalTime > 0)
        {
            //等待一秒後執行
            yield return new WaitForSeconds(0.01f);

            totalTime -= 0.01f;
            needSec -= 0.01f;

            if (needSec < 0)
            {
                needSec = 0;
                Debug.Log("結束");
                CutFail();
                timeFinished = true;
            }
            timerText.text = string.Format("{0}", needSec.ToString("f2")).Replace(".", ":");
        }
    }


    public bool GetTimeStatus()
    {
        return timeFinished;
    }

    public int GetNowWoodID()
    {
        return nowWoodID;
    }
    public int GetWoodIndex()
    {
        return nowWoodSlotID;
    }


    ///<summary>
    ///線托拽系統
    /// </summary>
    private void OnOperateRangePointerDown(PointerEventData eventData)
    {
        Debug.Log("有案到下去");
        bool isTouch = false;
        for (int i = 0; i < targetPoints.Length; i++)
        {
            Vector2 myPoint = new Vector2(targetPoints[i].x, targetPoints[i].y);
            if (IsTouch(eventData.position, myPoint, mouseFollowerSize))
            {
                startIndex = i;
                isTouch = true;
                break;
            }
        }

        if (!isTouch)
        {
            isOperate = false;
            return;
        }



        Debug.Log("pointer down is touch.");
        isOperate = true;
        currentIndex = startIndex;
        nextIndex = -99;
    }

    private void OnOperateRangeBeginDrag(PointerEventData eventData)
    {
        Debug.Log("有開始脫");
        if (!isOperate)
            return;
        StartCutWood();
        Vector2 point = TargetPoint.GetOriginPos(targetPoints[currentIndex]);
        
        finishedPoints.Add(point);
        finishedLine.Points = finishedPoints.ToArray();
        Debug.Log("targetpoint位置為：" + targetPoints[currentIndex]);
        Debug.Log("現在的點位置為：" + point);
        Debug.Log("線的位置為：" + finishedLine.Points);
        finishedLine.gameObject.SetActive(true);
        previewPoints[0] = point;
        previewPoints[1] = point;
        previewLine.gameObject.SetActive(true);
        isFinished = false;
        nextIndex = -99;
        nextValue = 0;
    }

    private void OnOperateRangeDrag(PointerEventData eventData)
    {
        if (!isOperate)
            return;

        //設置mouseFollower的位置
        mouseFollower.position = eventData.position;
        previewPoints[1] = TargetPoint.GetOriginPos(eventData.position);
        previewLine.SetAllDirty();

        //若已完成，則return
        if (isFinished)
            return;

        //若為無效值，判斷跟哪個鄰近點比較接近
        if (nextIndex == -99)
        {
            int tempIndex = CycleValue(currentIndex + 1, targetPoints.Length - 1);
            if (IsTouch(eventData.position, targetPoints[tempIndex], mouseFollowerSize))
                nextValue = 1;
            else
            {
                tempIndex = CycleValue(currentIndex - 1, targetPoints.Length - 1);
                if (!IsTouch(eventData.position, targetPoints[tempIndex], mouseFollowerSize))
                    return;

                nextValue = -1;
            }
            nextIndex = tempIndex;
        }
        else if (!IsTouch(eventData.position, targetPoints[nextIndex], mouseFollowerSize))
            return;

        currentIndex = nextIndex;
        if (currentIndex == startIndex)
        {
            Debug.Log("is finish.");
            CutSucceed();
            isFinished = true;
        }

        Vector2 point = TargetPoint.GetOriginPos(targetPoints[currentIndex]);
        finishedPoints.Add(point);
        previewPoints[0] = point;
        finishedLine.Points = finishedPoints.ToArray();

        nextIndex = CycleValue(nextIndex + nextValue, targetPoints.Length - 1);
    }

    private void OnOperateRangeEndDrag(PointerEventData eventData)
    {
        isOperate = false;
        isFinished = false;
        finishedPoints.Clear();
        //finishedLine.gameObject.SetActive(false);
        previewLine.gameObject.SetActive(false);
    }

    private bool IsTouch(Vector2 point1, Vector2 point2, float range)
    {
        float x = point1.x - point2.x;
        float y = point1.y - point2.y;
        if (x < 0)
            x = x * -1;
        if (y < 0)
            y = y * -1;

        return x <= range && y <= range;
    }

    /// <summary>
    /// When value bigger than max, value = 0,
    /// <para>value smaller than min, value = max.</para>
    /// </summary>
    private int CycleValue(int value, int max, int min = 0)
    {
        int v = (value > max) ? min : ((value < min) ? max : value);
        return v;
    }

    private void ClearLine()
    {
        previewPoints[0] = new Vector2(0, 0);
        previewPoints[1] = new Vector2(0, 0);
        previewLine.SetAllDirty();
        finishedPoints.Clear();
        finishedLine.Points = finishedPoints.ToArray();
        finishedLine.SetAllDirty();
    }



}


public static class TargetPoint
{
    private static float w = Screen.width;
    private static float h = Screen.height;


    private static Vector2[] pointWood301 = new Vector2[8]
    {
        new Vector2(1260,810),
        new Vector2(1150,810),
        new Vector2(1040,810),
        new Vector2(1040,700),
        new Vector2(1040,590),
        new Vector2(1150,590),
        new Vector2(1260,590),
        new Vector2(1260,700)
    };

    private static Vector2[] pointWood302 = new Vector2[8]
    {
        new Vector2(785,795),
        new Vector2(1015,795),
        new Vector2(1245,795),
        new Vector2(1245,565),
        new Vector2(1245,335),
        new Vector2(1015,335),
        new Vector2(785,335),
        new Vector2(785,565)
    };

    private static Vector2[] pointWood303 = new Vector2[8]
    {
        new Vector2(1255,800),
        new Vector2(1255,440),
        new Vector2(1255,80),
        new Vector2(890,80),
        new Vector2(535,80),
        new Vector2(535,440),
        new Vector2(535,800),
        new Vector2(890,800)
    };

    private static Vector2[] pointWood311 = new Vector2[8]
    {
        new Vector2(1230,325),
        new Vector2(110,325),
        new Vector2(970,325),
        new Vector2(970,540),
        new Vector2(970,790),
        new Vector2(1110,790),
        new Vector2(1230,790),
        new Vector2(1230,540)
    };

    private static Vector2[] pointWood321 = new Vector2[8]
    {
        new Vector2(1230,80),
        new Vector2(1230,440),
        new Vector2(1230,800),
        new Vector2(1092,800),
        new Vector2(955,800),
        new Vector2(955,440),
        new Vector2(955,80),
        new Vector2(1092,80)
    };

    private static Vector2[] pointWood331 = new Vector2[8]
    {
        new Vector2(990,345),
        new Vector2(785,345),
        new Vector2(785,85),
        new Vector2(990,85),
        new Vector2(1245,85),
        new Vector2(1245,345),
        new Vector2(1245,545),
        new Vector2(990,545)
    };

    private static Vector2[] pointWood341 = new Vector2[12]
    {
        new Vector2(1245,795),
        new Vector2(975,795),
        new Vector2(755,795),
        new Vector2(540,795),
        new Vector2(540,520),
        new Vector2(755,520),
        new Vector2(755,305),
        new Vector2(975,305),
        new Vector2(975,90),
        new Vector2(1245,90),
        new Vector2(1245,305),
        new Vector2(1245,520)
    };

    private static Vector2[] pointWood351 = new Vector2[10]
    {
        new Vector2(1030,95),
        new Vector2(750,95),
        new Vector2(750,515),
        new Vector2(535,515),
        new Vector2(535,790),
        new Vector2(1030,790),
        new Vector2(1030,580),
        new Vector2(1240,580),
        new Vector2(1240,305),
        new Vector2(1030,305)
    };

    private static Vector2[] pointWood361 = new Vector2[7]
    {
        new Vector2(785,575),
        new Vector2(785,80),
        new Vector2(985,80),
        new Vector2(1245,80),
        new Vector2(1245,575),
        new Vector2(1245,790),
        new Vector2(985,790),
    };

    private static Vector2[] pointWood371 = new Vector2[8]
    {
        new Vector2(1240,790),
        new Vector2(980,790),
        new Vector2(785,790),
        new Vector2(785,515),
        new Vector2(980,515),
        new Vector2(980,85),
        new Vector2(1240,85),
        new Vector2(1240,515),
    };

    private static Vector2[] pointWood601 = new Vector2[] //圓的可能還會再改
    {
        new Vector2(880,120),

        new Vector2(570,435),

        new Vector2(880,770),

        new Vector2(1220,435)

    };

    private static Dictionary<int, Vector2[]> pointsDict = new Dictionary<int, Vector2[]>()
    {
        {301,pointWood301 },{302,pointWood302},{303,pointWood303},{311,pointWood311},{321,pointWood321},
        {331,pointWood331 },{341,pointWood341},{351,pointWood351},{361,pointWood361},{371,pointWood371},{601,pointWood601}
    };

    public static Vector2[] GetDict(int woodID)
    {
        foreach (var woodPoint in pointsDict)
        {
            if (woodPoint.Key == woodID)
            {
                //UpdatePointPos(w, h, woodPoint.Value);
                return woodPoint.Value;
            }
        }
        return null;
    }


    public static void GoUpdatePoints() => UpdateAllPointPos(w, h);

    private static void UpdateAllPointPos(float width, float height)
    {
        ///算他實際位移量
        float _x = width / 1800 * 900;
        float _y = height / 900 * 450;

        Vector2 resetVector = new Vector2(-900, -450);
        Vector2 newOffsetVector = new Vector2(_x, _y);

        float changeScale_x = width / 1800;
        float changeScale_y = height / 900;


        foreach (var myValue in pointsDict.Values)
        {
            ///先重製、改變大小、加上修改後偏移量
            for (int i = 0; i < myValue.Length; i++)
            {
                myValue[i] += resetVector;
                myValue[i].x *= changeScale_x;
                myValue[i].y *= changeScale_y;
                myValue[i] += newOffsetVector;
            }
        }

    }


    /// <summary>
    /// 不知道為何，線的座標會維持在1800*900，猜測是UI LineRenderer會把範圍固定在我們所設置的Canvas大小，也就是1800*900
    /// </summary>
    public static Vector2 GetOriginPos(Vector2 nowPoint)
    {
        float changeScale_x = w / 1800;
        float changeScale_y = h / 900;

        Vector2 afterResetPos = new Vector2(nowPoint.x /= changeScale_x, nowPoint.y /= changeScale_y);
        return afterResetPos;
    }

}
