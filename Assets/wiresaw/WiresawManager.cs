using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class WiresawManager : MonoBehaviour
{
    [Header("壓力表")]
    public Slider myStressSlider;
    private Button myButton;
    private bool isStart;
    [SerializeField] private Text timerText;

    [Header("螺絲")]
    private const float rotateTarget = 540;
    private const float rotateSpeed = 180;


    [Header("UI組件")]
    public Text textLabel;


    [Header("文本文件")]
    public TextAsset textFile;
    public float textSpeed;

    [Header("內部係數")]
    public static WiresawManager Instance;
    [SerializeField] GameObject topButton;
    [SerializeField] GameObject underButton;
    [SerializeField] Item wiresaw;
    [SerializeField] DraggableKnife myKnife;
    int textRange;
    bool topButtonOpen;
    bool underButtonOpen;
    bool textFinished;
    bool delayFinished;
    public bool isFinished;
    private bool isSignal = false;

    private Coroutine rotateCoroutine = null;

    bool knifeAdd;

    [SerializeField] List<GameObject> myText = new List<GameObject>();
    List<string> textList = new List<string>();




    private void Awake()
    {
        Instance = this;
        GetTextFromFile(textFile);

        myButton = transform.Find("Stress/click").GetComponent<Button>();
        myButton.onClick.AddListener(delegate () { buttonOnClick(); });
        ResetWireSaw();
        //StartMyGame();

    }

    private void OnEnable()
    {
        if (!isSignal)
        {
            SiginalUI.Instance.SiginalText("手線鋸組裝知識\n已登入百科全書");
            BookContentManager.Instance.ActivateKnowledge(0);
            isSignal = true;
        }
        else
        {
            SiginalUI.Instance.SiginalText("已重製手線鋸狀態\n請重新開始組裝");
        }



    }


    public void topButtonMananger()
    {
        if (textFinished == true)
        {
            if (topButtonOpen == false)
            {
                rotateCoroutine = StartCoroutine(RotateTopButton(1, DetectFInished));
                topButtonOpen = true;
                StartCoroutine(SetTextUI(15));

            }
            else if (topButtonOpen == true)
            {
                rotateCoroutine = StartCoroutine(RotateTopButton(-1, DetectFInished));
                topButtonOpen = false;
                StartCoroutine(SetTextUI(25));
            }
        }
        else if (textFinished == false)
        {
            textLabel.text = "您動作太快了，請稍帶三秒後再繼續盡情操作";
        }

    }

    public void underButtonManager()
    {
        if (textFinished == true)
        {
            if (underButtonOpen == false)
            {
                rotateCoroutine = StartCoroutine(RotateUnderButton(1, DetectFInished));
                underButtonOpen = true;
                StartCoroutine(SetTextUI(20));

            }
            else if (underButtonOpen == true)
            {
                rotateCoroutine = StartCoroutine(RotateUnderButton(-1, DetectFInished));
                underButtonOpen = false;
                StartCoroutine(SetTextUI(30));
            }
        }
        else if (textFinished == false)
        {
            textLabel.text = "您動作太快了，請稍帶三秒後再繼續盡情操作";
        }

    }


    IEnumerator RotateTopButton(int inverse, UnityAction DetectFinished = null)
    {
        float deltaZ = 0;
        while (deltaZ < rotateTarget)
        {
            deltaZ += rotateSpeed * Time.deltaTime;
            topButton.transform.localEulerAngles = new Vector3(topButton.transform.rotation.x, topButton.transform.rotation.y, deltaZ * inverse);
            yield return null;
        }
        DetectFinished?.Invoke();

    }

    IEnumerator RotateUnderButton(int inverse, UnityAction DetectFinished = null)
    {
        float deltaZ = 0;

        while (deltaZ < rotateTarget)
        {
            deltaZ += rotateSpeed * Time.deltaTime;
            underButton.transform.localEulerAngles = new Vector3(underButton.transform.rotation.x, underButton.transform.rotation.y, deltaZ * inverse);
            yield return null;
        }
        DetectFinished?.Invoke();
    }


    private void DetectFInished()
    {
        if (topButtonOpen == false && underButtonOpen == false && knifeAdd)
        {
            InventoryManager.Instance.AddNewItem(wiresaw);
            textFinished = true;

            isFinished = true;
            textLabel.text = "您以成功組裝手線鋸，可去背包查看，知識也同步進百科全書了。";
        }
    }


    //1是逆時針鬆開，-1是順時針轉緊




    public void AddKnife()
    {
        if (topButtonOpen == false || underButtonOpen == false)
        {
            knifeAdd = false;
            DraggableKnife.Instance.goToOriginal();
            StartCoroutine(SetTextUI(5));

        }

        if (topButtonOpen == true && underButtonOpen == true)
        {
            knifeAdd = true;
            StartCoroutine(SetTextUI(10));
            DraggableKnife.Instance.addKnifeToWiresaw();
            isStart = true;
            StartMyGame();
        }
    }

    void GetTextFromFile(TextAsset file)
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

    IEnumerator SetTextUI(int n)
    {
        Debug.Log(n);
        Debug.Log(textList[n]);
        textFinished = false;
        textLabel.text = "";
        textRange = n;
        while (n - textRange < 5)
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

    public void buttonOnClick()
    {
        myStressSlider.value += 0.5f;
    }

    public void StartMyGame()
    {
        myStressSlider.gameObject.SetActive(true);
        myButton.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        timerText.text = "10";
        StartCoroutine(StartStressGame());
    }

    IEnumerator StartStressGame()
    {
        var myRange = new List<float>(8) { 0.3f, 0.6f, 0.9f, 0.9f, 3f, 0.88f, 0.4f, 2f };
        var thisTime = 10f;
        myStressSlider.value = 10;
        var addKnifeSuccess = false;

        SiginalUI.Instance.SiginalText("請配合左下角的按鍵\n將上方壓力值控制在中間時\n將鋸條拖進去", 10,40);
        float signalTIme = 10;
        while (signalTIme > 0)
        {
            yield return null; //延遲 一秒
            signalTIme -= Time.deltaTime;

        }
        while (thisTime > 0)
        {
            timerText.text = string.Format("{0}", thisTime.ToString("f2")).Replace(".", ":");
            var index = Random.Range(0, myRange.Count);
            var myTIme = myRange[index];
            yield return null; //延遲 一秒
            myStressSlider.value -= Time.deltaTime * myTIme;
            thisTime -= Time.deltaTime; //扣掉一楨的的秒數
            if (topButtonOpen == false || underButtonOpen == false)
            {
                addKnifeSuccess = true;
                break;
            }

        }
        if (myStressSlider.value < 11.25 && myStressSlider.value > 8.75 && addKnifeSuccess)
        {
            Debug.Log("成功");
            myStressSlider.gameObject.SetActive(false);
            myButton.gameObject.SetActive(false);
            timerText.gameObject.SetActive(false);


        }
        else
        {
            SiginalUI.Instance.SiginalText("組裝失敗，請繼續加油");
            ResetWireSaw();
            StopCoroutine(rotateCoroutine);
            rotateCoroutine = null;
        }
    }

    public void ResetWireSaw()
    {
        myStressSlider.gameObject.SetActive(false);
        myButton.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);

        isFinished = false;
        textFinished = true;
        delayFinished = false;
        topButtonOpen = false;
        underButtonOpen = false;


        SetTextUI(0);

        knifeAdd = false;
        myKnife.goToOriginal();
        textRange = 0;
        textLabel.text = "您可以藉由拖動鋸條以及尖嘴鉗來組裝手線鋸";

        isStart = false;
    }


}
