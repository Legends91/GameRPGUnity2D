using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameController gameController;
    public Transform diemhoisinh;

    SpriteRenderer spriteRenderer;
    public Sprite passive, active;
    

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameController.UpdatediemSave(diemhoisinh.position);
            spriteRenderer.sprite = active;
        }
    }

}
