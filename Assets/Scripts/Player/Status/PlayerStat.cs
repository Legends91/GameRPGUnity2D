using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    #region
    [Header("------ Tên nhân vật ------")]
    public string playerName;
    public string description;

    [Header("------ Thông số nhân vật ------")]
    public int playerLevel;
    public int maxLevel;
    public int currentExp;
    public int[] nextLevelExp;

    public int currentHp, currentMp, maxHp, maxMp;
    public int attack, defense;
    #endregion

    [Header("----------- UI -----------")]
    public GameObject[] gameObjectWithAnimators;
    public int curHp, preHp, curMp, preMp, curAtt, preAtt, curDef, preDef;

    [SerializeField] private Sprite[] sprites;
    public Sprite playerSprite;

    [Header("------ Khai báo ------")]
    public ParticleSystem lvEffect;
    public GameObject statPanel;
    public bool isOpenPanel = false;

    [Header("-------Thuộc tính-------")]
    [SerializeField] private AttributesScriptableObject playerAttributesSO;

    private void Start()
    {
        statPanel.SetActive(false);
        nextLevelExp = new int[maxLevel + 1];
        nextLevelExp[1] = 10;

        for(int i = 2; i < maxLevel; i++)
        {
            nextLevelExp[i] = Mathf.RoundToInt(nextLevelExp[i - 1] * 1.1f);
        }

        curHp = maxHp;
        curMp = maxMp;
        preHp = 0;
        curAtt = attack;
        curDef = defense;
        preAtt = 0;
        preDef = 0;

        playerSprite = sprites[0];
    }
    public void SetStatPanel(bool open)
    {
        statPanel.SetActive(open);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddExp(150);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isOpenPanel == false)
            {
                statPanel.SetActive(true);
                isOpenPanel = true; 
            }    
           /* else
            {
                statPanel.SetActive(false);
                isOpenPanel = false;
            } */
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            statPanel.SetActive(false);
            isOpenPanel = false;
        }
    }

    public void AddExp(int amount)
    {
        currentExp += amount;

       
        preHp = maxHp;
        preMp = maxMp;
        preAtt = curAtt;
        preDef = curDef;

        
        if (currentExp >= nextLevelExp[playerLevel] && playerLevel < maxLevel)
        {
            LevelUp();
        }

        if(playerLevel >= maxLevel)
        {
            currentExp = 0;
        }

        FindObjectOfType<UIManager>().UpdatePlayerStatus();
    }

    private void LevelUp()
    {
        currentExp -= nextLevelExp[playerLevel];
        playerLevel++;
        maxHp = Mathf.RoundToInt(maxHp * 1.2f);
        currentHp = maxHp;
        maxMp += 20;
        currentMp = maxMp;
        attack = Mathf.CeilToInt(attack * 1.1f);
        defense = Mathf.RoundToInt(defense * 1.05f);

      
        curHp = maxHp;
        curMp = maxMp;
        curAtt = attack;
        curDef = defense;

        
        FindObjectOfType<UIManager>().MakingAnimation();
        UpdateProfile();
    }

    private void UpdateProfile()
    {
        if(playerLevel < 3)
        {
            playerSprite = sprites[0];
        } 
        else if(playerLevel < 5)
        {
            playerSprite = sprites[0];
        } 
        else if(playerLevel < 8)
        {
            playerSprite = sprites[0];
        }
        else
        {
            playerSprite = sprites[0];
        }

        if(playerLevel == 3 || playerLevel == 5 || playerLevel == 8 || playerLevel == 10)
        {
            StartCoroutine(LevelUpEffectCo());
        }
    }

    IEnumerator LevelUpEffectCo()
    {
        lvEffect.gameObject.SetActive(true);
        lvEffect.Play();
        yield return new WaitForSeconds(lvEffect.duration);
        lvEffect.gameObject.SetActive(false);

    }

    public void LoadData(GameData data)
    {/*
        data.playerAttributesData.curHp = playerAttributesSO.curHp;
        data.playerAttributesData.curMp = playerAttributesSO.curMp;
        data.playerAttributesData.curAtt = playerAttributesSO.curAtt;
        data.playerAttributesData.curDef = playerAttributesSO.curDef;
    }

    public void SaveData(GameData data)
    {
        data.playerAttributesData.curHp = playerAttributesSO.curHp;
        data.playerAttributesData.curMp = playerAttributesSO.curMp;
        data.playerAttributesData.curAtt = playerAttributesSO.curAtt;
        data.playerAttributesData.curDef = playerAttributesSO.curDef; */
    }
}
