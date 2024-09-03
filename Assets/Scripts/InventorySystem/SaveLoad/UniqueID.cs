using System;
using System.CodeDom.Compiler;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]

public class UniqueID : MonoBehaviour
{
    [ReadOnly, SerializeField] private string _id = Guid.NewGuid().ToString();

    [SerializeField]
    private static SerializableDictionary<string, GameObject> idDatabase = new SerializableDictionary<string, GameObject>();
    public string ID => _id;
    private void OnValidate()
    {
        if (idDatabase.ContainsKey(ID)) Generate();
        else idDatabase.Add(_id, this.gameObject);
    }

    private void OnDestroy()
    {
        if (idDatabase.ContainsKey(ID)) idDatabase.Remove(ID);
    }
    [ContextMenu("GenerateID")]
    private void Generate()
    {
        _id = Guid.NewGuid().ToString();
        idDatabase.Add(_id, this.gameObject);
        Debug.Log(idDatabase.Count);
    }
}
