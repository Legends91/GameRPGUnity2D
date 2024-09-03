using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PMove : MonoBehaviour, IDataPersistence
{
    private Rigidbody2D rb;
    Animator animator;

    [Header("------ Di chuyển ------")]
    public float xInput;
    public float yInput;
    public float speedX, speedY;
    private bool isWalking = false;
    public bool AllowMove = true;
    public bool flip = true;

    /* public bool attacking;
     public int combo;
     public int comboNumber;
     public float comboTiming;
     public float comboTempo; */

    [Header("------ Test ------")]
    public float KBforce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KBfromRight;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
     void Update()
    {
        
        if (flip == true)
        {
            FlipSprite();
        }
        CheckMove();
    }
    
    public void Movement(InputAction.CallbackContext x)
    {
        
        xInput = x.ReadValue<Vector2>().x * speedX;
        yInput = x.ReadValue<Vector2>().y * speedY;
        
        /* 
         rb.velocity = moveDir * speed * Time.deltaTime;
         moveDir = new Vector3(speedX, speedY).normalized;
         if (AllowMove == true)
         {
             if (KBCounter <= 0)
             {
                 rb.velocity = new Vector2(speedX, speedY);
             }
             else
             {
                 if (KBfromRight == true)
                 {
                     rb.velocity = new Vector2(-KBforce, KBforce);
                 }
                 if (KBfromRight == false)
                 {
                     rb.velocity = new Vector2(KBforce, KBforce);
                 }
                 KBCounter -= Time.deltaTime;
             }
             if (speedX != 0 || speedY != 0)
             {
                 animator.SetFloat("X", speedX);
                 animator.SetFloat("Y", speedY);
                 if (!isMoving)
                 {
                     isMoving = true;
                     animator.SetBool("IsMoving", isMoving);
                 }
             }
             else
             {
                 if (isMoving)
                 {
                     isMoving = false;
                     animator.SetBool("IsMoving", isMoving);
                     DontMove();
                 }
             }

             if (speedX > 0)
             {
                 animator.SetBool("isFacingRight", true);
             }
             else if (speedX < 0)
             {
                 animator.SetBool("isFacingRight", false);
             }
         }
        */
    }
    public void CheckMove()
    {
       
        if (AllowMove == true)
        {
            rb.velocity = new Vector2(xInput * speedX, yInput * speedY);
            if (xInput != 0 || yInput != 0)
            {
                animator.SetFloat("X", xInput);
                animator.SetFloat("Y", yInput);
                isWalking = true;
                animator.SetBool("IsMoving", isWalking);
            }
            else
            {
                animator.SetFloat("X", xInput);
                animator.SetFloat("Y", yInput);
                isWalking = false;
                animator.SetBool("IsMoving", isWalking);
                DontMove();
            }

            if (xInput > 0)
            {
                animator.SetBool("isFacingRight", true);
            }
            else if (xInput < 0)
            {
                animator.SetBool("isFacingRight", false);
            }
        }
    }
    public void AllowFlip()
    {
        flip = true;
    }
    public void DontAllowFlip()
    {
        flip = false;
    }
    private void FlipSprite()
    {
        if (Mathf.Abs(rb.velocity.x) > 0)
        {
            float direction = Mathf.Sign(rb.velocity.x);
            transform.localScale = new Vector3(direction, 1, 1);
        }

    }

    //Nếu đánh thì ngừng di chuyển và set chỉ số di chuyển về 0 (không bị trượt đi 1 đoạn) đồng thời set kiểm tra di chuyển về false
    public void StopMove()
    {
        AllowMove = false;
        isWalking = false;
        animator.SetBool("IsMoving", isWalking);
    }
    public void Move()
    {
        AllowMove = true;
    }
    public void DontMove()
    {
        rb.velocity = Vector3.zero;
    }
    public void LoadData(GameData data)
    {
        this.transform.position = data.targetPosition;
    }
    public void SaveData(GameData data)
    {
        data.targetPosition = this.transform.position;
    }
}
