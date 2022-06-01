﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHp : ISkill
{
    public SkillID ID => SkillID.AddHP;


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
        Player.Instance.MaxHPPlus += _addTable[_level] - _addTable[_level - 1];
        _level++;
    }

    public void SetUp()
    {
        Player.Instance.MaxHPPlus += _addTable[0];
        _level = 1;
    }

    public void Update()
    {
    }

    public void Delete()
    {
        Player.Instance.MaxHPPlus -= _addTable[_level];
    }

}
