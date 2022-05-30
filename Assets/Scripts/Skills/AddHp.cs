using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHp : ISkill
{
    public SkillDef ID => SkillDef.AddHP;

    int _level;

    float[] _addTable = new float[]
    {
        10,
        20,
        40,
        80,
        160,
        320,
        640,
        1280
    };


    public void LevelUp()
    {
        GameManager.Player.MaxHPPlus += _addTable[_level];
        _level++;
    }

    public void SetUp()
    {
        GameManager.Player.MaxHPPlus += _addTable[0];
        _level = 1;
    }

    public void Update()
    {
    }

}
