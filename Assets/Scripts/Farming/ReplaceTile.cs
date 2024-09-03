using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ReplaceTile : MonoBehaviour
{
    public Tilemap replaceTilemap; // Tilemap cần thay thế RuleTile
    public RuleTile ruleTile; // RuleTile để thay thế

    public void TileReplacer(Vector3Int position)
    {
        // Lấy Sorting Layer của replaceTilemap
        int replaceSortingLayerID = replaceTilemap.GetComponent<TilemapRenderer>().sortingLayerID;

        // Tìm tất cả các Tilemap trong cảnh
        Tilemap[] allTilemaps = FindObjectsOfType<Tilemap>();

        // Biến kiểm tra có RuleTile từ các Tilemap đè lên replaceTilemap hay không
        bool overlappingRuleTile = false;

        foreach (Tilemap tilemap in allTilemaps)
        {
            // Kiểm tra xem tilemap có RuleTile và có Sorting Layer ID lớn hơn replaceTilemap không
            TilemapRenderer tilemapRenderer = tilemap.GetComponent<TilemapRenderer>();
            if (tilemap != replaceTilemap && tilemapRenderer != null)
            {
                int tilemapSortingLayerID = tilemapRenderer.sortingLayerID;

                // Kiểm tra nếu tilemap có sorting layer ID lớn hơn của replaceTilemap và có RuleTile tại vị trí position
                if (tilemapSortingLayerID > replaceSortingLayerID && tilemap.GetTile(position) is RuleTile)
                {
                    overlappingRuleTile = true;
                    break; // Đánh dấu là có RuleTile đè lên replaceTilemap, không cần kiểm tra tiếp
                }
            }
        }

        // Nếu không có RuleTile nào đang đè lên và không có RuleTile nào trong replaceTilemap tại vị trí position, thêm RuleTile vào replaceTilemap
        if (!overlappingRuleTile && replaceTilemap.GetTile(position) == null)
        {
            replaceTilemap.SetTile(position, ruleTile);
        }
    }
}

