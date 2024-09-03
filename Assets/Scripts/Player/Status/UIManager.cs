using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerStat[] playerStatus;

    public Text nameText;
    public Text levelText, hpText, mpText, expText;
    public Text attackText, defenseText, nextExpText;

    public Slider expSldier;
    public Text sliderText;

    public Image playerImage;

    public Text hpDisplayText, mpDisplayText, attDisplayText, defDisplayText;

    private int currentIndex;

    public Text playerOrderText;

    private void Start()
    {
        currentIndex = 0;
        UpdatePlayerStatus();
    }

    public void NextButton()
    {
        currentIndex++;
        if(currentIndex > playerStatus.Length - 1)
        {
            currentIndex = 0;
        }
        UpdatePlayerStatus();
    }

    public void LastButton()
    {
        currentIndex--;
        if(currentIndex < 0)
        {
            currentIndex = playerStatus.Length - 1;
        }
        UpdatePlayerStatus();
    }

    public void UpdatePlayerStatus() 
    {
        nameText.text = playerStatus[currentIndex].playerName;

        levelText.text = playerStatus[currentIndex].playerLevel.ToString();
        hpText.text = "" + playerStatus[currentIndex].currentHp + "/" + playerStatus[currentIndex].maxHp;
        mpText.text = "" + playerStatus[currentIndex].currentMp + "/" + playerStatus[currentIndex].maxMp;
        expText.text = "" + playerStatus[currentIndex].currentExp;
        attackText.text = "" + playerStatus[currentIndex].attack;
        defenseText.text = "" + playerStatus[currentIndex].defense;
        nextExpText.text = "" + playerStatus[currentIndex].nextLevelExp[playerStatus[currentIndex].playerLevel];

        expSldier.value = playerStatus[currentIndex].currentExp;
        expSldier.maxValue = playerStatus[currentIndex].nextLevelExp[playerStatus[currentIndex].playerLevel];

        sliderText.text = playerStatus[currentIndex].currentExp + "/" + playerStatus[currentIndex].nextLevelExp[playerStatus[currentIndex].playerLevel];

        playerImage.sprite = playerStatus[currentIndex].playerSprite;

        playerOrderText.text = "" + (currentIndex + 1) + " / " + playerStatus.Length; 
    }

    public void MakingAnimation()
    {
        for (int i = 0; i < playerStatus[currentIndex].gameObjectWithAnimators.Length; i++)
        {
            hpDisplayText.text = "+" + (playerStatus[currentIndex].curHp - playerStatus[currentIndex].preHp);
            mpDisplayText.text = "+" + (playerStatus[currentIndex].curMp - playerStatus[currentIndex].preMp);
            attDisplayText.text = "+" + (playerStatus[currentIndex].curAtt - playerStatus[currentIndex].preAtt);
            defDisplayText.text = "+" + (playerStatus[currentIndex].curDef - playerStatus[currentIndex].preDef);

            playerStatus[currentIndex].gameObjectWithAnimators[i].GetComponent<Animator>().SetTrigger("Levelup");
        }
    }


}
