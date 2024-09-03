using UnityEngine;

public class Org2 : MonoBehaviour
{
    [Header("----- Khai báo -----")]
    public OrcHanhDong OrcHanhDong;
    public Animator Anyma;
    private Transform player;
    [Header("----- Thông số -----")]
    public float tocdodichuyen;
    public float tamphathien;
    public float tamdanh1;
    public float tamdanh2;
    public float tamdanh3;
    [Header("----- Kiểm tra -----")]
    bool isFacingRight = false;
    float horizontalInput;


    private bool AtkCoolDown;
    float distanceFromPlayer;
    private bool move;

    void Start()
    {
        Anyma = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }
    private void Update()
    {
        FlipSprite();
        Move();
    }

    void Move()
    {
        distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < tamphathien && distanceFromPlayer > tamdanh1 && move == true)
        {
            Anyma.SetBool("Running", true);
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, tocdodichuyen * Time.deltaTime);
        }
        else if (distanceFromPlayer <= tamdanh1 && distanceFromPlayer > tamdanh2 && AtkCoolDown == false)
        {
            Anyma.SetBool("Running", false);
            Anyma.SetTrigger("Atk1");
        }
        else if (distanceFromPlayer <= tamdanh2 && distanceFromPlayer > tamdanh3 && AtkCoolDown == false)
        {
            Anyma.SetBool("Running", false);
            Anyma.SetTrigger("Atk2");
        }
        else if (distanceFromPlayer <= tamdanh3 && AtkCoolDown == false)
        {
            Anyma.SetBool("Running", false);
            Anyma.SetTrigger("Atk3");
        }
        else { Anyma.SetBool("Running", false); }
    }
    void MoveStop()
    {
        move = false;
    }
    void AllowMove()
    {
        move = true;
    }
    void AtkCool()
    {
        AtkCoolDown = true;
    }

    void AtkNotCool()
    {
        AtkCoolDown = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, tamphathien);
        Gizmos.DrawWireSphere(transform.position, tamdanh1);
        Gizmos.DrawWireSphere(transform.position, tamdanh2);
        Gizmos.DrawWireSphere(transform.position, tamdanh3);
    }

    void FlipSprite()
    {
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        transform.localScale = new Vector3(direction, 1, 1);
    }

}
