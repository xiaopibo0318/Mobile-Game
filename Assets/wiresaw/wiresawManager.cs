using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wiresawManager : MonoBehaviour
{
    [Header("壓力表")]
    public GameObject myStress;
    private float stressValue;
    public GameObject myClcikObject;
    private Button mybutton;
    private bool isStart;
    

    [Header("UI組件")]
    public Text textLabel;


    [Header("文本文件")]
    public TextAsset textFile;
    public float textSpeed;

    [Header("內部係數")]
    public static wiresawManager Instance;
    [SerializeField] GameObject topButton;
    [SerializeField] GameObject underButton;

    [SerializeField] int rotateSpeed;
    [SerializeField] Item wiresaw;
    [SerializeField] DraggableKnife myKnife;
    float change_z;
    int inverse;
    int textRange;
    bool topButtonOpen;
    bool underButtonOpen;
    bool textFinished;
    bool delayFinished;
    bool _OTV;

    int status;
    // 0 = 兩鎖, 1 = 上鎖下開, 2 = 下鎖上開, 3 = 全開
    // 4 = 上刀, 5 = 上鎖下開, 6 = 下鎖上開, 7 = 全鎖 

    bool knifeAdd;

    [SerializeField] List<GameObject> myText = new List<GameObject>();
    List<string> textList = new List<string>();

    private void Awake()
    {
        Instance = this;  
        getTextFromFile(textFile);

        mybutton = myClcikObject.GetComponent<Button>();
        //mybutton.onClick.AddListener(buttonOnClick);
        mybutton.onClick.AddListener(delegate () { buttonOnClick(); });

        ResetWireSaw();
        //StartMyGame();

    }

    private void FixedUpdate()
    {
        if (topButtonOpen == true && underButtonOpen == true)
        {
            if (knifeAdd == false)
            {
                StartCoroutine(delay(5));
                if (textFinished == true)
                {
                    status = 3;
                    OTV();
                    textFinished = false;
                }
            }
        }
        if (knifeAdd == true)
        {
            if(topButtonOpen == false && underButtonOpen == false)
            {
                status = 8;
            }
        }
        //Debug.Log("狀態為"+status);
        //Debug.Log("上方按鈕"+topButtonOpen);
        //Debug.Log("下方按鈕"+underButtonOpen);
        if (status == 8)
        {
            StartCoroutine(delay(5));
            if (textFinished == false)
            {
                //StartCoroutine(SetTextUI(45));
                InventoryManager.Instance.AddNewItem(wiresaw);
                status = 0;
                textFinished = true;
            }
            status = 9;
        }
        if (status == 9)
        {
            BookContentManager.Instance.ActivateKnowledge(0);
            textLabel.text = "您以成功組裝手線鋸，可去背包查看，知識也同步進百科全書了。";
        }
    }

    public void topButtonMananger()
    {
        if(textFinished == true)
        {
            if (topButtonOpen == false && knifeAdd == false)
            {
                inverse = 1;
                StartCoroutine(rotateTopButton());
                status = 1;
                topButtonOpen = true;
                StartCoroutine(SetTextUI(15));

            }
            else if (topButtonOpen == true && knifeAdd == false)
            {
                inverse = -1;
                StartCoroutine(rotateTopButton());
                status = 1;
                topButtonOpen = false;
                StartCoroutine(SetTextUI(25));
            }
            else if (topButtonOpen == false && knifeAdd == true)
            {
                inverse = 1;
                StartCoroutine(rotateTopButton());
                status = 6;
                topButtonOpen = true;
                StartCoroutine(SetTextUI(15));
            }
            else if (topButtonOpen == true && knifeAdd == true)
            {
                inverse = -1;
                StartCoroutine(rotateTopButton());
                status = 5;
                topButtonOpen = false;
                StartCoroutine(SetTextUI(25));
            }
        }else if (textFinished == false)
        {
            textLabel.text = "您動作太快了，請稍帶三秒後再繼續盡情操作";
        }
        
    }

    public void underButtonManager()
    {
        if(textFinished == true)
        {
            if (underButtonOpen == false && knifeAdd == false)
            {
                inverse = 1;
                StartCoroutine(rotateUnderButton());
                status = 2;
                underButtonOpen = true;
                StartCoroutine(SetTextUI(20));

            }
            else if (underButtonOpen == true && knifeAdd == false)
            {
                inverse = -1;
                StartCoroutine(rotateUnderButton());
                status = 0;
                underButtonOpen = false;
                StartCoroutine(SetTextUI(30));
            }
            else if (underButtonOpen == false && knifeAdd == true)
            {
                inverse = 1;
                StartCoroutine(rotateUnderButton());
                status = 5;
                underButtonOpen = true;
                StartCoroutine(SetTextUI(20));
            }
            else if (underButtonOpen == true && knifeAdd == true)
            {
                inverse = -1;
                StartCoroutine(rotateUnderButton());
                status = 6;
                underButtonOpen = false;
                StartCoroutine(SetTextUI(30));
            }
        }else if (textFinished == false)
        {
            textLabel.text = "您動作太快了，請稍帶三秒後再繼續盡情操作";
        }

    }


    IEnumerator rotateTopButton()
    {
        if( inverse == 1)
        {
            change_z = 0;
            while (change_z < 180)
            {
                yield return new WaitForSeconds(.1f);
                change_z += rotateSpeed * Time.fixedDeltaTime * 10 * inverse;
                topButton.transform.localEulerAngles = new Vector3(topButton.transform.rotation.x, topButton.transform.rotation.y, change_z);
                //Debug.Log(change_z);
                Draggable.Instance.goToOriginal();
                if (change_z < 0) break;
            }
        }else if (inverse == -1)
        {
            change_z = 180;
            while (change_z > 0)
            {
                yield return new WaitForSeconds(.1f);
                change_z += rotateSpeed * Time.fixedDeltaTime * 10 * inverse;
                topButton.transform.localEulerAngles = new Vector3(topButton.transform.rotation.x, topButton.transform.rotation.y, change_z);
                //Debug.Log(change_z);
                Draggable.Instance.goToOriginal();
                if (change_z < 0) break;
            }
        }
        
    }

    IEnumerator rotateUnderButton()
    {
        if(inverse == 1)
        {
            change_z = 0;
            while (change_z < 180)
            {
                yield return new WaitForSeconds(.1f);
                change_z += rotateSpeed * Time.fixedDeltaTime * 10 * inverse;
                underButton.transform.localEulerAngles = new Vector3(underButton.transform.rotation.x, underButton.transform.rotation.y, change_z);
                //Debug.Log(change_z);
                Draggable.Instance.goToOriginal();
                if (change_z < 0) break;
            }
        }else if (inverse == -1)
        {
            change_z = 180;
            while (change_z >0)
            {
                yield return new WaitForSeconds(.1f);
                change_z += rotateSpeed * Time.fixedDeltaTime * 10 * inverse;
                underButton.transform.localEulerAngles = new Vector3(underButton.transform.rotation.x, underButton.transform.rotation.y, change_z);
                //Debug.Log(change_z);
                Draggable.Instance.goToOriginal();
                if (change_z < 0) break;
            }
        }
        
    }

    public void addKnife()
    {
        if (status < 3)
        {
            DraggableKnife.Instance.goToOriginal();
            StartCoroutine( SetTextUI(5));
        }
        
        if (status == 3)
        {
            knifeAdd = true;
            StartCoroutine( SetTextUI(10));
            status = 4;
            DraggableKnife.Instance.addKnifeToWiresaw();
            isStart = true;
            StartMyGame();
        }
    }

    public void OTV()
    {
        if(_OTV == true)
        {
            StartCoroutine(SetTextUI(35));
        }
        _OTV = false;
    }


    void getTextFromFile(TextAsset file)
    {
        textList.Clear();

        //自動定義類型var
        var lineData = file.text.Split('\n');

        //讀取文件
        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }

    //public void textBridge(int n)
    //{
    //    StartCoroutine(SetTextUI(n));
    //}


    IEnumerator SetTextUI(int n)
    {
        Debug.Log(n);
        Debug.Log(textList[n]);
        textFinished = false;
        textLabel.text = "";
        textRange = n;
        while ( n - textRange < 5)
        {
            for (int i = 0; i < textList[n].Length; i++)
            {
                textLabel.text += textList[n][i];
                yield return new WaitForSeconds(textSpeed);
            }
            n += 1;
        }
        

        textFinished = true;
    }


    IEnumerator delay(int _time)
    {
        while (_time > 0)
        {
            yield return new WaitForSeconds(1);
            _time -= 1;
        }
        delayFinished = true;
    }

    public void buttonOnClick()
    {
        myStress.GetComponent<Slider>().value += 0.5f;
    }

    public void StartMyGame()
    {
        myStress.SetActive(true);
        myClcikObject.SetActive(true);
        StartCoroutine(StartStressGame());
    }

    IEnumerator StartStressGame()
    {
        var myRange = new List<float>(8) { 0.3f, 0.6f, 0.9f, 0.9f, 3f, 8f, 4f, 2f };
        var thisTime = 10f;
        while (thisTime>0)
        {
            var index = Random.Range(0, myRange.Count);
            var myTIme = myRange[index];
            yield return new WaitForFixedUpdate();
            myStress.GetComponent<Slider>().value -= Time.deltaTime * myTIme;
            thisTime -= Time.fixedDeltaTime;
            if (status == 5 || status == 6)
                break;
        }
        if (myStress.GetComponent<Slider>().value<11.25 && myStress.GetComponent<Slider>().value > 8.75)
        {
            Debug.Log("成功");
            myStress.SetActive(false);
            myClcikObject.SetActive(false);

        }
        else
        {
            Debug.Log("失敗");
            ResetWireSaw();
        }
    }

    public void ResetWireSaw()
    {
        myStress.SetActive(false);
        myClcikObject.SetActive(false);

        textFinished = true;
        delayFinished = false;
        topButtonOpen = false;
        underButtonOpen = false;
        change_z = topButton.transform.rotation.z;

        status = 0;
        inverse = 1;

        SetTextUI(0);

        knifeAdd = false;
        myKnife.goToOriginal();
        _OTV = true;
        textRange = 0;
        textLabel.text = "您可以藉由拖動鋸條以及尖嘴鉗來組裝手線鋸";

        isStart = false;
    }

}
