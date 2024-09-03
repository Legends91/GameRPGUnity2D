using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{
    [SerializeField] Image Yes;
    [SerializeField] Image No;
    private bool isFullscreen = false;

    void Start()
    {
        if (!PlayerPrefs.HasKey("isFullscreen"))
        {
            PlayerPrefs.SetInt("isFullscreen", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateButtonIcon();
        Screen.fullScreen = isFullscreen;
    }
    private void UpdateButtonIcon()
    {
        if (isFullscreen == false)
        {
            Yes.enabled = false;
            No.enabled = true;
        }
        else
        {
            Yes.enabled = true;
            No.enabled = false;
        }
    }

    public void Nhannut()
    {
        if (isFullscreen == false)
        {
            isFullscreen = true;
            Screen.fullScreen = true;
        }
        else
        {
            isFullscreen = false;
            Screen.fullScreen = false;
        }
        Save();
        UpdateButtonIcon();
    }
    private void Load()
    {
        isFullscreen = PlayerPrefs.GetInt("isFullscreen") == 1;
    }
    private void Save()
    {
        PlayerPrefs.SetInt("isFullscreen", isFullscreen ? 1 : 0);
    }
}
