using UnityEngine;

public class PlantingSystem : MonoBehaviour
{
    public GameObject seedPrefab; // Prefab của hạt giống
    public LayerMask groundLayer; // Layer của mặt đất
    public Sprite dugGroundSprite; // Sprite của mặt đất đã đào

    private void Update()
    {
        // Kiểm tra xem người chơi có click chuột trái không
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, groundLayer);

            if (hit.collider != null)
            {
                // Đổi sprite của mặt đất thành sprite đã đào
                hit.collider.GetComponent<SpriteRenderer>().sprite = dugGroundSprite;

                // Trồng hạt giống tại vị trí click chuột
                Instantiate(seedPrefab, hit.point, Quaternion.identity);
            }
        }
    }
}
