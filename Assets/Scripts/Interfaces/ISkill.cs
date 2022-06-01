using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    SkillID ID { get; }

    void SetUp();

    void Update();

    void LevelUp();

    void Delete();
}


public enum SkillID
{
    Gun = 0,
    AddHP = 1,
}