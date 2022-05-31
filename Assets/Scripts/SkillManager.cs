using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// �X�L�����Ǘ�����R���|�[�l���g
/// </summary>
public class SkillManager : SingletonMonoBehaviour<SkillManager>
{
    List<ISkill> _skills = new List<ISkill>();




    public void AddSkill(int id)
    {
        var hs = _skills.Where(s => s.ID == (SkillDef)id);
        if (hs.Count() != 0)
        {
            switch (id)
            {
                default:
                    break;
            }
        }
        else
        {
            hs.Single().LevelUp();
        }
    }
}
