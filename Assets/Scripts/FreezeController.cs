using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeController : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    CanvasGroup group;

    [SerializeField] float fadeTime = 0.5f;
    bool gamePaused;

    private void Awake()
    {
        group = canvas.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        group.alpha = 0f;
        group.interactable = false;
        group.blocksRaycasts = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Freeze();
        }
    }

    private void Freeze()
    {
        if (!gamePaused)
        {
            Time.timeScale = 0f;
            StartCoroutine(FadeCanvas(0f, 1f));
            gamePaused = true;
        }
        else
        {
            UnFreeze();
        }
    }

    public void UnFreeze()
    {
        Time.timeScale = 1f;
        StartCoroutine(FadeCanvas(1f, 0f));
        gamePaused = false;
    }

    IEnumerator FadeCanvas(float from, float to)
    {
        float elapsedTime = 0f;
        group.interactable = true;
        group.blocksRaycasts = true;

        while (elapsedTime < fadeTime)
        {
            group.alpha = Mathf.Lerp(from, to, elapsedTime / fadeTime);
            elapsedTime += Time.unscaledDeltaTime; //Para cuando el timeScale = 0f
            yield return null;
        }

        group.alpha = to;

        if (to == 0f)
        {
            group.interactable = false;
            group.blocksRaycasts = false;
        }
    }
}
