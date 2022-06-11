using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    static GameData _instance = new GameData();
    private GameData() { }
    static public GameData Instance => _instance;


    //基礎
    public float BaseAtk { get => _atkBase; set => _atkBase = value; }
    public float BaseDef { get => _defBase; set => _defBase = value; }
    public float BaseMaxHP { get => _maxHPBase; set => _maxHPBase = value; }
    public float BaseRecovery { get => _recoveryBase; set => _recoveryBase = value; }
    public float BaseCT { get => _coolTimeBase; set => _coolTimeBase = value; }
    public float BaseAttackArea { get => _attackAreaBase; set => _attackAreaBase = value; }
    public float BaseAttackSpeed { get => _attackSpeedBase; set => _attackSpeedBase = value; }
    public float BaseDuration { get => _durationBase; set => _durationBase = value; }
    public float BaseAmount { get => _amountBase; set => _amountBase = value; }
    public float BaseMoveSpeed { get => _moveSpeedBase; set => _moveSpeedBase = value; }
    public float BaseCollectionArea { get => _collectionAreaBase; set => _collectionAreaBase = value; }
    public float BaseLuck { get => _luckBase; set => _luckBase = value; }


    //倍率
    public float AtkFact { get => _atkFact; set => _atkFact = value; }
    public float DefFact { get => _defFact; set => _defFact = value; }
    public float MaxHPFact { get => _maxHPFact; set => _maxHPFact = value; }
    public float RecoveryFact { get => _recoveryFact; set => _recoveryFact = value; }
    public float CoolTimeFact { get => _coolTimeFact; set => _coolTimeFact = value; }
    public float AttackAreaFact { get => _attackAreaFact; set => _attackAreaFact = value; }
    public float AttackSpeedFact { get => _attackSpeedFact; set => _attackSpeedFact = value; }
    public float DurationFact { get => _durationFact; set => _durationFact = value; }
    public float AmountFact { get => _amountFact; set => _amountFact = value; }
    public float MoveSpeedFact { get => _moveSpeedFact; set => _moveSpeedFact = value; }
    public float CollectionAreaFact { get => _collectionAreaFact; set => _collectionAreaFact = value; }
    public float LuckFact { get => _luckFact; set => _luckFact = value; }
    public float DifficultyFact { get => _difficultyFact; set => _difficultyFact = value; }


    //最終値
    /// <summary></summary>
    public float Atk { get { return _atkBase * _atkFact; } }
    /// <summary></summary>
    public float Def { get { return _defBase * _defFact; } }
    /// <summary></summary>
    public float MaxHP { get { return _maxHPBase * _maxHPFact; } }
    /// <summary></summary>
    public float Recovery { get { return _recoveryBase * _recoveryFact; } }
    /// <summary></summary>
    public float CoolTime { get { return _coolTimeBase * _coolTimeFact; } }
    /// <summary></summary>
    public float AttackArea { get { return _attackAreaBase * _attackAreaFact; } }
    /// <summary></summary>
    public float AttackSpeed { get { return _attackSpeedBase * _attackSpeedFact; } }
    /// <summary></summary>
    public float Duration { get { return _durationBase * _durationFact; } }
    /// <summary></summary>
    public float Amount { get { return _amountBase * _amountFact; } }
    /// <summary></summary>
    public float MoveSpeed { get { return _moveSpeedBase * _moveSpeedFact; } }
    /// <summary></summary>
    public float CollectionArea { get { return _collectionAreaBase * _collectionAreaFact; } }

    /// <summary></summary>
    float _atkBase;
    /// <summary></summary>
    float _defBase;
    /// <summary></summary>
    float _maxHPBase;
    /// <summary></summary>
    float _recoveryBase;
    /// <summary></summary>
    float _coolTimeBase;
    /// <summary></summary>
    float _attackAreaBase;
    /// <summary></summary>
    float _attackSpeedBase;
    /// <summary></summary>
    float _durationBase;
    /// <summary></summary>
    float _amountBase;
    /// <summary></summary>
    float _moveSpeedBase;
    /// <summary></summary>
    float _collectionAreaBase;
    /// <summary></summary>
    float _luckBase;
    /// <summary></summary>
    float _baseExpFact;
    /// <summary></summary>
    float _baseDifficulty;
    /// <summary></summary>
    float _base;
    ///// <summary></summary>
    //float _base;
    ///// <summary></summary>
    //float _base;
    ///// <summary></summary>
    //float _base;
    ///// <summary></summary>
    //float _base;
    ///// <summary></summary>
    //float _base;

    float _atkFact = 1;
    /// <summary></summary>
    float _defFact = 1;
    /// <summary></summary>
    float _maxHPFact = 1;
    /// <summary></summary>
    float _recoveryFact = 1;
    /// <summary></summary>
    float _coolTimeFact = 1;
    /// <summary></summary>
    float _attackAreaFact = 1;
    /// <summary></summary>
    float _attackSpeedFact = 1;
    /// <summary></summary>
    float _durationFact = 1;
    /// <summary></summary>
    float _amountFact = 1;
    /// <summary></summary>
    float _moveSpeedFact = 1;
    /// <summary></summary>
    float _collectionAreaFact = 1;
    /// <summary></summary>
    float _luckFact = 1;
    /// <summary></summary>
    float _difficultyFact = 1;
}
