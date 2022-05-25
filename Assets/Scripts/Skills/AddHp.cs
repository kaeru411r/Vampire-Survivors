using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHp : ISkill
{
    public SkillDef ID => SkillDef.AddHP;

    const string _csvFileName = "AddHPTable";

    public void LevelUp()
    {
        //string filePath = Application.streamingAssetsPath + _csvFileName;
        
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
