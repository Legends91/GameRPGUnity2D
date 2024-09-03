using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("------ Khai báo ------")]
    [SerializeField] Health playerHealth;

    [Header("------ Máu nhân vật ------")]
    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite halfHeart;
    [SerializeField] Sprite emptyHeart;


    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        UpdateHearts();
    }
    void Update()
    {
        UpdateHearts();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (playerHealth.LuongMauHienTai >= (i + 1) * 2)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (playerHealth.LuongMauHienTai == (i * 2) + 1)
            {
                hearts[i].sprite = halfHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
