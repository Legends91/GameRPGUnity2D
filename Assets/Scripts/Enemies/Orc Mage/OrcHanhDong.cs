using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcHanhDong : MonoBehaviour
{

    private Animation Animations;
    private OrcDiChuyen agentMover;
    private Transform player;
    private Animator Anyma;
    [Header("----- Khoảng cách với người chơi -----")]
    public float distanceFromPlayer;
    [Header("----- Thời gian hồi ATK 1 -----")]
    public float CoolDownAtkTime;
    public bool CoolDownAtk;
    [Header("----- ATK 1 -----")]
    public List<GameObject> Sum;
    private int currentIndex = 0;
    [Header("----- ATK 2 -----")]
    public List<GameObject> Sum2;
    private int currentIndex2 = 0;
    [Header("----- ATK 3 -----")]

    //private Vukhi weaponParent;

    private Vector2 pointerInput, movementInput;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }
    
    private void Update()
    {
        distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (CoolDownAtkTime > 0)
        {
            CoolDownAtkTime -= Time.deltaTime;
        }
        //pointerInput = GetPointerInput();
        //movementInput = movement.action.ReadValue<Vector2>().normalized;
        agentMover.MovementInput = MovementInput;
        //weaponParent.PointerPosition = pointerInput;
        // AnimateCharacter();     
    }
    public void SummonGameObject()
    {
        // Kiểm tra xem danh sách có phần tử nào không
        if (Sum.Count == 0)
        {
            Debug.LogWarning("Danh sách Sum trống.");
            return;
        }

        // Triệu hồi game object từ danh sách
        GameObject summonedObject = Instantiate(Sum[currentIndex], player.position, Quaternion.identity);


        // Tăng chỉ số để triệu hồi game object tiếp theo
        currentIndex++;

        // Nếu đã triệu hồi hết danh sách, reset chỉ số để bắt đầu lại từ đầu
        if (currentIndex >= Sum.Count)
        {
            currentIndex = 0;
        }
    }
    public void SummonGameObjectAtk2()
    {
        // Kiểm tra xem danh sách có phần tử nào không
        if (Sum2.Count == 0)
        {
            Debug.LogWarning("Danh sách Sum trống.");
            return;
        }

        // Triệu hồi game object từ danh sách
        GameObject summonedObject2 = Instantiate(Sum2[currentIndex2], player.position, Quaternion.identity);


        // Tăng chỉ số để triệu hồi game object tiếp theo
        currentIndex2++;

        // Nếu đã triệu hồi hết danh sách, reset chỉ số để bắt đầu lại từ đầu
        if (currentIndex2 >= Sum2.Count)
        {
            currentIndex2 = 0;
        }
    }

    public void PerformAttack()
    {
        //weaponParent.Attack();
    }

    private void Awake()
    {
        Anyma = GetComponent<Animator>();
        Animations = GetComponentInChildren<Animation>();
        //weaponParent = GetComponentInChildren<Vukhi>();
        agentMover = GetComponent<OrcDiChuyen>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        //  Animations.RotateToPointer(lookDirection);
        Animations.PlayAnimation(MovementInput);
    }
    public void CooldownAttack()
    {
        CoolDownAtkTime = 2.5f;
        CoolDownAtk = false;
    }
    public void Attacking()
    {
        CoolDownAtk = true;
    }


    
}
