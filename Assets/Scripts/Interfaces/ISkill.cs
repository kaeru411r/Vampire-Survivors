using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ISkill
{
    SkillDef ID { get; }

    void SetUp();

    void Update();

    void LevelUp();

    void Delete();
}


public enum SkillDef
{
    Gun = 0,
    AddHP = 1,
}