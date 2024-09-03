using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    public Material tainguyendiahinh;
    public Material tainguyenCanh;
    public float waterLevel = .4f;
    public float scale = .1f;
    public int size = 100;
    Cell[,] grid;

    void Start()
    {
        float[,] noiseMap = new float[size,size];
        float xOffset = Random.Range(-10000f, 10000f);
        float yOffset = Random.Range(-10000f, 10000f);
        for(int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * scale + xOffset, y * scale + yOffset);
                noiseMap[x,y] = noiseValue;
            }
        }
        float[,] falloffMap = new float[size,size];
        for(int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
            {
                float xv = x / (float)size * 2 - 1;
                float yv = y / (float)size * 2 - 1;
                float v = Mathf.Max(Mathf.Abs(xv), Mathf.Abs(yv));
                falloffMap[x, y] = Mathf.Pow(v, 3f) / (Mathf.Pow(v, 3f) + Mathf.Pow(2.2f - 2.2f * v, 3f));
            }
        }

        grid = new Cell[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = new Cell();
                float noiseValue = noiseMap[x,y];
                noiseValue -= falloffMap[x, y];
                cell.isWater = noiseValue < waterLevel;
                grid[x,y] = cell; 
            }
        }
        VeDiaHinh(grid);
        VeKetcau(grid);
        VeCanh(grid);
    }
    
    void VeCanh(Cell[,] grid)
    {
        Mesh mesh = new Mesh();
        List<Vector3> dinh = new List<Vector3>();
        List<int> tamgiac = new List<int>();
        for(int y = 0; y < size; y++)
        {
            for(int x = 0;x < size; x++)
            {
                Cell cell = grid[x,y];
                if(!cell.isWater)
                {
                    if(x > 0)
                    {
                        Cell trai = grid[x - 1,y];
                        if (trai.isWater)
                        {
                            Vector3 a = new Vector3(x - .5f, 0, y + .5f);
                            Vector3 b = new Vector3(x + .5f, 0, y + .5f);
                            Vector3 c = new Vector3(x - .5f, 0, y - .5f);
                            Vector3 d = new Vector3(x + .5f, 0, y - .5f);

                            Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                            for (int k = 0; k < 6; k++)
                            {
                                dinh.Add(v[k]);
                                tamgiac.Add(tamgiac.Count);
                            }
                        }
                    }
                    if (x < size - 1)
                    {
                        Cell phai = grid[x + 1, y];
                        if (phai.isWater)
                        {
                            Vector3 a = new Vector3(x - .5f, 0, y + .5f);
                            Vector3 b = new Vector3(x + .5f, 0, y + .5f);
                            Vector3 c = new Vector3(x - .5f, 0, y - .5f);
                            Vector3 d = new Vector3(x + .5f, 0, y - .5f);

                            Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                            for (int k = 0; k < 6; k++)
                            {
                                dinh.Add(v[k]);
                                tamgiac.Add(tamgiac.Count);
                            }
                        }
                    }
                    if (y > 0)
                    {
                        Cell duoi = grid[x, y - 1];
                        if (duoi.isWater)
                        {
                            Vector3 a = new Vector3(x - .5f, 0, y + .5f);
                            Vector3 b = new Vector3(x + .5f, 0, y + .5f);
                            Vector3 c = new Vector3(x - .5f, 0, y - .5f);
                            Vector3 d = new Vector3(x + .5f, 0, y - .5f);

                            Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                            for (int k = 0; k < 6; k++)
                            {
                                dinh.Add(v[k]);
                                tamgiac.Add(tamgiac.Count);
                            }
                        }
                    }
                    if (y < size - 1)
                    {
                        Cell tren = grid[x, y + 1];
                        if (tren.isWater)
                        {
                            Vector3 a = new Vector3(x - .5f, 0, y + .5f);
                            Vector3 b = new Vector3(x + .5f, 0, y + .5f);
                            Vector3 c = new Vector3(x - .5f, 0, y - .5f);
                            Vector3 d = new Vector3(x + .5f, 0, y - .5f);

                            Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                            for (int k = 0; k < 6; k++)
                            {
                                dinh.Add(v[k]);
                                tamgiac.Add(tamgiac.Count);
                            }
                        }
                    }
                }
            }
        }
        mesh.vertices = dinh.ToArray();
        mesh.triangles = tamgiac.ToArray();
        mesh.RecalculateNormals();

        GameObject canh = new GameObject("Edge");
        canh.transform.SetParent(transform);

        MeshFilter meshFilter = canh.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        MeshRenderer meshRenderer = canh.AddComponent<MeshRenderer>();
        meshRenderer.material = tainguyenCanh;
    }
    void VeDiaHinh(Cell[,] grid)
    {
        Mesh mesh = new Mesh();
        List<Vector3> dinh = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> tamgiac = new List<int>();
        for(int y = 0; y < size; y++)
        {
            for(int x = 0;x < size; x++)
            {
                Cell cell = grid[x,y];
                if (!cell.isWater)
                {
                    Vector3 a = new Vector3(x - .5f, 0, y + .5f);
                    Vector3 b = new Vector3(x + .5f, 0, y + .5f);
                    Vector3 c = new Vector3(x - .5f, 0, y - .5f);
                    Vector3 d = new Vector3(x + .5f, 0, y - .5f);

                Vector2 dinhA = new Vector2(x / (float)size , y / (float)size);
                Vector2 dinhB = new Vector2((x + 1) / (float)size, y / (float)size);
                Vector2 dinhC = new Vector2(x / (float)size, (y + 1) / (float)size);
                Vector2 dinhD = new Vector2((x+1) / (float)size, (y + 1) / (float)size);
                Vector3[] v = new Vector3[] { a, b, c, b, d, c };
                Vector3[] uv = new Vector3[] { dinhA, dinhB, dinhC, dinhB, dinhD, dinhC};
                    for(int k = 0; k < 6; k++)
                    {
                        dinh.Add(v[k]);
                        tamgiac.Add(tamgiac.Count);
                        uvs.Add(uv[k]);
                    }
                }
            }
        }
        mesh.vertices = dinh.ToArray();
        mesh.triangles = tamgiac.ToArray();
        mesh.RecalculateNormals();

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
    }

    void VeKetcau(Cell[,] grid)
    {
        Texture2D texture = new Texture2D(size, size);
        Color[] mausac = new Color[size*size];
        for(int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];
                if (cell.isWater)
                    mausac[y * size + x] = Color.blue;
                else
                    mausac[y * size + x] = Color.green;
            }
        }
        texture.filterMode = FilterMode.Point;
        texture.SetPixels(mausac); 
        texture.Apply();

        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material = tainguyendiahinh;
        meshRenderer.material.mainTexture = texture;
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        for (int y = 0;y < size;y++)
        {
            for(int x = 0;x < size; x++)
            {
                Cell cell = grid[x,y];
                if(cell.isWater)
                    Gizmos.color = Color.blue;
                else Gizmos.color = Color.green;
                Vector3 vitri = new Vector3(x, 0, y);
                Gizmos.DrawCube(vitri, Vector3.one);
            }
        }
    }
}
