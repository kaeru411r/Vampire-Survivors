using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ISkill
{
    SkillDef ID { get; }

    void SetUp();

    void Update();

    void LevelUp();
}


public enum SkillDef
{
    Gun = 0,
    AddHP = 1,
}