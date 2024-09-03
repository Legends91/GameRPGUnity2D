using UnityEngine;

public class Chatcay : Congcu
{
    [SerializeField] GameObject roido;
    [SerializeField] int soluongroi = 5;
    [SerializeField] float dientich = 0.7f;
    [SerializeField] P Player;

    [Header("------ Hoạt Ảnh ------")]
    [SerializeField] Animator animator;
    [SerializeField] float TimeGrow;
    [SerializeField] bool Growing;
    [SerializeField] int SoLan;
    [SerializeField] bool KichHoatChatCay;
    [SerializeField] bool DeadTree;
    private void Start()
    {
        TimeGrow = 10;
        animator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<P>();
    }
    void Update()
    {
        if (TimeGrow > 0)
        {
            TimeGrow -= Time.deltaTime;
            animator.SetFloat("Time", TimeGrow);
        }
        if (Player.xInput != 0)
        {
            animator.SetFloat("X", Player.xInput);
        }
        
    }
    public void Grow()
    {
        Growing = true;
        animator.SetBool("Growing", Growing);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Chop" && Growing == true) 
        {
            animator.SetBool("ChatCay", true);
            KichHoatChatCay = true;
            if (KichHoatChatCay == true && SoLan <= 0)
            {
                SoLan = 3;
                animator.SetInteger("SoLan", SoLan);
            }
            if (DeadTree == true)
            {
                animator.SetBool("Dead", DeadTree);
                DeadTree = false;
            }
        }
    }
    public override void Dap()
    {
        if (Growing == true) 
        {
            
        }
    }

    public void CoolDownChatCay()
    {
        animator.SetBool("ChatCay", false);
        SoLan -= 1;
        animator.SetInteger("SoLan", SoLan);
    }

    public void Drop()
    {
        while (soluongroi > 0)
        {
            soluongroi -= 1;
            Vector3 vitri = transform.position;
            vitri.x += dientich * UnityEngine.Random.value - dientich / 2;
            vitri.y += dientich * UnityEngine.Random.value - dientich / 2;
            GameObject go = Instantiate(roido);
            go.transform.position = vitri;
            DeadTree = true;
        }
    }
    

    public void Dead()
    {
        soluongroi = 1;
        while (soluongroi > 0)
        {
            soluongroi -= 1;
            Vector3 vitri = transform.position;
            vitri.x += dientich * UnityEngine.Random.value - dientich / 2;
            vitri.y += dientich * UnityEngine.Random.value - dientich / 2;
            GameObject go = Instantiate(roido);
            go.transform.position = vitri;
        }
        Destroy(gameObject);
    }
}
