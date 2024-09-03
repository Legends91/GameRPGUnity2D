using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIQuai : MonoBehaviour
{
    private enum Trangthai
    {
        Dichuyen
    }

    private Trangthai trangthai;
    private Timduong timduong;

    private void Awake()
    {
        timduong = GetComponent<Timduong>();
        trangthai = Trangthai.Dichuyen;
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while (trangthai == Trangthai.Dichuyen)
        {
            Vector2 vitridichuyen = GetVitridichuyen();
            timduong.Chuyentoi(vitridichuyen);
            yield return new WaitForSeconds(2f);
        }
    }

    private Vector2 GetVitridichuyen()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
