using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


/// <summary>
/// スキルを管理するコンポーネント
/// </summary>
public class SkillManager : MonoBehaviour
{

    static public SkillManager Instance { get; private set; }

    //Dictionary<SkillID, SkillData> _allSkills;


    /// <summary>現在取得しているスキル</summary>
    public ISkill[] Skills { get { return _skills.Values.ToArray(); } }

    public ISkill[] AllSkills
    {
        get
        {
            if (_allSkills == null)
            {
                AllSkillGet();
            }
            return _allSkills.Values.ToArray();
        }
    }


    Dictionary<SkillID, ISkill> _skills = new Dictionary<SkillID, ISkill>();
    [SerializeField] Dictionary<SkillID, ISkill> _allSkills;


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
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
            if (!_skills[sId].IsLevelMax)
            {
                _skills[sId].LevelUp();
            }
        }
        else
        {
            var s = GetSkill(id);
            {
                if(s != null)
                {
                    _skills.Add(sId, s);
                    s.SetUp();
                }
            }
        }
    }

    /// <summary>
    /// スキルの取得、強化
    /// </summary>
    /// <param name="id"></param>
    public void AddSkill(SkillID id)
    {
        if (_skills.ContainsKey(id))
        {
            if (!_skills[id].IsLevelMax)
            {
                _skills[id].LevelUp();
            }
        }
        else
        {
            var s = GetSkill(id);
            {
                if (s != null)
                {
                    _skills.Add(id, s);
                    s.SetUp();
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
        return GetSkill(id);
    }

    /// <summary>
    /// IDと一致するスキルを返す
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ISkill GetSkill(SkillID id)
    {
        if (_allSkills == null)
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
    /// スキルを消去
    /// </summary>
    /// <param name="id"></param>
    public void RemoveSkill(int id)
    {
        RemoveSkill((SkillID)id);
    }


    /// <summary>
    /// スキルを消去
    /// </summary>
    /// <param name="id"></param>
    public void RemoveSkill(SkillID id)
    {
        _skills[id].Delete();
        _skills.Remove(id);
    }

    /// <summary>
    /// 全てのスキルを取得する
    /// </summary>
    void AllSkillGet()
    {
        if (_allSkills == null)
        {
            _allSkills = new Dictionary<SkillID, ISkill>();
        }
        _allSkills.Add(SkillID.AddHP, SkillIDCheck(SkillID.AddHP, new AddHp()));
    }
}
