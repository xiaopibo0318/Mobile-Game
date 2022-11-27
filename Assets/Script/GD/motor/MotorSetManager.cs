using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class MotorSetManager : MonoBehaviour, IPointerDownHandler
{
    [Header("Operate Item")]
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private List<Transform> allMotorTransform;
    [SerializeField] private Button goBackButton;
    [SerializeField] private Button confirmButton;

    [Header("內部變數")]
    private int currentID = -99;
    private bool isFirst;
    private MotorData[] motorDatas = new MotorData[4];

    [Header("馬達圖片")]
    [SerializeField] private Sprite[] allMotorImage;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isFirst)
        {
            SiginalUI.Instance.SiginalText("要怎麼擺放馬達\n才能讓電梯向下呢\n可以參考百科全書的特性");
            isFirst = false;
            return;
        }


        for (int i = 0; i < allMotorTransform.Count; i++)
        {
            if (eventData.pointerCurrentRaycast.gameObject == allMotorTransform[i].gameObject)
            {
                currentID = i;
                allMotorTransform[currentID].DOScale(1.2f, 0.8f);

            }
            else
            {
                allMotorTransform[i].DOScale(1, 0.8f);
            }

        }
    }

    private void Start()
    {
        ButtonInit();
        isFirst = true;
    }


    private void ButtonInit()
    {
        leftButton.onClick.AddListener(delegate { RotateMotor(270); });
        rightButton.onClick.AddListener(delegate { RotateMotor(90); });
        goBackButton.onClick.AddListener(delegate { CanvasManager.Instance.openCanvas("original"); });
        confirmButton.onClick.AddListener(DetectSuccess);
    }

    private void ResetMotorData()
    {
        for (int i = 0; i < motorDatas.Length; i++)
        {
            motorDatas[i] = new MotorData(0, 0);
        }
    }


    /// <summary>
    /// 1右、2下、3左、4上
    /// 如果往上翻一次、轉 -90 度
    /// 如果往下翻一次、轉 90 度
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="vect"></param>
    private void RotateMotor(float dir, int vect = 0)
    {
        if (currentID == -99)
        {
            SiginalUI.Instance.SiginalText("還未選擇馬達呦！", 1, 40);
        }
        //if (((int)((allMotorTransform[currentID].rotation.z) / 90)) % 90 == 0)
        //{
        //    dir += (((int)((allMotorTransform[currentID].rotation.z) / 90)) + 1) * 90;

        //}
        //else
        //{
        //    dir += ((int)((allMotorTransform[currentID].rotation.z) / 90)) * 90;
        //}
        Debug.Log($"原本的是{allMotorTransform[currentID].rotation.eulerAngles.z}，改變角度為：{dir}");
        if (!Mathf.Approximately(allMotorTransform[currentID].rotation.eulerAngles.z % 90, 0)) return;

        dir += ((int)(allMotorTransform[currentID].rotation.eulerAngles.z));
        while (dir > 361) dir -= 360;
        while (dir < 1) dir += 360;
        motorDatas[currentID].UpdateMotorData(vect, dir);

        int temp = 0;
        for (int i = 0; i < allMotorImage.Length; i++)
        {
            if (allMotorImage[i] == motorDatas[currentID].image)
            {
                temp = i;
            }
        }

        switch (motorDatas[currentID].vect)
        {
            case 1:
                Debug.Log("右翻");
                motorDatas[currentID].image = allMotorImage[(temp + 1) % 4];
                break;
            case 2:
                Debug.Log("左翻");
                motorDatas[currentID].image = allMotorImage[(temp + 3) % 4];
                break;
            case 3:
                Debug.Log("前翻");

                break;
            case 4:
                Debug.Log("後翻");
                break;
        }
        SetupMotorTransform();
    }

    private void SetupMotorTransform()
    {
        Vector3 rotateValue = new Vector3(0, 0, motorDatas[currentID].dir);
        allMotorTransform[currentID].DORotate(rotateValue, .5f);
        allMotorTransform[currentID].GetComponent<Image>().sprite = motorDatas[currentID].image;

    }


    private void DetectSuccess()
    {
        for (int i = 0; i < allMotorTransform.Count; i++)
        {
            allMotorTransform[i].DOScale(1, .01f);
        }


        Debug.Log($"原本的是1{Mathf.Approximately(allMotorTransform[0].eulerAngles.z, 180)}");
        Debug.Log($"原本的是2{Mathf.Approximately(allMotorTransform[1].eulerAngles.z, 0)}");
        Debug.Log($"原本的是3{Mathf.Approximately(allMotorTransform[2].eulerAngles.z, 180)}");
        Debug.Log($"原本的是4{Mathf.Approximately(allMotorTransform[3].eulerAngles.z, 0)}");



        if (
            (Mathf.Approximately(allMotorTransform[0].eulerAngles.z, 180) ||
            allMotorTransform[0].eulerAngles.z == 180) &&
            (Mathf.Approximately(allMotorTransform[1].eulerAngles.z, 0) ||
            allMotorTransform[1].eulerAngles.z == 0) &&
            (Mathf.Approximately(allMotorTransform[2].eulerAngles.z, 180) ||
            allMotorTransform[2].eulerAngles.z == 180) &&
            (Mathf.Approximately(allMotorTransform[3].eulerAngles.z, 0) ||
            allMotorTransform[3].eulerAngles.z == 0))
        {
            GDMananger.Instance.gameStatus = 6;
            GDMananger.Instance.UpdateMap();
            SiginalUI.Instance.SiginalText("電梯好像能使用了");
            Debug.Log("成功");
        }
    }


}




public class MotorData
{
    public int vect { get; set; }
    public float dir { get; set; }
    public Sprite image { get; set; }

    public MotorData(int vect, float dir)
    {
        this.vect = vect;
        this.dir = dir;
    }

    public void ResetMotorData()
    {
        this.vect = 0;
        this.dir = 0;
    }

    public void UpdateMotorData(int newVect, float newDir)
    {
        this.vect = newVect;
        this.dir = newDir;
    }

}