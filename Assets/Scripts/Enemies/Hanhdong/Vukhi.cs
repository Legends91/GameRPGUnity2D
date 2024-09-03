using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vukhi : MonoBehaviour
{
    //public SpriteRenderer characterRenderer;
    //public SpriteRenderer weaponRenderer;
    public Vector2 PointerPosition { get; set; }

    //public Animator animator;
    public Animator aniEnemy;
    public float delay = 0.3f;
    private bool attackBlocked;
    //public GameObject weaponhold;

    public bool IsAttacking { get; private set; }

    public Transform circleOrigin;
    public float radius;
    public PMove PMove;

    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    private void Update()
    {
        if (IsAttacking)
            return;
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

      /*  Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -1;
        }
        else if (direction.x > 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        } */
    }

    public void Attack()
    {
        //weaponhold.SetActive(true);
        if (attackBlocked)
            return;
        //animator.SetTrigger("SwordATK");
        aniEnemy.SetTrigger("Atk");
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
    }


    

    /* public void SetWindow(bool weapon1)
    {
        weaponhold.SetActive(weapon1);
    } */
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Attack();
        }
    }
    public void DetectColliders()
    {/*
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            Health health;
            if (health = collider.GetComponent<Health>())
            {
                health.NhanST(1);
            }
        }*/
    } 


}
