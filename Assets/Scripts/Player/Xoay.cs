using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xoay : MonoBehaviour
{
    // Tốc độ quay của nhân vật
    public float rotateSpeed = 5f;

    // Tham chiếu tới GameObject chứa hình ảnh của nhân vật
    public GameObject spriteHolder;

    void Update()
    {
        // Lấy vị trí của chuột trong không gian thế giới
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Đảm bảo rằng vị trí z là 0

        // Tính toán hướng theo chiều x và chiều y giữa nhân vật và chuột
        float deltaX = mousePosition.x - transform.position.x;
        float deltaY = mousePosition.y - transform.position.y;

        // Tính toán góc quay (theo đơn vị độ)
        float angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;

        // Tạo ra một Quaternion xoay theo góc tính toán
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        // Đặt xoay của nhân vật
        transform.rotation = rotation;

        // Xoay toàn bộ GameObject chứa hình ảnh của nhân vật
        spriteHolder.transform.rotation = rotation;
    }
}
