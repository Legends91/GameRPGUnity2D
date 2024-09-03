
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    Rigidbody2D rgdbody2;
    [SerializeField] float khoangcach = 1f;
    [SerializeField] float khuvuctuongtac = 1.2f;

    private void Awake()
    {
        rgdbody2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }

    public void Sdcongcu()
    {
        Vector2 vitri = rgdbody2.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(vitri, khoangcach);
        foreach (Collider2D c in colliders)
        {
            Congcu dap = c.GetComponent<Congcu>();
            if(dap != null)
            {
                dap.Dap();
                break;
            }
        }
    }
}
