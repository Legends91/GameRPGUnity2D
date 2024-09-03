using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanhDong : MonoBehaviour
{
    Animator animator;

    [Header("------ Bộ đếm từng đòn đánh ------")]
    public bool startCombo;
    public bool combo = true;
    public int comboNumber = 1;
    public float comboTime;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (startCombo == true)
        {
            comboTime -= Time.deltaTime;
        }
        else
        {
            comboTime = 0.7f;
            comboNumber = 1;
        }
        //Thời gian giữa những lần combo trừ dần.
        //Nếu combo được kích hoạt và tiếp tục nhấn J thì combo sẽ được đếm + dần và reset thời gian giữa những lần combo.
        if (combo == true && Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("Atk" + comboNumber);
            comboNumber++;
            comboTime = 0.7f;
        }
        //Số combo (Nếu combo của nhân vật là 3 và đếm lên là 4 thì set lại combo đầu tiên).
        if (comboNumber == 4)
        {
            comboNumber = 1;
        }
        // Nếu số thời gian nhỏ hơn 0, set lại thời gian combo.
        //Nếu di chuyển, set lại toàn bộ combo (kể cả thời gian cooldown)
    }
    public void ResetCombo()
    {
        startCombo = false;
        comboNumber = 1;
        comboTime = 0f;
        combo = true;
    }
    //ComboAtk
    public void StartCombo()
    {
        startCombo = true;
    }
    public void EndCombo()
    {
        startCombo = false;
    }
    //ComboAtk
    public void Cooldown()
    {
        combo = false;
    }

    public void ActiveCombo()
    {
        combo = true;
    }
}
