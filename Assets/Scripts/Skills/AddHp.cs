﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの最大HPを上昇させる
/// </summary>
public class AddHp : ISkill
{
    static AddHp instance = new AddHp();
    private AddHp() { }
    public static AddHp Instance { get { return instance; } }

    public SkillID ID => SkillID.AddHP;

    public SkillType Type => SkillType.Passive;

    public bool IsLevelMax => _isLevelMax;

    /// <summary>スキルがレベルアップ可能か</summary>
    bool _isLevelMax = false;

    /// <summary>現在のスキルのレベル</summary>
    int _level;

    [Tooltip("レベルごとの上昇値")]
    [SerializeField] float[] _addTable = new float[]
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
        if (_level == _addTable.Length)
        {
            _isLevelMax = true;
        }
        else
        {
            GameData.Instance.BaseMaxHP += _addTable[_level] - _addTable[_level - 1];
            _level++;
        }
        Debug.Log(1);
    }

    public void SetUp()
    {
        GameData.Instance.BaseMaxHP += _addTable[0];
        _level = 1;
    }

    public void Update()
    {
    }

    public void Delete()
    {
        GameData.Instance.BaseMaxHP -= _addTable[_level];
    }

}
