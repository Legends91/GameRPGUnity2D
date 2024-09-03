using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttributesData
{
    public int curHp, curMp, curAtt, curDef;
    [SerializeField] private PlayerStat player;

    public void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStat>();
    }
    public AttributesData() 
    {
        curHp = player.curHp;
        curMp = player.curMp;
        curAtt = player.curAtt;
        curDef = player.curDef;
    }
}
