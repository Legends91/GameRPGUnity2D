using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hanhdong : MonoBehaviour
{
    private Animation Animations;
    private Dichuyen agentMover;
    private Transform player;
    private Animator Anyma;
    public float CoolDownAtkTime;
    public bool CoolDownAtk;
    public float tamban;
    public float distanceFromPlayer;
    public float distanceFromFence;

    //private Vukhi weaponParent;

    private Vector2 pointerInput, movementInput;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    private void Update()
    {

        distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (CoolDownAtkTime > 0)
        {
            CoolDownAtkTime -= Time.deltaTime;
        }
        //pointerInput = GetPointerInput();
        //movementInput = movement.action.ReadValue<Vector2>().normalized;
        agentMover.MovementInput = MovementInput;
        //weaponParent.PointerPosition = pointerInput;
        // AnimateCharacter();

        if (distanceFromPlayer <= tamban && CoolDownAtkTime <= 0)
            Anyma.SetTrigger("Atk");
    }


    public void PerformAttack()
    {
    }

    private void Awake()
    {
        Anyma = GetComponent<Animator>();
        Animations = GetComponentInChildren<Animation>();
        //weaponParent = GetComponentInChildren<Vukhi>();
        agentMover = GetComponent<Dichuyen>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        //  Animations.RotateToPointer(lookDirection);
        Animations.PlayAnimation(MovementInput);
    }
    public void CooldownAttack()
    {
        CoolDownAtkTime = 1.5f;
        CoolDownAtk = false;
    }
    public void Attacking()
    {
        CoolDownAtk = true;
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, tamban);
    }
}
