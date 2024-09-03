using UnityEngine;
using UnityEngine.InputSystem;

public class P : MonoBehaviour, IDataPersistence

{
    [Header("------ Khai báo ------")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] WeaponHold weaponhold;
    [SerializeField] ToolController toolController;
    [SerializeField] SGridMap gridMap;
    [SerializeField] ReplaceTile replaceTile;
    [SerializeField] Stamina stamina;

    [Header("------ Di chuyển ------")]
    [SerializeField] public float xInput;
    [SerializeField] public float yInput;
    [SerializeField] float speedX, speedY;
    private bool isMoving = false;
    [SerializeField] bool AllowMove = true;
    [SerializeField] bool flip = true;
    [SerializeField] bool checkshift = false;
    [SerializeField] float TimeMove;

    [Header("------ Hiệu ứng âm thanh ------")]
    [SerializeField] public AudioSource runSFX;
    [SerializeField] public AudioSource walkSFX;
    [SerializeField] public AudioSource digSFX;
    [SerializeField] public float CooldownSFX;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        weaponhold = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<WeaponHold>();
        stamina = GameObject.FindGameObjectWithTag("PlayerStatus").GetComponent<Stamina>();

        toolController = GameObject.FindGameObjectWithTag("ChatCay").GetComponent<ToolController>();
        
        gridMap = GameObject.FindGameObjectWithTag("Highlight").GetComponent<SGridMap>();
        replaceTile = GameObject.FindGameObjectWithTag("Highlight").GetComponent<ReplaceTile>();
    }
    // Update is called once per frame
    void Update()
    {
        CheckMove();
        
        ThoiGianSFX();
        if (checkshift == true && weaponhold.Swing == false && weaponhold.Chop == false)
        {
            speedX = 3;
            speedY = 3;
            stamina.StaminaTieuHao += 0.1f * Time.deltaTime;
            animator.SetBool("Shift", true);
        }
        if (stamina.StaminaHienTai <= 0 || checkshift == false || weaponhold.Swing == true || weaponhold.Chop == true)
        {
            speedX = 1;
            speedY = 1;
            animator.SetBool("Shift", false);
        }
    }

    public void ShiftRun(InputAction.CallbackContext shift)
    {
        if (shift.performed && stamina.StaminaHienTai > 0)
        {
            animator.SetBool("Shift", true);
            checkshift = true;
        }
        if (shift.canceled)
        {
            animator.SetBool("Shift", false);
            checkshift = false;
        }
        
    }
    public void Movement(InputAction.CallbackContext x)
    {
        Vector2 input = x.ReadValue<Vector2>();
        xInput = input.x;
        yInput = input.y;
    }
    public void Dig(InputAction.CallbackContext dig)
    {
       /* if (dig.performed && TimeSecond <= 0)
        {
            digSFX.Play();
            animator.SetTrigger("Dig");
           
            DontMove();
            toolController.Sdcongcu();
            // Gọi phương thức thay thế RuleTile
            //Vector3Int position = gridMap.GetPreviousMousePosition();
            //replaceTile.TileReplacer(position);
        } */
    }
    void ThoiGianSFX()
    {
        if (CooldownSFX > 0)
        {
            CooldownSFX -= Time.deltaTime;
        }
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
                isMoving = true;
                animator.SetBool("IsMoving", isMoving);
                if (checkshift == false && CooldownSFX <= 0)
                {
                    walkSFX.Play();
                    CooldownSFX = 0.5f;
                }
                if (checkshift == true && CooldownSFX <= 0)
                {
                    runSFX.Play();
                    CooldownSFX = 0.5f;
                }

            }
            else
            {
                isMoving = false;
                animator.SetBool("IsMoving", isMoving);
                rb.velocity = Vector3.zero;
            }
        }
    }
    //Nếu đánh thì ngừng di chuyển và set chỉ số di chuyển về 0 (không bị trượt đi 1 đoạn) đồng thời set kiểm tra di chuyển về false
    public void DontMove()
    {
        AllowMove = false;
        rb.velocity = Vector3.zero;
        animator.SetBool("IsMoving", false);
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
