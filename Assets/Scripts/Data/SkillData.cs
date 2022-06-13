using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スキルとそのIDを保存するクラス
/// </summary>
public class SkillData
{
    public SkillData(ISkill skill)
    {
        Skill = skill;
        ID = skill.ID;
    }
    public ISkill Skill;
    public SkillID ID;
}