using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcLoot : MonoBehaviour
{
    [Header("Loot")]
    public List<LootItem> lootTable = new List<LootItem>();
    public OrcHealth health;
    public ParticleController particle;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void DieLoot()
    {
        foreach (LootItem item in lootTable)
        {
            if (Random.Range(0f, 100f) <= item.dropChance)
            {

                GameObject droppedItem = Instantiate(item.itemPrefabs, transform.position, Quaternion.identity);
                Rigidbody2D rb = droppedItem.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Tính hướng và lực để vật phẩm bay ra
                    Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f)).normalized;
                    float forceMagnitude = Random.Range(2f, 5f);
                    rb.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);

                }
            }
        }
        health.Death();
    }
    void Drop()
    {
        particle.ExpDrop();
    }

    void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            GameObject droppedLoot = Instantiate(loot, transform.position, Quaternion.identity);

            droppedLoot.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
