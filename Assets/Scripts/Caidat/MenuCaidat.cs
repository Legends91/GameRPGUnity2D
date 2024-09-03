using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuCaidat : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider slider;

    [System.Obsolete]
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Amluong", 0.75f); // thanh truot cua am luong

        int savedQualityLevel = PlayerPrefs.GetInt("ChatLuong", QualitySettings.GetQualityLevel());
        QualitySettings.SetQualityLevel(savedQualityLevel);

    }

    public void ChinhAmluong(float amluong)
    {
        float sliderValue = slider.value;
        audioMixer.SetFloat("Amluong", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("Amluong", sliderValue);
        PlayerPrefs.Save();
    }
    public void ChinhDohoa(int chatluong)
    {
        QualitySettings.SetQualityLevel(chatluong);
        PlayerPrefs.SetInt("ChatLuong", chatluong);
        PlayerPrefs.Save();
    }
}
