using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "word";
    private readonly string backupExtension = ".bak";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption) 
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load(string profileId, bool allowRestoreFromBackup = true) 
    {
        if (profileId == null) 
        {
            return null;
        }

        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath)) 
        {
            try 
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (useEncryption) 
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e) 
            {
                if (allowRestoreFromBackup) 
                {
                    Debug.LogWarning("Tai du lieu that bai\n" + e);
                    bool rollbackSuccess = AttemptRollback(fullPath);
                    if (rollbackSuccess)
                    {
                        loadedData = Load(profileId, false);
                    }
                }
                
                else 
                {
                    Debug.LogError("Co loi xay ra: " 
                        + fullPath  + " dan den khong the backup du lieu.\n" + e);
                }
            }
        }
        return loadedData;
    }

    public void Save(GameData data, string profileId) 
    {
        if (profileId == null) 
        {
            return;
        }

        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        string backupFilePath = fullPath + backupExtension;
        try 
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            if (useEncryption) 
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream)) 
                {
                    writer.Write(dataToStore);
                }
            }

            GameData verifiedGameData = Load(profileId);
            if (verifiedGameData != null) 
            {
                File.Copy(fullPath, backupFilePath, true);
            }
            
            else 
            {
                throw new Exception("Khong the backup du lieu.");
            }

        }
        catch (Exception e) 
        {
            Debug.LogError("Co loi xay ra trong qua trinh luu: " + fullPath + "\n" + e);
        }
    }

    public void Delete(string profileId) 
    {
        if (profileId == null) 
        {
            return;
        }

        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        try 
        {
            if (File.Exists(fullPath)) 
            {
                Directory.Delete(Path.GetDirectoryName(fullPath), true);
            }
            else 
            {
                Debug.LogWarning("Khong tim thay du lieu tu duong dan: " + fullPath);
            }
        }
        catch (Exception e) 
        {
            Debug.LogError("Lỗi: " 
                + profileId + " tại đường dẫn: " + fullPath + "\n" + e);
        }
    }

    public Dictionary<string, GameData> LoadAllProfiles() 
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos) 
        {
            string profileId = dirInfo.Name;

            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Lỗi: "
                    + profileId);
                continue;
            }

            GameData profileData = Load(profileId);
            if (profileData != null) 
            {
                profileDictionary.Add(profileId, profileData);
            }
            else 
            {
                Debug.LogError("Lỗi: " + profileId);
            }
        }

        return profileDictionary;
    }

    public string GetMostRecentlyUpdatedProfileId() 
    {
        string mostRecentProfileId = null;

        Dictionary<string, GameData> profilesGameData = LoadAllProfiles();
        foreach (KeyValuePair<string, GameData> pair in profilesGameData) 
        {
            string profileId = pair.Key;
            GameData gameData = pair.Value;

            if (gameData == null) 
            {
                continue;
            }

            if (mostRecentProfileId == null) 
            {
                mostRecentProfileId = profileId;
            }
            else 
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileId].lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData.lastUpdated);
                if (newDateTime > mostRecentDateTime) 
                {
                    mostRecentProfileId = profileId;
                }
            }
        }
        return mostRecentProfileId;
    }

    private string EncryptDecrypt(string data) 
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++) 
        {
            modifiedData += (char) (data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }

    private bool AttemptRollback(string fullPath) 
    {
        bool success = false;
        string backupFilePath = fullPath + backupExtension;
        try 
        {
            if (File.Exists(backupFilePath))
            {
                File.Copy(backupFilePath, fullPath, true);
                success = true;
                Debug.LogWarning("Lỗi: " + backupFilePath);
            }
            
            else 
            {
                throw new Exception("Lỗi.");
            }
        }
        catch (Exception e) 
        {
            Debug.LogError("Lỗi: " 
                + backupFilePath + "\n" + e);
        }

        return success;
    }
}
