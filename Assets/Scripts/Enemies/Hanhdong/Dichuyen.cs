using UnityEngine;

public class Dichuyen : MonoBehaviour
{
    private Rigidbody2D rb2d;
    //public Vukhi vukhi;
    [SerializeField]
    private float tocdotoida = 2, tangtoc = 50, giamtoc = 100;
    [SerializeField]
    private float tocdohientai = 0;
    [SerializeField]
    private Vector2 oldMovementInput;
    public Vector2 MovementInput { get; set; }
    [SerializeField]
    public GameObject targetObject;
    private Vector3 vitrihientai;
    [SerializeField]
    private Animator animator;
    
    public bool Move = true;
    public bool flip = true;
    void Start()
    {
        if (targetObject != null)
        {
            vitrihientai = targetObject.transform.position;
        }
        animator = GetComponent<Animator>();
    }
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (flip == true)
        {
            FlipSprite();
        }
        LockMove();
        UnlockMove();
        Movement();
    }

    public void Movement()
    {
        if (targetObject != null)
        {
            // Lấy vị trí của targetObject
            Vector3 currentPosition = targetObject.transform.position;
            // So sánh vị trí hiện tại với vị trí trước đó
            if (currentPosition != vitrihientai)
            {
                // Xác định liệu vị trí tăng lên hay giảm đi
                if (currentPosition.x > vitrihientai.x)
                {
                    animator.SetBool("isFacingRight", true);
                }
                else if (currentPosition.x < vitrihientai.x)
                {
                    animator.SetBool("isFacingRight", false);
                }

                // Cập nhật vị trí trước đó thành vị trí hiện tại
                vitrihientai = currentPosition;
            }

        }
        if (MovementInput.magnitude > 0 && tocdohientai >= 0 && Move == true)
        {
            //vukhi.SetWindow(false);
            animator.SetBool("Running", true);
            oldMovementInput = MovementInput;
            tocdohientai += tangtoc * tocdotoida * Time.deltaTime;

        }
        else
        {
            if (tocdohientai == 0)
            {
                animator.SetBool("Running", false);
            }
            else
            {
                tocdohientai -= giamtoc * tocdotoida * Time.deltaTime;
            }

        }
        tocdohientai = Mathf.Clamp(tocdohientai, 0, tocdotoida);
        rb2d.velocity = oldMovementInput * tocdohientai;
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
        if (Mathf.Abs(rb2d.velocity.x) > 0)
        {
            float direction = Mathf.Sign(rb2d.velocity.x);
            transform.localScale = new Vector3(direction, 1, 1);
        }

    }
    public void LockMove()
    {
        Move = false;
    }

    public void UnlockMove()
    {
        Move = true;
    }

}
