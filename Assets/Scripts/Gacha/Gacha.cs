using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    private float tocdo;
    private bool dangGacha;
    public ChestLv1 chest; // Thêm tham chiếu đến script ChestLv1

    private List<CaseCell> cells = new List<CaseCell>();
    private bool hasScrolled; // Biến để đánh dấu đã cuộn lần đầu

    public void Scroll()
    {
        if (dangGacha || hasScrolled) // Kiểm tra nếu đang cuộn hoặc đã cuộn lần đầu
            return;

        GetComponent<RectTransform>().localPosition = new Vector2(1080, 0);
        tocdo = Random.Range(4, 5);
        dangGacha = true;

        if (cells.Count == 0)
        {
            for (int i = 0; i < 50; i++)
            {
                cells.Add(Instantiate(_prefab, transform).GetComponentInChildren<CaseCell>());
            }
        }

        foreach (var cell in cells)
        {
            cell.Setup();
        }

        hasScrolled = true; // Đánh dấu đã cuộn lần đầu
    }

    private void Update()
    {
        if (!hasScrolled) // Nếu chưa cuộn lần đầu, thì không cập nhật vị trí
            return;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.left * 100, tocdo * Time.deltaTime * 1500);
        if (tocdo > 0)
            tocdo -= Time.deltaTime;
        else
        {
            tocdo = 0;
            dangGacha = false;

            chest.SetPanel(false); // Tắt panel sau khi cuộn lần đầu
        }
    }
}
