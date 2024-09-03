using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] Spawnable;
    public Vector2 BottomLeft, TopRight;
    public int SpawnableCount;
    void Start()
    {
        for (int i = 0; i < Spawnable.Length; i++)
        {
            int SpawnableArrayIndex = Random.Range(0, Spawnable.Length);
            Vector2 vitri = new Vector2(Random.Range(BottomLeft.x, TopRight.x),
                Random.Range(BottomLeft.y, TopRight.y));
            GameObject g = Instantiate(Spawnable[SpawnableArrayIndex], vitri, Quaternion.identity);
            g.transform.parent = transform;
        }
    }
}
