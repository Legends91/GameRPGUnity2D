using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Transform weaponParentTransform; // Transform nơi vũ khí sẽ được khởi tạo
    private GameObject currentWeapon;
    public void InstantiateItem(string itemName)
    {
        DestroyWeapon();
        // Tải prefab dựa trên tên item từ thư mục Resources
        GameObject itemPrefab = Resources.Load<GameObject>(itemName);
        if (itemPrefab != null)
        {
            // Khởi tạo prefab và gắn nó làm con của weaponParentTransform
            currentWeapon = Instantiate(itemPrefab, weaponParentTransform);
            currentWeapon.transform.localPosition = Vector3.zero; // Đặt vị trí tương đối
            currentWeapon.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogWarning("Không tìm thấy prefab cho: " + itemName);
        }
    }
    public void DestroyWeapon()
    {
        // Hủy prefab hiện tại nếu tồn tại
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

    }
}
