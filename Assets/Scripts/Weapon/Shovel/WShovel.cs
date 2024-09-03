using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class WShovel : MonoBehaviour
{
    [Header("------ Khai báo ------")]
    [SerializeField] Animator animator;
    [SerializeField] P player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<P>();
        animator = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeCooldown();
        DirecPlayer();
    }

    void TimeCooldown()
    {
      /*  if (player.TimeSecond > 0)
        {
            animator.SetBool("Dig", true);
        }
        else animator.SetBool("Dig", false); */
    }

    void DirecPlayer()
    {
        if (player.xInput != 0 || player.yInput != 0)
        {

            animator.SetFloat("X", player.xInput);
            animator.SetFloat("Y", player.yInput);
        }
    }
}
