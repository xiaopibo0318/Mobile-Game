using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalManger : Singleton<FinalManger>
{
    [SerializeField] private Sprite[] awardSprites;
    private int totalTime;
    private int level;
    [SerializeField] private Image awardSignal;
    [SerializeField] private Text mainText;
    private Coroutine coroutine;

    private void Start()
    {
        CaculateScore();
        string timeText = ((int)totalTime / 60).ToString() + "：" + ((int)totalTime % 60).ToString();
        TextUpdate(Player.Instance.myStatus.name, timeText);
    }

    private void OnEnable()
    {
        StartText();
    }

    /// <summary>
    /// 共 75分鐘 4500秒
    /// 金 30min (1800)
    /// 銀 50min (3000)
    /// </summary>
    private void CaculateScore()
    {
        totalTime = PostMethod.Instance.totalSeconds;
        if (totalTime <= 1800)
        {
            level = 0;
        }
        else if (totalTime <= 3000)
        {
            level = 1;
        }
        else
        {
            level = 2;
        }
        int sceneIndex = Player.Instance.myStatus.levelChoose;
        int score = (sceneIndex - 2) + level * 3;
        awardSignal.sprite = awardSprites[score];
    }

    private void StartText()
    {
        coroutine = StartCoroutine(RollText());
    }

    private void TextUpdate(string name = "xiaopibo", string score = "29:30", string location = "宮殿")
    {

        mainText.text = "";

        if(location == "崑崙山")
        {
            mainText.text += "你發現了崑崙山失竊的靈果\n以及西王母的資產\n";
            mainText.text += "這是西王母的保險櫃\n";
            mainText.text += "原來靈果並不是無緣故得消失\n";
            mainText.text += "而是西王母藏起來了\n";
        }
        else if(location == "宮殿")
        {
            mainText.text += "你發現了女媧造人是個騙局\n這一切都只是為了鞏固自己身為首領的權力\n";
            mainText.text += "以及將自己神化而散布出的謠言\n";
            mainText.text += "雕的失蹤正是因為部落中有一股推翻女媧的勢力正在崛起\n";
            mainText.text += "女媧為了避免土雕落入敵方勢力而將其鎖在這間暗房之中\n";
            mainText.text += "\n\n\n";
        }
        mainText.text += "恭喜玩家" + name + "\n";
        mainText.text += "用時 " + score + " 通關 " + location + "\n";
        mainText.text += "\n\n\n";
        mainText.text += "恭喜您獲得以下獎章";
        mainText.text += "\n\n\n\n\n";
        mainText.fontSize = 50;
        mainText.text += "《西境山海》 Cast\n";
        mainText.text += "程式設計： 徐存昇、許皓陞\n";
        mainText.text += "美術設計： 許聿銘\n";
        mainText.text += "場景搭建： 張子濰\n";
        mainText.text += "教材設計： 卓育霆\n";
        mainText.text += "劇情設計： 徐存昇、張子濰";

        mainText.text += "\n\n\n\n\n";
        mainText.text += "《特別感謝》\n";
        mainText.text += "友情支援： Tk.Studio\n";
        mainText.text += "音樂設計： 廖于任\n";
        mainText.text += "獎章系統： 劉威成\n";
        mainText.text += "技術指導： Apple Wang\n\n";
        mainText.text += "指導老師： 蕭顯勝\n";
        mainText.text += "\n\n\n\n\n";
        mainText.text += "《西境山海製作委員會》2022製作";

    }

    IEnumerator RollText()
    {
        float textSpeed = 50;
        RectTransform moveContent = mainText.gameObject.GetComponentInParent<RectTransform>();
        var targetPos = moveContent.position.y;
        while (targetPos < 2000)
        {
            targetPos += textSpeed * Time.deltaTime;
            yield return null;

            moveContent.position = new Vector2(mainText.gameObject.transform.position.x, targetPos);
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
