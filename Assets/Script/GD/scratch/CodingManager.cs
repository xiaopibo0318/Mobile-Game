using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodingManager : Singleton<CodingManager>
{
    public List<GameObject> blocks = new List<GameObject>();
    public GameObject blockGrid;
    public GameObject emptyBlockSlot;

    [Header("執行列表")]
    public blockList exeLists;
    private List<int> tempMoveList = new List<int>();


    [Header("UI組件")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button clearButton;
    [SerializeField] private Button goBackButton;

    public string frequencyBuzzer { get; set; }
    public int pinBuzzer { get; set; }

    public int pinBuzzerElectricSet { get; set; }
    public bool isElectricSuccess { get; set; }

    private void Close() => CanvasManager.Instance.openCanvas("original");

    // Start is called before the first frame update

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        clearButton.onClick.AddListener(ResetBlock);
        goBackButton.onClick.AddListener(Close);
        ResetBlock();
        isElectricSuccess = false;
    }

    public void setUpBlockSlot()
    {
        blocks.Clear();
        for (int i = 0; i < 7; i++)
        {
            blocks.Add(Instantiate(emptyBlockSlot));
            emptyBlockSlot.name = "blockSlot";
            blocks[i].transform.SetParent(blockGrid.transform);
            blocks[i].GetComponent<blockSlot>().slotID = i;
        }
    }


    private void ClearTempList()
    {

        tempMoveList.Clear();
    }

    public void StartGame()
    {
        StartCoroutine(GoStartGame());
    }

    IEnumerator GoStartGame()
    {
        
        for (int i = 0; i < exeLists.exeList.Length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            switch (exeLists.exeList[i])
            {
                case 21:
                    Debug.Log($"新的東西：{ pinBuzzer }頻率是：{frequencyBuzzer}");
                    Debug.Log("電線的pin為；" + pinBuzzerElectricSet);
                    Debug.Log($"電線連接狀況：{isElectricSuccess.ToString()}");
                    /// 4 -> 13
                    /// 5 -> 12
                    /// 8 -> 9
                    if (pinBuzzerElectricSet == pinBuzzer && isElectricSuccess)
                    {

                        if (frequencyBuzzer == "1047")
                        {
                            SiginalUI.Instance.SiginalText("打開房門成功");
                            GDMananger.Instance.gameStatus = 9;
                            GDMananger.Instance.UpdateMap();
                            Debug.Log("成功");
                        }
                    }
                    break;
                default:
                    break;
            }
            SiginalUI.Instance.SiginalText("已燒錄程式進電路板中");
        }
        ClearTempList();
    }



    public void ResetBlock()
    {
        for (int i = 0; i < exeLists.exeList.Length; i++)
        {
            exeLists.exeList[i] = -99;
        }
        for (int i = 0; i < blockGrid.transform.childCount; i++)
        {
            GameObject go = blockGrid.transform.GetChild(i).gameObject;
            Destroy(go);
        }
        setUpBlockSlot();
        ClearTempList();
    }
}
