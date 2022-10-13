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
        TextUpdate();
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
        int sceneIndex = 2; //Player.Instance.myStatus.levelChose;
        int score = (sceneIndex - 2) + level * 3;
        awardSignal.sprite = awardSprites[score];
    }

    private void StartText()
    {
        coroutine = StartCoroutine(RollText());
    }

    private void TextUpdate(string name = "xiaopibo", string score = "29:30", string location = "崑崙山")
    {
        //mainText.fontSize = 70;
        mainText.text = "";
        mainText.text += "恭喜玩家" + name + "\n";
        mainText.text += "用時 " + score + " 通關 " + location + "\n";
        mainText.text += "\n\n\n";
        mainText.text += "恭喜您獲得以下獎章\n";
        mainText.text += "\n\n\n\n\n";
        mainText.fontSize = 50;
        mainText.text += "《西境山海》 Cast\n";
        mainText.text += "程式設計： 徐存昇、許皓陞\n";
        mainText.text += "美術設計： 許聿銘\n";
        mainText.text += "場景搭建： 張子濰\n";
        mainText.text += "教材設計： 卓育霆\n";
        
        mainText.text += "\n\n\n\n\n";
        mainText.text += "《特別感謝》\n";
        mainText.text += "音樂設計： 廖于任\n";
        mainText.text += "獎章系統： 劉威成\n";
        mainText.text += "技術指導： 王厚竣\n\n";
        mainText.text += "指導老師： 蕭顯勝\n";
        mainText.text += "\n\n\n\n\n";
        mainText.text += "《西境山海製作委員會》2022製作";
        
    }

    IEnumerator RollText()
    {
        RectTransform moveContent = mainText.gameObject.GetComponentInParent<RectTransform>();
        var targetPos = moveContent.position.y;
        while (targetPos < 2000)
        {
            targetPos += 100 * Time.deltaTime;
            yield return null;

            moveContent.position = new Vector2(mainText.gameObject.transform.position.x, targetPos);
        }
    }
}
