using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHp : ISkill
{
    public SkillDef ID => SkillDef.AddHP;

    float _value = 10.0f;


    public void LevelUp()
    {
        //GameManager.Player
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
