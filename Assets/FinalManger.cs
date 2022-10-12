using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalManger : Singleton<FinalManger>
{
    [SerializeField] private Sprite[] awardSprites;
    private int totalTime;
    private int level;
    private Image awardSignal;

    private void CaculateScore()
    {
        totalTime = PostMethod.Instance.totalSeconds;
        if (totalTime > 2399)
        {
            level = 0;
        }
        else if (totalTime > 1199)
        {
            level = 1;
        }
        else
        {
            level = 2;
        }
        int sceneIndex = Player.Instance.myStatus.levelChose;
        int score = (sceneIndex - 2) + level * 3;
        awardSignal.sprite = awardSprites[score];
    }

}
