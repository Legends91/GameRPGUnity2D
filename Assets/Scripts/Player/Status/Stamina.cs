using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [Header("------ Kế thừa ------")]
    [SerializeField] Flash flash;
    [SerializeField] Slider StaminaUI;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;
    [SerializeField] private Sprite[] spritesForImage1;
    [SerializeField] private Sprite[] spritesForImage2;
    //[SerializeField] TextMeshProUGUI ThanhChu;

    [Header("------ Thanh lực ------")]
    public float StaminaToiDa = 100;
    public float StaminaHienTai;
    public float StaminaTieuHao;
    public float StaminaHoiPhuc;
    [SerializeField] float TimeStaminaTieuHao;
    [SerializeField] float TimeStaminaHoiPhuc;
    [SerializeField] float CooldownStaminaHoiPhuc;
    [SerializeField] float fadeDuration = 0.5f;
    private void Awake()
    {
        StaminaUI = GameObject.FindGameObjectWithTag("Stamina").GetComponent<Slider>();
        canvasGroup = GameObject.FindGameObjectWithTag("Stamina").GetComponent<CanvasGroup>();
        flash = GameObject.FindGameObjectWithTag("PlayerStatus").GetComponent<Flash>();

        StaminaHienTai = StaminaToiDa;
        StaminaUI.maxValue = StaminaToiDa;
        StaminaUI.value = StaminaHienTai;
    }

    void Update()
    {
        StaminaConsume();
        ChangeImgs();
        if (StaminaHienTai < 0)
        {
            StaminaHienTai = 0;
        }
    }

    public void StaminaConsume()
    {
        if (StaminaHienTai < StaminaToiDa)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += fadeDuration * Time.deltaTime;
            }
        }
        else
        {
            if (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= fadeDuration * Time.deltaTime;
                StaminaHienTai = StaminaToiDa;
            }
        }

        StaminaUI.value = StaminaHienTai;
        if (StaminaTieuHao > 0)
        {
            StaminaTieuHao -= TimeStaminaTieuHao * Time.deltaTime;
            StaminaHienTai -= TimeStaminaTieuHao * Time.deltaTime;
            TimeStaminaHoiPhuc = CooldownStaminaHoiPhuc;
        }
        else
        {
            StaminaTieuHao = 0;
            StaminaReturn();
        }
    }
    void StaminaReturn()
    {
        if (TimeStaminaHoiPhuc > 0)
        {
            TimeStaminaHoiPhuc -= Time.deltaTime;
        }
        else if (StaminaHienTai < StaminaToiDa && TimeStaminaHoiPhuc <= 0)
        { 
            StaminaHienTai += StaminaHoiPhuc * Time.deltaTime;
            TimeStaminaHoiPhuc = 0;
        }
    }

    void ChangeImgs()
    {
        if (image1 != null)
        {
            int index = GetSpriteIndex((int)StaminaHienTai);
            if (index >= 0 && index < spritesForImage1.Length)
            {
                image1.sprite = spritesForImage1[(int)index];
            }
        }

        // Đổi hình ảnh của image2 dựa trên giá trị currentValue
        if (image2 != null)
        {
            int index = GetSpriteIndex((int)StaminaHienTai);
            if (index >= 0 && index < spritesForImage2.Length)
            {
                image2.sprite = spritesForImage2[index];
            }
        }
    }
    private int GetSpriteIndex(int value)
    {
        switch (value)
        {
            case 100: return 0;
            case 80: return 1;
            case 60: return 2;
            case 40: return 3;
            case 20: return 4;
            default: return -1; // Giá trị không hợp lệ
        }
    }
}
