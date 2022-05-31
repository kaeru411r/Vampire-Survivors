using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// プレイヤーの操作、管理をするコンポーネント
/// </summary>
public class Player : SingletonMonoBehaviour<Player>
{

    //基礎ステータス
    [Tooltip("基礎最大HP")]
    [SerializeField] float _baseMaxHp;
    [Tooltip("基礎移動速度")]
    [SerializeField] float _baseSpeed;
    [Tooltip("基礎攻撃力")]
    [SerializeField] float _baseAtk;
    [Tooltip("基礎防御力")]
    [SerializeField] float _baseDef;
    [Tooltip("基礎運")]
    [SerializeField] float _baseLuck;
    [Tooltip("基礎経験値倍率")]
    [SerializeField] float _baseExpFact;
    [Tooltip("基礎クールタイム倍率")]
    [SerializeField] float _baseCtFact;

    /// <summary>最大HP</summary>
    float _maxHp;
    /// <summary>現在の残りHP</summary>
    float _hp;
    /// <summary>スキルリスト</summary>
    List<ISkill> _skills = new List<ISkill>();

    //ステータス補正値
    float _maxHPPlus;
    float _exp;
    float _atkCorrection;
    float _expFactCorrection;
    float _ctFactCorrection;
    float _defCorrection;
    float _luckCorrection;

    public float MaxHPPlus { get => _maxHPPlus; set => _maxHPPlus = value; }
    public float Exp { get => _exp; set => _exp = value; }
    public float AtkCorrection { get => _atkCorrection; set => _atkCorrection = value; }
    public float ExpFactCorrection { get => _expFactCorrection; set => _expFactCorrection = value; }
    public float CtFactCorrection { get => _ctFactCorrection; set => _ctFactCorrection = value; }
    public float DefCorrection { get => _defCorrection; set => _defCorrection = value; }
    public float LuckCorrection { get => _luckCorrection; set => _luckCorrection = value; }

    // Start is called before the first frame update
    void Start()
    {
        _hp = _maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        _skills.ForEach(s => s.Update());
    }

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

    public void Damage(float damage)
    {
        _hp -= damage;
        GameManager.Instance.AddPlayerDamageLog($"プレイヤーが{damage}ダメージを受けた");
        if (_hp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        GameManager.Instance.AddPlayerDeathLog($"プレイヤーが死亡した");
    }
}
