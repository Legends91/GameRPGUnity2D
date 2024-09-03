using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NhanbietMuctieu : Nhanbiet
{
    [SerializeField]
    private float banKinhPhatHien = 5;

    [SerializeField]
    private LayerMask chuongngaivatLayerMask, objectLayerMask;

    [SerializeField]
    private bool showGizmos = false;

    public List<Transform> colliders;

    public bool attacking;
    public override void Detect(AIData aiData)
    {
        Collider2D playerCollider =
            Physics2D.OverlapCircle(transform.position, banKinhPhatHien, objectLayerMask);

        if (playerCollider != null)
        {
            Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit =
                Physics2D.Raycast(transform.position, direction, banKinhPhatHien, chuongngaivatLayerMask);

            if (hit.collider != null && (objectLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                Debug.DrawRay(transform.position, direction * banKinhPhatHien, Color.magenta);
                colliders = new List<Transform>() { playerCollider.transform };
            }
            else
            {
                colliders = null;
            }
        }
        else
        {
            colliders = null;
        }
        aiData.targets = colliders;
    }

    private void OnDrawGizmosSelected()
    {
        if (showGizmos == false)
            return;

        Gizmos.DrawWireSphere(transform.position, banKinhPhatHien);

        if (colliders == null)
            return;
        Gizmos.color = Color.magenta;
        foreach (var item in colliders)
        {
            Gizmos.DrawSphere(item.position, 0.3f);
        }
    }
}
