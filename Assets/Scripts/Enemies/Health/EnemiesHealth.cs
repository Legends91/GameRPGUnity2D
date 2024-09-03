using UnityEngine;

public class EnemiesHealth : MonoBehaviour
{
    [Header("------ Khai báo ------")]
    [SerializeField] public FloatingHealth heathbar;
    [SerializeField] private Animator ani;
    [SerializeField] public Collider2D col;
    [SerializeField] private Flash flashEffect;
    // [SerializeField] public NhanbietMuctieu nhanbietMuctieu;

    // [SerializeField] public Hanhdong hanhdong;
    // [SerializeField] private PlayerStat player;
    // [SerializeField] private GameObject popupDmg;
    // [SerializeField] private Text popupText;


    [Header("------ Máu quái ------")]
    [SerializeField] public float mauhientai;
    [SerializeField] public float mautoida = 3;
    [SerializeField] public float KBForce = 2;
    [SerializeField] public bool isDeath;
    

    
    void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
        ani = GetComponent<Animator>();
        heathbar = GetComponentInChildren<FloatingHealth>();
        flashEffect = GetComponent<Flash>();
        mauhientai = mautoida;
        heathbar.UpdateHeathBar(mauhientai, mautoida);
        //player = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
        //popupDmg = GameObject.FindWithTag("Dmg").GetComponent<GameObject>();
        //popupText = GameObject.FindWithTag("Dmg").GetComponent<Text>();
    }

    private void Update()
    {
        LuongMau();
    }
    public void NhanST(float trumau)
    {
        mauhientai -= trumau;
        //  popupText.text = trumau.ToString();
        heathbar.UpdateHeathBar(mauhientai, mautoida);
        flashEffect.FlashTrigger();

        //    Instantiate(popupDmg, transform.position, Quaternion.identity);
    }
    public void Death()
    {
        Destroy(gameObject);
    }

    public void LuongMau()
    {
        if (mauhientai > 0)
        {
            col.enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slash" & mauhientai > 0)
        {
            NhanST(1);

            //ani.SetTrigger("TakeDmg");
            //hanhdong.CoolDownAtkTime = 0.5f;
            if (mauhientai <= 0)
            {
                isDeath = true;
                //nhanbietMuctieu.colliders = null;
                ani.SetBool("isDeath", true);
                ani.SetTrigger("Death");
                col.enabled = false;
            }
        }
    }
}
