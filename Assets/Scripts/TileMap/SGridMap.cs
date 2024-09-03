using UnityEngine;
using UnityEngine.Tilemaps;

public class SGridMap : MonoBehaviour
{
    // Biến public để gán từ Unity Inspector
    public Tilemap highlightTilemap; // Tilemap để highlight
    public Tile highlightTile; // Tile dùng để highlight
    public Transform character; // Transform của nhân vật
    public Animator animator; // Animator của nhân vật

    // Biến lưu trữ vị trí trước đó
    private Vector3Int previousMousePosition = new Vector3Int(-1, -1, -1);
    private Vector3Int previousCharacterCellPosition = new Vector3Int(-1, -1, -1);

    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = character.GetComponent<Animator>();

        // Kiểm tra và báo lỗi nếu highlightTilemap hoặc highlightTile chưa được gán
        if (highlightTilemap == null || highlightTile == null)
        {
            Debug.LogError("HighlightTilemap hoặc HighlightTile chưa được khai báo!.");
        }
    }

    void Update()
    {
        MouseLight();
    }

    public void MouseLight()
    {
        // Kiểm tra và thoát nếu bất kỳ biến nào chưa được gán từ Unity Inspector
        if (highlightTilemap == null || highlightTile == null || character == null || animator == null)
        {
            return;
        }

        // Lấy vị trí của chuột trong không gian thế giới và chuyển đổi thành vị trí ô tile
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = highlightTilemap.WorldToCell(mouseWorldPos);
        Vector3Int characterCellPosition = highlightTilemap.WorldToCell(character.position);

        // Tính toán vùng bao quanh nhân vật để highlight
        Vector3Int bottomLeft = characterCellPosition + new Vector3Int(-2, -2, 0);
        Vector3Int topRight = characterCellPosition + new Vector3Int(2, 2, 0);

        // Xóa highlight ở vị trí trước đó nếu nhân vật di chuyển đến ô mới
        if (characterCellPosition != previousCharacterCellPosition)
        {
            ClearPreviousHighlights();
            previousCharacterCellPosition = characterCellPosition;
        }

        // Giới hạn vị trí chuột trong vùng highlight
        Vector3Int clampedPosition = ClampToBounds(cellPosition, bottomLeft, topRight);

        // Nếu vị trí chuột thay đổi, thực hiện highlight ô mới
        if (clampedPosition != previousMousePosition)
        {
            // Xóa highlight ở vị trí chuột trước đó
            if (previousMousePosition != new Vector3Int(-1, -1, -1))
            {
                highlightTilemap.SetTile(previousMousePosition, null);
            }

            // Đặt highlightTile vào vị trí chuột mới nếu vị trí nằm trong vùng highlight cho phép
            if (IsWithinBounds(clampedPosition, bottomLeft, topRight))
            {
                highlightTilemap.SetTile(clampedPosition, highlightTile);
            }

            // Lưu vị trí chuột mới
            previousMousePosition = clampedPosition;

            // Cập nhật giá trị X và Y vào animator
            //animator.SetFloat("X", clampedPosition.x - characterCellPosition.x);
            //animator.SetFloat("Y", clampedPosition.y - characterCellPosition.y);
        }
    }

    // Kiểm tra xem một vị trí có nằm trong giới hạn cho phép không
    bool IsWithinBounds(Vector3Int position, Vector3Int bottomLeft, Vector3Int topRight)
    {
        return position.x >= bottomLeft.x && position.x <= topRight.x &&
               position.y >= bottomLeft.y && position.y <= topRight.y;
    }

    // Giới hạn vị trí trong vùng highlight cho phép
    Vector3Int ClampToBounds(Vector3Int position, Vector3Int bottomLeft, Vector3Int topRight)
    {
        return new Vector3Int(
            Mathf.Clamp(position.x, bottomLeft.x, topRight.x),
            Mathf.Clamp(position.y, bottomLeft.y, topRight.y),
            position.z
        );
    }

    // Xóa các ô đã highlight trong vùng 5x5 xung quanh vị trí nhân vật trước đó
    void ClearPreviousHighlights()
    {
        BoundsInt bounds = new BoundsInt(
            previousCharacterCellPosition + new Vector3Int(-2, -2, 0),
            new Vector3Int(5, 5, 1) // Kích thước vùng
        );

        foreach (var position in bounds.allPositionsWithin)
        {
            highlightTilemap.SetTile(position, null);
        }
    }
    public Vector3Int GetPreviousMousePosition()
    {
        return previousMousePosition;
    }
}
