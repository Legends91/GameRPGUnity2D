using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAni : MonoBehaviour
{
    [SerializeField] string[] animationNames; // List các tên animation
    int randomIndex;
    string randomAnimation;
    float animationLength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /* void StartRandomAnimation()
    {
        // Chọn ngẫu nhiên một animation từ danh sách
        randomIndex = Random.Range(0, animationNames.Length);
        randomAnimation = animationNames[randomIndex];

        // Kích hoạt animation ngẫu nhiên
        animator.Play(randomAnimation);

        // Thời gian chờ trước khi thực hiện animation ngẫu nhiên khác
        animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        //Invoke("StartRandomAnimation", animationLength);
    } */
}
