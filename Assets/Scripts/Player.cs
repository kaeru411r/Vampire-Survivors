using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// プレイヤーの操作、管理をするコンポーネント
/// </summary>
public class Player
{
    static private Player _instance = new Player();
    private Player(){}
    static public Player Instance => _instance;

    //基礎ステータス
    /// <summary>基礎最大HP</summary>
    float _baseMaxHp;
    /// <summary>基礎移動速度</summary>
    float _baseSpeed;
    /// <summary>基礎攻撃力</summary>
    float _baseAtk;
    /// <summary>基礎防御力</summary>
    float _baseDef;
    /// <summary>基礎運</summary>
    float _baseLuck;
    /// <summary>基礎経験値倍率</summary>
    float _baseExpFact;
    /// <summary>基礎クールタイム倍率</summary>
    float _baseCtFact;

    /// <summary>最大HP</summary>
    float _maxHp;
    //ステータス補正値
    /// <summary>最大HPの倍数</summary>
    float _maxHPFact;
    /// <summary>移動速度の倍数</summary>
    float _speedFact;
    /// <summary>攻撃力の倍数</summary>
    float _atkFact;
    /// <summary>防御力の倍数</summary>
    float _defFact;
    /// <summary>獲得経験値量の倍数</summary>
    float _luckFact;

    /// <summary>現在の残りHP</summary>
    float _hp;
    /// <summary>スキルリスト</summary>
    List<SkillID> _skillIDs = new List<SkillID>();
    /// <summary>現在の経験値量</summary>
    float _exp;
    /// <summary></summary>
    public float Exp { get => _exp; }



    /// <summary>基礎最大HP</summary>
    public float BaseMaxHp { get => _baseMaxHp; set => _baseMaxHp = value; }
    /// <summary>基礎移動速度</summary>
    public float BaseSpeed { get => _baseSpeed; set => _baseSpeed = value; }
    /// <summary>基礎攻撃力</summary>
    public float BaseAtk { get => _baseAtk; set => _baseAtk = value; }
    /// <summary>基礎防御力</summary>
    public float BaseDef { get => _baseDef; set => _baseDef = value; }
    /// <summary>基礎運</summary>
    public float BaseLuck { get => _baseLuck; set => _baseLuck = value; }
    /// <summary>基礎経験値倍率</summary>
    public float BaseExpFact { get => _baseExpFact; set => _baseExpFact = value; }
    /// <summary>基礎クールタイム倍率</summary>
    public float BaseCtFact { get => _baseCtFact; set => _baseCtFact = value; }
    /// <summary>最大HPの倍数</summary>
    public float MaxHPFact { get => _maxHPFact; set => _maxHPFact = value; }
    /// <summary>移動速度の倍数</summary>
    public float SpeedFact { get => _speedFact; set => _speedFact = value; }
    /// <summary>攻撃力の倍数</summary>
    public float AtkFact { get => _atkFact; set => _atkFact = value; }
    /// <summary>防御力の倍数</summary>
    public float DefFact { get => _defFact; set => _defFact = value; }
    /// <summary>獲得経験値量の倍数</summary>
    public float LuckFact { get => _luckFact; set => _luckFact = value; }

    // Start is called before the first frame update
    void Start()
    {
        _hp = _maxHp;
        _skillIDs.ForEach(i => SkillManager.Instance.AddSkill(i));
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

    public void AddSkill(int id)
    {
        _skillIDs.Add((SkillID)id);
        SkillManager.Instance.AddSkill(id);
    }
}
