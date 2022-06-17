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

    static public SkillManager Instance;

    //Dictionary<SkillID, SkillData> _allSkills;

    bool _isPause;


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
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        AllSkillGet();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        GameManager.Instance.OnPause += OnPause;
        GameManager.Instance.OnResume += OnResume;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPause -= OnPause;
        GameManager.Instance.OnResume -= OnResume;
    }
    private void Update()
    {
        if (!_isPause && GameManager.Instance.IsPlay)
        {
            foreach (var skill in Skills)
            {
                skill.Update();
            }
        }
    }

    public void SelectStart()
    {
        //_canvas.alpha = 1;

        //List<SkillSelectTable> table = new List<SkillSelectTable>();
        //var list = _allSkills.Where(s => GameManager.Level >= s.Level);

        //int totalProb = _allSkills.Sum(s => s.Probability);
        //int rand = UnityEngine.Random.Range(0, _allSkills.Count);

        //for (int i = 0; i < _selectList.Count; ++i)
        //{
        //    _selectTable[i] = null;
        //    _selectText[i].text = "";
        //}

        //for (int i = 0; i < _selectList.Count; ++i)
        //{
        //    foreach (var s in list)
        //    {
        //        if (rand < s.Probability)
        //        {
        //            _selectTable[i] = s;
        //            _selectText[i].text = s.Name;
        //            list = list.Where(ls => !(ls.Type == s.Type && ls.TargetId == s.TargetId));
        //            break;
        //        }
        //        rand -= s.Probability;
        //    }
        //}
    }


    /// <summary>
    /// スキルの取得、強化
    /// </summary>
    /// <param name="id"></param>
    public void AddSkill(int id)
    {
        AddSkill((SkillID)id);
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
                    Debug.Log(1);
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
        return GetSkill((SkillID)id);
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
        if (_allSkills.ContainsKey(id))
        {
            return _allSkills[id];
        }
        Debug.LogError($"Skill{id}は設定されていません");
        return null;
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
    /// 新スキルを実装する際はここで追加する
    /// </summary>
    void AllSkillGet()
    {
        if (_allSkills == null)
        {
            _allSkills = new Dictionary<SkillID, ISkill>();
        }
        _allSkills.Add(SkillID.AddHP, SkillIDCheck(SkillID.AddHP, AddHp.Instance));
        _allSkills.Add(SkillID.Gun, SkillIDCheck(SkillID.Gun, Gun.Instance));
        _allSkills.Add(SkillID.Homing, SkillIDCheck(SkillID.Homing, Horming.Instance));
    }
    public void OnPause()
    {
        _isPause = true;
    }

    public void OnResume()
    {
        _isPause = false;
    }
}
