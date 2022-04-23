using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;

    bool textFinished;

    int total_seconds;
    public int needMin;
    public int needSec;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        timerText.text = string.Format("{0}:{1}", needMin.ToString("00"), needSec.ToString("00"));
        total_seconds = needMin * 60 + needSec;

        while (total_seconds > 0)
        {
            //等待一秒後執行
            yield return new WaitForSeconds(1);

            total_seconds -= 1;
            needSec -= 1;

            if(needSec < 0 && needMin > 0)
            {
                needMin -= 1;
                needSec = 59;
            }else if (needSec <0 && needMin == 0)
            {
                needSec = 0;
            }
            timerText.text = string.Format("{0}:{1}", needMin.ToString("00"), needSec.ToString("00"));
        }
    }
}
