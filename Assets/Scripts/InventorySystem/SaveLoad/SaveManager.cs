using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour//, IDataPersistence
{
    public static GameData data;
    private FileDataHandler fileDataHandler;
    private string profileId = "playerProfile";

    private void Awake()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, "GameData.json", true);
        data = new GameData();
        LoadGame();
    }

    public void LoadData(GameData _data)
    {
        data = _data;
    }

    public void SaveData(GameData savedata)
    {
        savedata = data;
    }

    public void LoadGame()
    {
        GameData loadedData = fileDataHandler.Load(profileId);

        if (loadedData != null)
        {
            data = loadedData;

            foreach (var item in FindObjectsOfType<ItemPickUp>())
            {
                item.LoadData(data);
            }

            var playerInventoryHolder = FindObjectOfType<PlayerInventoryHolder>();
            if (playerInventoryHolder != null)
            {
                playerInventoryHolder.LoadData(data);
            }
        }
    }

    public void SaveGame()
    {
        foreach (var item in FindObjectsOfType<ItemPickUp>())
        {
            item.SaveData(data);
        }

        var playerInventoryHolder = FindObjectOfType<PlayerInventoryHolder>();
        if (playerInventoryHolder != null)
        {
            playerInventoryHolder.SaveData(data);
        }

        data.lastUpdated = DateTime.Now.ToBinary();
        fileDataHandler.Save(data, profileId);
    } 
}
