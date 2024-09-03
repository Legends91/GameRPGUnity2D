using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("------ Khai báo ------")]
    [SerializeField] public Collider2D col;
    [SerializeField] private Flash flashEffect;

    [Header("------ Máu nhân vật ------")]
    public int LuongMauToiDa;
    public int LuongMauHienTai;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        flashEffect = GetComponent<Flash>();
        LuongMauHienTai = LuongMauToiDa;
    }
    void Update()
    {
        PLuongMau();
    }


    public void PLuongMau()
    {
        col.enabled = LuongMauHienTai > 0;
        if (LuongMauHienTai <= 0)
        {
            SceneManager.LoadSceneAsync("GameOver");
        }
    }

    public void PNhanST(int satthuong)
    {
        LuongMauHienTai -= satthuong;
        flashEffect.FlashTrigger();
        //  popupText.text = trumau.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GoblinAtk" & LuongMauHienTai > 0)
        {
            Debug.Log("Đánh trúng!");
            PNhanST(1);



        }
    }
}
