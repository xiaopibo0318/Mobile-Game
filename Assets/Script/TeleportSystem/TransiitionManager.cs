﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransiitionManager : MonoBehaviour
{

    [SerializeField] private CanvasGroup fadeCanvasGroup;
    private float fadeDuration = 3;
    private bool isFade;

    [SerializeField] private GameObject player;

    public static TransiitionManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }


    public void TPGD2() => StartCoroutine(TeleportToGD2());

    private IEnumerator TeleportToGD2()
    {
        yield return Fade(1);

        player.transform.position = new Vector3(-100, 0, 0);

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

}
