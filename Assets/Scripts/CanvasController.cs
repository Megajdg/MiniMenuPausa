using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject pause;
    [SerializeField] GameObject options;
    public void ChangeScene()
    {
        if (pause.activeSelf)
        {
            options.SetActive(true);
            pause.SetActive(false);
        }
        else
        {
            options.SetActive(false);
            pause.SetActive(true);
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
