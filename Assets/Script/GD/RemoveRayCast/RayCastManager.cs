using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RayCastManager : Singleton<RayCastManager>, IPointerDownHandler
{
    [SerializeField] private GameObject[] rayCastObject;
    private bool isTrigger = false;
    private Coroutine coroutine;
    private Coroutine lightCoroutine;

    public GameObject playerObject { get; set; }

    [Header("警報")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    private float fadeDuration = .5f;

    [Header("操作介面")]
    private GameObject lineNeedToDelete = null;
    private bool isPassFirst = true;
    [SerializeField] private Button goBackButton;

    private void Start()
    {
        goBackButton.onClick.AddListener(delegate { CanvasManager.Instance.openCanvas("original"); });
        isPassFirst = true;
        //fadeCanvasGroup.blocksRaycasts = false;
        fadeCanvasGroup.gameObject.GetComponent<Image>().raycastTarget = false;
    }

    public void TriggerRayCast()
    {
        if (isTrigger) return;
        SiginalUI.Instance.SiginalText("觸碰到紅外線\n請在10秒內到安全區域\n解除警報");
        //AudioManager.Instance.XXXX;
        isTrigger = true;
        coroutine = StartCoroutine(StartWarning());
        lightCoroutine = StartCoroutine(RedLight(1));
    }

    public void HandleWarning()
    {
        isTrigger = false;
        StopCoroutine(coroutine);
        StopCoroutine(lightCoroutine);
        fadeCanvasGroup.alpha = 0;
        SiginalUI.Instance.SiginalText("成功解除警報");
    }

    IEnumerator StartWarning()
    {
        float time = 10;
        while (time > 0)
        {
            yield return null;
            time -= Time.deltaTime;
        }

        if (playerObject != null)
        {
            SiginalUI.Instance.SiginalText("你被自動保全系統發現\n被抓到了宮殿大廳");
            playerObject.transform.position = Vector3.zero;
        }
        isTrigger = false;
    }

    private IEnumerator RedLight(float targetAlpha)
    {
        fadeCanvasGroup.blocksRaycasts = false;
        while (isTrigger)
        {
            float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;

            while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
            {
                fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
                yield return null;
            }
            if (targetAlpha > 0.9) targetAlpha = 0;
            else targetAlpha = 1;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"碰到的物品" + eventData.pointerCurrentRaycast.gameObject.name);
        if (eventData.pointerCurrentRaycast.gameObject.name.Contains("linePrefab") && isPassFirst)
        {
            lineNeedToDelete = eventData.pointerCurrentRaycast.gameObject;
            SiginalUI.Instance.TextInterectvie("請問要拔除這條線嗎？", DeleteOneLine);

        }
        if (eventData.pointerCurrentRaycast.gameObject.name.Contains("ElectricLine") && !isPassFirst)
        {
            lineNeedToDelete = eventData.pointerCurrentRaycast.gameObject;
            SiginalUI.Instance.TextInterectvie("請問要拔除這條線嗎？", DeleteElectricLine);
        }
    }


    private void DeleteOneLine()
    {
        Destroy(lineNeedToDelete);
        SiginalUI.Instance.SiginalText("斷電成功");
        rayCastObject[0].SetActive(false);
        rayCastObject[2].SetActive(false);
        isPassFirst = false;
        CanvasManager.Instance.openCanvas("original");
        GDMananger.Instance.gameStatus = 7;
        GDMananger.Instance.UpdateMap();
    }

    private void DeleteElectricLine()
    {
        Destroy(lineNeedToDelete);
        SiginalUI.Instance.SiginalText("斷電成功");
        rayCastObject[1].SetActive(false);
        rayCastObject[3].SetActive(false);
        CanvasManager.Instance.openCanvas("original");
        GDMananger.Instance.gameStatus = 8;
        GDMananger.Instance.UpdateMap();
    }

}
