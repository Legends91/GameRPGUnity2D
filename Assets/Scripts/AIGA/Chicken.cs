using System.Collections;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [Header("------ Khai báo ------")]
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;

    [Header("------ Di chuyển ------")]
    [SerializeField] float Speed;
    [SerializeField] float Tamdichuyen;
    [SerializeField] float Tamtoida;
    [SerializeField] Vector2 Waypoint;
    [SerializeField] float MinWaitTime; // Thời gian chờ ngẫu nhiên tối thiểu
    [SerializeField] float MaxWaitTime; // Thời gian chờ ngẫu nhiên tối đa
    float StoreTime;
    Vector2 oldPosition;
    float direction;

    [Header("------ Hoạt Ảnh ------")]
    [SerializeField] bool isFlipping = false;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        SetWayPoint(); // Đặt waypoint ban đầu
    }

    void Update()
    {
        if (!isFlipping)
        {
            ChickenMove();
        }
        FlipSprite();
    }

    void ChickenMove()
    {
        if (StoreTime > 0)
        {
            StoreTime -= Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, Waypoint, Speed * Time.deltaTime);
        }
        else
        {
            if (Vector2.Distance(transform.position, Waypoint) < 0.1f)
            {
                StartCoroutine(WaitBeforeNextMove());
                animator.SetBool("isMoving", false);
            }
        }
    }
    private IEnumerator WaitBeforeNextMove()
    {
        // Đặt StoreTime ngẫu nhiên để chờ đợi
        StoreTime = Random.Range(MinWaitTime, MaxWaitTime);
        yield return new WaitForSeconds(StoreTime);

        // Sau khi chờ đợi, đặt waypoint mới
        SetWayPoint();
    }

    private void OnDrawGizmosSelected()
    {
        // Vẽ phạm vi di chuyển
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Tamdichuyen);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Tamtoida);

        // Vẽ waypoint hiện tại
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Waypoint, 0.2f);
    }

    private void FlipSprite()
    {
        float currentPositionX = transform.position.x;

        if (currentPositionX != oldPosition.x)
        {
            animator.SetBool("isMoving", true);
            direction = Mathf.Sign(currentPositionX - oldPosition.x);
            Vector3 OldlocalScale = transform.localScale;
            transform.localScale = new Vector3(direction, 1, 1);
            if (transform.localScale != OldlocalScale)
            {
                rb.velocity = Vector3.zero;
                isFlipping = true;
                animator.SetBool("isFlipping", isFlipping);
            }
        }
        oldPosition = transform.position;
    }
    private void NotFlipping()
    {
        isFlipping = false;
        animator.SetBool("isFlipping", isFlipping);
    }
    void SetWayPoint()
    {
        Waypoint = (Vector2)transform.position + Random.insideUnitCircle * Tamtoida;
        StoreTime = Vector2.Distance(transform.position, Waypoint) / Speed; // Đặt StoreTime cho lần di chuyển tiếp theo
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Khi xảy ra va chạm, chọn waypoint mới để tránh vật cản
        SetWayPoint();
    }
}
