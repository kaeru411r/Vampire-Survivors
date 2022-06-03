using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    /// <summary>スキルのID</summary>
    SkillID ID { get; }

    /// <summary>スキルのタイプ</summary>
    SkillType Type { get; }

    /// <summary>スキルのレベルが最大かどうか</summary>
    bool IsLevelMax { get;}

    /// <summary>初回取得時に呼ぶ</summary>
    void SetUp();

    /// <summary>毎フレーム呼ぶ</summary>
    void Update();

    /// <summary>二回目以降の取得時に呼ぶ</summary>
    void LevelUp();

    /// <summary>スキル削除時に呼ぶ</summary>
    void Delete();
}


public enum SkillID
{
    Gun = 0,
    AddHP = 1,
}

public enum SkillType
{
    Active = 0,
    Passive = 1,
}