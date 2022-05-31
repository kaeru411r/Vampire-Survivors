using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLevel
{
    int _level;
    int _maxLevel;

    public int Level { get => _level; set => _level = value; }
    public int MaxLevel { get => _maxLevel; set => _maxLevel = value; }

    public SkillLevel(int level, int maxLevel)
    {
        _level = level;
        MaxLevel = maxLevel;
    }

    public SkillLevel(int maxLevel)
    {
        _maxLevel = maxLevel;
    }

    static public SkillLevel operator ++(SkillLevel skillLevel)
    {
        skillLevel.Level++;
        if(skillLevel.Level > skillLevel.MaxLevel)
        {
            Debug.LogError($"LevelがMaxLevelを超過するため、処理を実行できませんでした。");
            skillLevel.Level--;
        }
        return skillLevel;
    }

}
