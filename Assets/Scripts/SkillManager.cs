using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// スキルを管理するコンポーネント
/// </summary>
public class SkillManager : SingletonMonoBehaviour<SkillManager>
{

    /// <summary>現在取得しているスキル</summary>
    public ISkill[] Skills { get { return _skills.Values.ToArray(); } }
    
    public ISkill[] AllSkills
    {
        get
        {
            if(_allSkills == null)
            {
                _allSkills = new Dictionary<SkillID, ISkill>();
                AllSkillGet();
            }
            return _allSkills.Values.ToArray();
        }
    }


    Dictionary<SkillID, ISkill> _skills = new Dictionary<SkillID, ISkill>();
    Dictionary<SkillID, ISkill> _allSkills;


    private void Awake()
    {
        AllSkillGet();
    }

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
        var sId = (SkillID)id;
        if (_skills.ContainsKey(sId))
        {
            _skills[sId].Update();
        }
        else
        {
            var s = GetSkill(id);
            {
                if(s != null)
                {
                    _skills.Add(sId, s);
                }
            }
        }
    }
    /// <summary>
    /// IDと一致するスキルを返す
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ISkill GetSkill(int id)
    {
        return GetSkill((SkillID)id);
    }

    /// <summary>
    /// IDと一致するスキルを返す
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ISkill GetSkill(SkillID id)
    {
        if(_allSkills == null)
        {
            AllSkillGet();
        }
        switch (id)
        {
            case SkillID.AddHP:
                return _allSkills[id];
            default:
                Debug.LogError($"Skill{id}は設定されていません");
                return null;
        }
    }

    ISkill SkillIDCheck(SkillID id, ISkill skill)
    {
        if (id == skill.ID)
        {
            return skill;
        }
        Debug.LogError($"Skill{id}の取得に失敗しました");
        return null;
    }

    /// <summary>
    /// 全てのスキルを取得する
    /// </summary>
    void AllSkillGet()
    {
        _allSkills.Add(SkillID.AddHP, SkillIDCheck(SkillID.AddHP, new AddHp()));
    }
}
