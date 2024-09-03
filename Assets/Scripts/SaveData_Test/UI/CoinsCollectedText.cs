using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinsCollectedText : MonoBehaviour, IDataPersistence
{
    [SerializeField] private int totalCoins = 0;

    private int coinsCollected = 0;

    private Text coinsCollectedText;

    private void Awake() 
    {
        coinsCollectedText = this.GetComponent<Text>();
    }

    private void Start() 
    {
        GameEventsManager.instance.onCoinCollected += OnCoinCollected;
    }

    public void LoadData(GameData data) 
    {
        foreach(KeyValuePair<string, bool> pair in data.coinsCollected) 
        {
            if (pair.Value) 
            {
                coinsCollected++;
            }
        }
    }

    public void SaveData(GameData data)
    {
    }

    private void OnDestroy() 
    {
        GameEventsManager.instance.onCoinCollected -= OnCoinCollected;
    }

    private void OnCoinCollected() 
    {
        coinsCollected++;
    }

    private void Update() 
    {
        coinsCollectedText.text = coinsCollected + " "  ;
    }
}
