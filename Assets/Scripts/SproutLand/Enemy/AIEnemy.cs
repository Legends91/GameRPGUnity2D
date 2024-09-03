using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemy : MonoBehaviour
{
    [Header("------ Khai báo ------")]
    [SerializeField] Transform player;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] EnemiesHealth enemyhealth;

    [Header("------ Di chuyển ------")]
    public float moveSpeed = 2f;
    public float attackRange = 1.5f;
    public float detectionRange = 5f;
    private Vector2 movement;
    [SerializeField] bool isMoving;

    [Header("------ Điểm trở về ------")]
    private int currentPatrolIndex = 0;

    [Header("------ CoolDown Đánh ------")]
    [SerializeField] float CDAtk;


    private enum State
    {
        Patrol,
        Chase,
        Attack
    }
    private State currentState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyhealth = GetComponent<EnemiesHealth>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        patrolPoints = GameObject.FindGameObjectsWithTag("Patrol")
            .Select(go => go.transform).ToArray();
        navMeshAgent.speed = moveSpeed;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        if (navMeshAgent.isOnNavMesh)
        {
            currentState = State.Patrol;
            navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
            
        }
        else
        {
          //  Debug.LogError("NavMeshAgent is not on a NavMesh.");
        }
    }

    void Update()
    {
        if (enemyhealth.isDeath == false)
        {
            if (!navMeshAgent.isOnNavMesh)
            {
                //Debug.LogError("NavMeshAgent is not on a NavMesh.");
                return; // Ngừng script nếu NavMeshAgent không trên NavMesh
            }

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
           // Debug.Log("Enemy Position: " + transform.position);
           // Debug.Log("Distance to Player: " + distanceToPlayer);

            switch (currentState)
            {
                case State.Patrol:
                    if (distanceToPlayer <= detectionRange && !Physics2D.Linecast(transform.position, player.position))
                    {
                        currentState = State.Chase;
                    }
                    Patrol();
                    break;
                case State.Chase:
                    if (distanceToPlayer <= attackRange)
                    {
                        currentState = State.Attack;
                    }
                    else if (distanceToPlayer > detectionRange)
                    {
                        currentState = State.Patrol;
                        navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
                    }
                    ChasePlayer();
                    break;
                case State.Attack:
                    if (distanceToPlayer > attackRange)
                    {
                        currentState = State.Chase;
                    }
                    Attack();
                    break;
            }
            UpdateAnimation();
        }  
    }

    void Patrol()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.position);
    }

    void Attack()
    {
        if (CDAtk <= 0)
        {
            navMeshAgent.ResetPath();
            rb.velocity = Vector2.zero;
            animator.SetTrigger("Attack");
            CDAtk = 1.5f;
        }
    }

    void UpdateAnimation()
    {
        Vector2 velocity = navMeshAgent.velocity;
        if (velocity.x != 0 && velocity.y != 0)
        {
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            animator.SetFloat("X", velocity.x);
            animator.SetFloat("Y", velocity.y);
        }
        else
        {
            isMoving = false;
            animator.SetBool("isMoving", isMoving);
        }
        
        animator.SetFloat("Speed", velocity.sqrMagnitude);
        CDAtk -= Time.deltaTime;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
