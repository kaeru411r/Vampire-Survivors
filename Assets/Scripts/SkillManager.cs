using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// �X�L�����Ǘ�����R���|�[�l���g
/// </summary>
public class SkillManager : SingletonMonoBehaviour<SkillManager>
{
    public ISkill GetSkill(int id)
    {
        switch (id)
        {
            case 0:
                return new AddHp();
            default:
                return null;
        }
    }
    public ISkill[] Skills
    {
        get
        {
            SkillsNullCheck();
            return _skills.ToArray();
        }
    }

    void SkillsNullCheck()
    {
        for(int i = 0; i < _skills.Count; i++)
        {
            if(_skills[i] == null)
            {
                _skills.RemoveAt(i);
                i--;
            }
        }
    }


    List<ISkill> _skills = new List<ISkill>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        _skills.ForEach(s => s.Update());
    }


    /// <summary>
    /// �X�L���̎擾�A����
    /// </summary>
    /// <param name="id"></param>
    public void AddSkill(int id)
    {
        var hs = _skills.Where(s => s.ID == (SkillDef)id);
        if (hs.Count() != 0)
        {
            ISkill s = GetSkill(id);
            if (s != null)
            {
                _skills.Add(s);
            }
        }
        else
        {
            hs.Single().LevelUp();
        }
    }
}
