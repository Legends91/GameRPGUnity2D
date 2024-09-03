using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DayNightScript : MonoBehaviour, IDataPersistence
{
    public TextMeshProUGUI timeDisplay;
    public TextMeshProUGUI dayDisplay;
    public Volume ppv;

    public float giay;
    public float tocdo;
    public int phut;
    public int gio;
    public int ngay = 1;
    public int ngayhientai;
    public GameObject circle;

    public bool activateLights;
    public GameObject[] lights;
    public SpriteRenderer[] stars;

    private float lastUpdateTime = 0f;

    void Start()
    {
        ppv = gameObject.GetComponent<Volume>();
        lastUpdateTime = Time.time;
    }

    void Update()
    {
        float deltaTime = Time.time - lastUpdateTime;
        lastUpdateTime = Time.time;

        CalcTime(deltaTime);
        DisplayTime();
        ngay = ngayhientai;

        RotateCircle();
        if (ngay >= 10)
        SceneManager.LoadSceneAsync("GameOver");
    }

    public void CalcTime(float deltaTime)
    {
        float giay = deltaTime * tocdo;

        phut += Mathf.FloorToInt(giay / 60);
        gio += Mathf.FloorToInt(phut / 60);
        ngayhientai += Mathf.FloorToInt(gio / 24);

        phut %= 60;
        gio %= 24;

        ControlPPV();
    }

    public void ControlPPV()
    {
        // Tính tỉ lệ của ppv.weight dựa trên thời gian trong ngày
        float timeRatio;
        if (gio >= 12) 
        {
            timeRatio = (gio - 12) / 12f + phut / (12f * 60f);
        }
        else 
        {
            timeRatio = 1f - (gio/ 12f + phut / (12f * 60f));
        }

   
        ppv.weight = Mathf.Clamp01(timeRatio);

        
        if (gio == 18 && phut > 45) 
        {
            if (!activateLights)
            {
                foreach (GameObject lightObject in lights)
                {
                    lightObject.SetActive(true);
                }
                activateLights = true;
            }
        }
        else if (gio == 6 && phut > 45) 
        {
            if (activateLights)
            {
                foreach (GameObject lightObject in lights)
                {
                    lightObject.SetActive(false);
                }
                activateLights = false;
            }
        }
    }

    public void DisplayTime()
    {
        timeDisplay.text = string.Format("{0:00}:{1:00}", gio, 00);
        dayDisplay.text = "DAY: " + ngay;
    }

    void RotateCircle()
    {
        // Tính góc quay dựa trên giờ và phút hiện tại
        float rotationAngle = (gio * 60 + phut) * 360 / (24 * 60);

        // xoay từ góc tính được
        Quaternion targetRotation = Quaternion.Euler(0, 0, rotationAngle);

        // Gán xoay cho circle
        circle.transform.rotation = targetRotation;
    }
    public void Skip1Day()
    {
        ngayhientai++;
    }

    public void LoadData(GameData data)
    {
        this.ngayhientai = data.ngay;
    }

    public void SaveData(GameData data)
    {
        data.ngay = this.ngayhientai;
    }
}
