using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcHealth : MonoBehaviour
{
    [SerializeField] public float mauhientai;
    [SerializeField] public float mautoida = 10;
    [SerializeField] public FloatingHealth heathbar;
    [SerializeField] private Animator ani;
    [SerializeField] public float KBForce = 2;
    [SerializeField] public Collider2D col;
    [SerializeField] public NhanbietMuctieu nhanbietMuctieu;

    [SerializeField] public OrcHanhDong orchanhdong;
    [SerializeField] private PlayerStat player;
    void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
        ani = GetComponent<Animator>();
        heathbar = GetComponentInChildren<FloatingHealth>();
        mauhientai = mautoida;
        heathbar.UpdateHeathBar(mauhientai, mautoida);
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
    }

    private void Update()
    {
        LuongMau();
    }
    public void NhanST(float trumau)
    {
        mauhientai -= trumau;
        heathbar.UpdateHeathBar(mauhientai, mautoida);
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
            ani.SetTrigger("TakeDmg");
            NhanST(player.curAtt);
            orchanhdong.CoolDownAtkTime = 0.5f;
            if (mauhientai <= 0)
            {
                nhanbietMuctieu.colliders = null;
                ani.SetBool("isDeath", true);
                ani.SetTrigger("Death");
                col.enabled = false;
            }
        }
    }
}
