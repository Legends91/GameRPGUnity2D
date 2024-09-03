using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timduong : MonoBehaviour
{
    [SerializeField] private float tocdodichuyen = 2f;

    private Rigidbody2D rb;
    private Vector2 huongdichuyen;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + huongdichuyen * (tocdodichuyen * Time.fixedDeltaTime));
    }
    public void Chuyentoi(Vector2 vitrichidinh)
    {
        huongdichuyen = vitrichidinh;
    }
}
