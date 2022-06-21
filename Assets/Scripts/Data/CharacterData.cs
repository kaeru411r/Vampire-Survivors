using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    //基礎
    [SerializeField] float _atk = 1;
    [SerializeField] float _def = 1;
    [SerializeField] float _maxHP = 100;
    [SerializeField] float _recovery = 0;
    [SerializeField] float _attackArea = 1;
    [SerializeField] float _attackSpeed = 1;
    [SerializeField] float _duration = 1;
    [SerializeField] float _moveSpeed = 1;
    [SerializeField] float _collectionArea = 1;
    [SerializeField] float _luck = 1;


    public void Select()
    {
        GameData.Instance.AtkFact = _atk;
        GameData.Instance.DefFact = _def;
        GameData.Instance.BaseMaxHP = _maxHP;
        GameData.Instance.AttackAreaFact = _attackArea;
        GameData.Instance.AttackSpeedFact = _attackSpeed;
        GameData.Instance.DurationFact = _duration;
        GameData.Instance.BaseRecovery = _recovery;
        GameData.Instance.MoveSpeedFact = _moveSpeed;
        GameData.Instance.BaseCollectionArea = _collectionArea;
        GameData.Instance.BaseLuck = _luck;
    }
}
