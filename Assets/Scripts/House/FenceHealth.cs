using UnityEngine;

public class FenceHealth : MonoBehaviour
{
    public float mauhientai;
    public float mautoida = 10;
    public FloatingHealth heathbar;
    public Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
        heathbar = GetComponentInChildren<FloatingHealth>();
        mauhientai = mautoida;
    }

    // Update is called once per frame
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
        if (collision.gameObject.tag == "EnemySlash" & mauhientai > 0)
        {
            NhanST(5);
            if (mauhientai <= 0)
            {
                col.enabled = false;
            }
        }
    }
}
