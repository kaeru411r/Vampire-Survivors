using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの操作、管理をするコンポーネント
/// </summary>
public class Player : MonoBehaviour
{
    [Tooltip("最大HP")]
    [SerializeField] float _maxHp;

    //基礎ステータス
    [Tooltip("基礎移動速度")]
    [SerializeField] float _speed;
    [Tooltip("基礎攻撃力")]
    [SerializeField] float _atk;
    [Tooltip("基礎防御力")]
    [SerializeField] float _def;
    [Tooltip("基礎運")]
    [SerializeField] float _luck;
    [Tooltip("基礎経験値倍率")]
    [SerializeField] float _expFact;
    [Tooltip("基礎クールタイム倍率")]
    [SerializeField] float _ctFact;

    float _hp;

    //ステータス補正値
    float _exp;
    float _atkCorrection;
    float _expFactCorrection;
    float _ctFactCorrection;
    float _defCorrection;
    float _luckCorrection;

    // Start is called before the first frame update
    void Start()
    {
        _hp = _maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void A(int a)
    {
        Debug.Log((SkillDef) a);
    }
}
