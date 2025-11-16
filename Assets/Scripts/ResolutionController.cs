using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionController : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolution;
    [SerializeField] Toggle fullscreen;

    private Resolution[] resolutions;
    private List<Resolution> resolutionList = new List<Resolution>();

    private const string resKey = "SelectedRes";
    private const string FScreenKey = "FullScreen";

    private void Start()
    {
        resolutions = Screen.resolutions;

        HashSet<string> seen  = new HashSet<string>();
        List<string> options = new List<string>();

        foreach (Resolution res in resolutions) {
            string key = res.width + "x" + res.height;
            if (!seen.Contains(key))
            {
                seen.Add(key);
                resolutionList.Add(res);
                options.Add(key);
            }
        }

        resolution.ClearOptions();
        resolution.AddOptions(options);

        int savedRes = PlayerPrefs.GetInt(resKey, resolutionList.Count - 1);
        resolution.value = savedRes;
        resolution.RefreshShownValue();

        bool inFullScreen = PlayerPrefs.GetInt(FScreenKey, 1) == 1;
        fullscreen.isOn = inFullScreen;
        ApplyResolution(savedRes, inFullScreen);

        resolution.onValueChanged.AddListener(OnResChanged);
        fullscreen.onValueChanged.AddListener(OnFScreenToggled);
    }

    private void ApplyResolution(int i, bool inFullScreen)
    {
        Resolution res = resolutions[i];
        Screen.SetResolution(res.width, res.height, inFullScreen);
    }

    private void OnFScreenToggled(bool isFScreen)
    {
        ApplyResolution(resolution.value, isFScreen);
        PlayerPrefs.SetInt(FScreenKey, isFScreen ? 1 : 0);
    }

    private void OnResChanged(int i)
    {
        ApplyResolution(i, fullscreen.isOn);
        PlayerPrefs.SetInt(resKey, i);
    }
}
