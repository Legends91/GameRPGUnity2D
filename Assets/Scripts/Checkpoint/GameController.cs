using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameController : MonoBehaviour
{
    [SerializeField] private AudioSource CheckpointSFX;
    Vector2 diemSave;
    Rigidbody2D playerrb;
    //public Health health;
    public int satthuong;
    
    private void Awake()
    {
        
    }
    void Start()
    {
        diemSave = transform.position;
        playerrb = GetComponent<Rigidbody2D>();
    }
 //   private void OnTriggerEnter2D(Collider2D collision)
   // {
     //   if (collision.CompareTag("Chuongngaivat"))
       // {
         //   Die();
       // }    
   // }

    public void UpdatediemSave(Vector2 pos)
    {
        diemSave = pos;
     //   CheckpointSFX.Play();
    }
    public void Die()
    {
        StartCoroutine(Respawn(0f));
   //     health.ResetDeath();
    }

    IEnumerator Respawn(float thoigiancho)
    {
       // health.NhanST(satthuong);
        playerrb.simulated = false;
        playerrb.velocity = new Vector2(0, 0);
        //transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(thoigiancho);
        transform.position = diemSave;
        //transform.localScale = new Vector3(1, 1, 1);
        playerrb.simulated = true;
    }    
}
