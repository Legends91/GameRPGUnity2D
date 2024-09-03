using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Dophangiai : MonoBehaviour
{
    [SerializeField] private Dropdown dophangiaiDrop;    

    private Resolution[] DPGs;
    private List<Resolution> dophangiaiList;

    private int DPGhientai = 0;
    [HideInInspector] public Resolution dophangiai;
    private void Start()
    {
        DPGs = Screen.resolutions;
        dophangiaiList = new List<Resolution>();

        dophangiaiDrop.ClearOptions();

        for (int i = 0; i < DPGs.Length; i++)
        {
            if (!dophangiaiList.Any(x => x.width == DPGs[i].width && x.height == DPGs[i].height)) 
            {
                dophangiaiList.Add(DPGs[i]);  
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < dophangiaiList.Count; i++)
        {
            string resolutionOption = dophangiaiList[i].width + " x " + dophangiaiList[i].height;
            options.Add(resolutionOption);
            if (dophangiaiList[i].width == Screen.width && dophangiaiList[i].height == Screen.height)
            {
                DPGhientai = i;
            }
        }

        dophangiaiDrop.AddOptions(options);
        dophangiaiDrop.value = DPGhientai;
        dophangiaiDrop.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        dophangiai = dophangiaiList[resolutionIndex];
        if (PlayerPrefs.GetInt("ScreenMode") == 0)
        {
            Screen.SetResolution(dophangiai.width, dophangiai.height, true);

            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        if (PlayerPrefs.GetInt("ScreenMode") == 1)
        {
            Screen.SetResolution(dophangiai.width, dophangiai.height, true);
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        if (PlayerPrefs.GetInt("ScreenMode") == 2)
        {
            Screen.SetResolution(dophangiai.width, dophangiai.height, false);
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}