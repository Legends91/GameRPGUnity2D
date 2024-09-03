using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class WeaponHold : MonoBehaviour
{
    [Header("------ Khai báo ------")]
    [SerializeField] private Animator animator;
    [SerializeField] private P player;
    [SerializeField] private PlayerInput input;


    [Header("------ Chặt cây ------")]
    [SerializeField] public bool Axe;
    [SerializeField] public bool Chop;

    [Header("------ Xới đất ------")]
    [SerializeField] public bool Shovel;

    [Header("------ Tấn công ------")]
    [SerializeField] public bool Sword;
    [SerializeField] public bool Swing;

    [Header("------ CoolDown ------")]
    [SerializeField] float ThoiGianCho;
    [SerializeField] float TimeSecond;

    [Header("------ Lưu di chuyển của player ------")]
    [SerializeField] public float xInputP;
    [SerializeField] public float yInputP;

    [Header("------ Hiệu ứng âm thanh ------")]
    [SerializeField] public AudioSource SlashSFX;
    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<P>();
        SlashSFX = GameObject.FindGameObjectWithTag("PlayerSound").GetComponent<AudioSource>();

    }
    void Update()
    {
        if (transform.childCount < 1)
        {
            CheckActiveWeapon();
        }

        ThoiGian();

        if (player.xInput != 0 || player.yInput != 0)
        {
            xInputP = player.xInput;
            yInputP = player.yInput;
        }
    }

    public void CheckActiveWeapon()
    {
        Sword = false;
        Axe = false;
        Shovel = false;
    }

    void ThoiGian()
    {
        if (ThoiGianCho > 0)
        {
            ThoiGianCho -= Time.deltaTime;
            animator.SetFloat("CoolAct", ThoiGianCho);
        }
        else
        {
            ThoiGianCho = 0;
            animator.SetFloat("CoolAct", ThoiGianCho);
        }
    }
    
    public void Action(InputAction.CallbackContext action)
    {
        if (action.performed && Sword && ThoiGianCho <= 0)
        {
            Swing = true;
            animator.SetTrigger("Swing");
            SlashSFX.Play();
            ThoiGianCho = TimeSecond;
        }

        if (action.performed && Shovel && ThoiGianCho <= 0)
        {
            
        }

        if (action.performed && Axe && ThoiGianCho <= 0)
        {
            Chop = true;
            animator.SetTrigger("Chop");
            SlashSFX.Play();
            ThoiGianCho = TimeSecond;
        }
    }
  

   
}
