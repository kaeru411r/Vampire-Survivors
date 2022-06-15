using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ホーミング弾のスキル
/// </summary>
public class Horming : ISkill
{
    static Horming instance = new Horming();
    private Horming() { }
    public static Horming Instance { get { return instance; } }

    public SkillID ID => SkillID.Homing;

    public SkillType Type => SkillType.Active;

    public bool IsLevelMax => _isLevelMax;

    bool _isLevelMax;

    public void Delete()
    {
        throw new System.NotImplementedException();
    }

    public void LevelUp()
    {
        throw new System.NotImplementedException();
    }

    public void SetUp()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}
