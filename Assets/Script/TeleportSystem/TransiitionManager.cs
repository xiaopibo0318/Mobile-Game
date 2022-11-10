using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 繼承原本 Monobehavior且在Awake中使用Singleton
/// </summary>
public class TransiitionManager : Singleton<TransiitionManager>
{

    [SerializeField] private CanvasGroup fadeCanvasGroup;
    private float fadeDuration = 3;
    private bool isFade;

    [SerializeField] private GameObject player;

    //public static TransiitionManager Instance;




    public void TPGD2() => StartCoroutine(TeleportToGD2());

    private IEnumerator TeleportToGD2()
    {
        yield return Fade(1);

        player.transform.position = new Vector3(-117, -8, 0);

        yield return Fade(0);
    }


    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;
        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;

    }


    public void GoToMTKL() => StartCoroutine(EnterMTKL());
    private IEnumerator EnterMTKL()
    {
        yield return Fade(1);

        SceneManager.LoadScene(2);
        player.transform.position = new Vector3(0, 0, 0);

        yield return Fade(0);
    }



    public void GoToGD() => StartCoroutine(EnterGD());
    private IEnumerator EnterGD()
    {
        yield return Fade(1);

        SceneManager.LoadScene(3);
        player.transform.position = new Vector3(0, 0, 0);

        yield return Fade(0);
    }

}
