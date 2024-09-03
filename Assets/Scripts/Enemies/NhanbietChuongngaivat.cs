using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NhanbietChuongngaivat : Nhanbiet
{
    [SerializeField]
    private float banKinhPhatHien = 2;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private bool showGizmos = true;

    Collider2D[] colliders;

    public override void Detect(AIData aiData)
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, banKinhPhatHien, layerMask);
        aiData.chuongngaivat = colliders;
    }

    private void OnDrawGizmos()
    {
        if (showGizmos == false)
            return;
        if (Application.isPlaying && colliders != null)
        {
            Gizmos.color = Color.red;
            foreach (Collider2D chuongngaivatcollider in colliders)
            {
                Gizmos.DrawSphere(chuongngaivatcollider.transform.position, 0.2f);
            }
        }
    }
}
