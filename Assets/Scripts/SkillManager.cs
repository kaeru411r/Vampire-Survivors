using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// スキルを管理するコンポーネント
/// </summary>
public class SkillManager : SingletonMonoBehaviour<SkillManager>
{
    /// <summary>
    /// IDと一致するスキルを返す
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ISkill GetSkill(int id)
    {
        switch (id)
        {
            case 1:
                ISkill s = new AddHp();
                return SkillIDCheck(id, ref s);
            default:
                Debug.LogError($"Skill{id}は設定されていません");
                return null;
        }
    }

    /// <summary>
    /// idとskillのIDが一致しているか確かめ、
    /// 一致していたらskillを一致していなかったらnullを返す
    /// </summary>
    /// <param name="id"></param>
    /// <param name="skill"></param>
    /// <returns></returns>
    public ISkill SkillIDCheck(int id, ref ISkill skill)
    {
        if((SkillDef)id == skill.ID)
        {
            return skill;
        }
        Debug.LogError($"Skill{id}の取得に失敗しました");
        return null;
    }


    /// <summary>現在取得しているスキル</summary>
    public ISkill[] Skills { get { return _skills.Values.ToArray(); } }


    Dictionary<int, ISkill> _skills = new Dictionary<int, ISkill>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        foreach(var skill in Skills)
        {
            skill.Update();
        }
    }


    /// <summary>
    /// スキルの取得、強化
    /// </summary>
    /// <param name="id"></param>
    public void AddSkill(int id)
    {
        if (_skills.ContainsKey(id))
        {
            _skills[id].Update();
        }
        else
        {
            var s = GetSkill(id);
            {
                if(s != null)
                {
                    _skills.Add(id, s);
                }
            }
        }
    }
}
