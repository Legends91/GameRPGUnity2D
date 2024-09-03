using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dovat : MonoBehaviour
{
    Transform player;
    [SerializeField] float tocdo = 5f;
    [SerializeField] float khoangcachnhat = 1.5f;
    [SerializeField] float tgbienmat = 10f;
    private void Awake()
    {
        //player = GameManager.instance.player.transform;
    }

    private void Update()
    {
        tgbienmat -= Time.deltaTime;
        if (tgbienmat < 0) { Destroy(gameObject); }

        float khoangcach = Vector3.Distance(transform.position, player.position);
        if(khoangcach > khoangcachnhat)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            tocdo * Time.deltaTime
            );

        if(khoangcach < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
