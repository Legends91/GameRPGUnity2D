using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public TextMeshProUGUI waveCountText;
    public List<Enemies> enemies = new List<Enemies>();
    public int currentWave = 0;
    private int waveValue;
    private int waveSpawn1;
    private List<GameObject> enemiesToSpawn = new List<GameObject>();
    public Transform spawnLocation;
    public float spawnRadius = 5f;
    public float cuongDo = 2;
    public DayNightScript dayNight;

    public GameObject warningPanel; // Panel cảnh báo
    public TextMeshProUGUI warningText; // Text cảnh báo
    private float spawnInternal;
    private float spawnTimer;
    private bool spawningWave = false;
    // public Gate gate;

    public int lastWaveSpawnedDay = -1; // Ngày cuối cùng mà một đợt quái đã được spawn

    void Start()
    {
        // StartNewWave();
    }

    void Update()
    {
        // Trigger mở cổng

        waveCountText.text = "Wave: " + currentWave.ToString();

        if (!spawningWave && GameObject.FindGameObjectsWithTag("Quai").Length == 0)
        {
            StartNewWave();
        }
    }

    void FixedUpdate()
    {
        waveSpawn1 = currentWave - 1;
        if (spawnTimer <= 0)
        {
            // Kiểm tra nếu danh sách quái vật cần sinh là trống
            if (enemiesToSpawn.Count > 0 && spawningWave && enemiesToSpawn.Count > 0)
            {
                Vector3 randomPosition = GetRandomPosition();
                Instantiate(enemiesToSpawn[0], randomPosition, Quaternion.identity);
                enemiesToSpawn.RemoveAt(0);

                spawnTimer = spawnInternal;
            }
            else
            {
                // Bắt đầu wave mới
                StartNewWave();
            }
        }
        else
        {
            // Giảm spawnTimer
            spawnTimer -= Time.fixedDeltaTime;
        }
        // if (spawningWave && enemiesToSpawn.Count > 0)
        // {
        // SpawnEnemies();
        // }
    }

    public void StartNewWave()
    {
        // cuongDo = 24;
        spawningWave = true;

        if (dayNight.ngayhientai - lastWaveSpawnedDay >= 1)
        {
            GenerateWave();
            DisplayWarning();
            lastWaveSpawnedDay = dayNight.ngayhientai; // Cập nhật ngày spawn cuối cùng
        }
    }

    public void GenerateWave()
    {
        if (spawningWave == true)
        {
            // gate.Open();
        }
        else
        {
            // gate.Close();
        }
        waveValue = currentWave * 10;
        GenerateEnemies();
        currentWave++;
    }

    public void GenerateEnemies()
    {
        List<GameObject> generateEnemies = new List<GameObject>();
        while (waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                generateEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue < 0)
            {
                break;
            }
        }
        // Xóa danh sách quái vật cũ và cập nhật với danh sách mới
        enemiesToSpawn.Clear();
        enemiesToSpawn = generateEnemies;
    }

    public void SpawnEnemies()
    {
        GameObject enemyToSpawn = enemiesToSpawn[0];
        Vector3 randomPosition = GetRandomPosition();
        Instantiate(enemyToSpawn, randomPosition, Quaternion.identity);
        enemiesToSpawn.RemoveAt(0);
    }

    public void DisplayWarning()
    {
        warningText.text = "WAVE " + currentWave + " IS COMING!";
        warningPanel.SetActive(true);
        Invoke("HideWarning", 3f); // Ẩn cảnh báo sau 3 giây
    }

    public void HideWarning()
    {
        warningPanel.SetActive(false);
        spawningWave = false;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomOffset = Random.insideUnitCircle * spawnRadius;
        return spawnLocation.position + new Vector3(randomOffset.x, 0, randomOffset.y);
    }
}

[System.Serializable]
public class Enemies
{
    public GameObject enemyPrefab;
    public int cost;
}
