﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour, IObjectPool
{
    [Tooltip("経験値のプレハブ")]
    [SerializeField] Exp _baseExp;
    [Tooltip("経験値のオブジェクト量")]
    [SerializeField] int _maxExpAmount = 1000;

    /// <summary>有効なエネミーのリスト</summary>
    static List<Exp> _activeExpList = new List<Exp>();
    /// <summary>無効なエネミーのリスト</summary>
    static List<Exp> _inactiveExpList = new List<Exp>();

    bool isActive;

    Rigidbody2D _rb;
    SpriteRenderer _sr;
    CircleCollider2D _cc;

    EnemyData _data;
    float _hp;
    /// <summary>今ポーズ中か否か</summary>
    bool _isPause;

    public bool IsActive { get => isActive; }

    static void ExpActiveCheck()
    {
        for(int i = 0; i < _activeExpList.Count; i++)
        {
            if (!_activeExpList[i].IsActive)
            {
                _inactiveExpList.Add(_activeExpList[i]);
                _activeExpList.RemoveAt(i);
                i--;
            }
        }
    }

    void ExpsSetUp()
    {
        var root = new GameObject();
        root.name = "Exps";
        for (int i = 0; i < _maxExpAmount; i++)
        {
            Exp e = Instantiate(_baseExp, root.transform);
            e.SetUp();
            _inactiveExpList.Add(e);
        }
    }

    private void Start()
    {
        if (_activeExpList.Count + _inactiveExpList.Count <= 0)
        {
            ExpsSetUp();
        }
    }

    private void Update()
    {
        if (isActive && !_isPause)
        {
            Move();
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// 敵が動く
    /// </summary>
    private void Move()
    {
        Vector2 dir = (Player.Instance.transform.position - transform.position).normalized;
        _rb.velocity = dir * _data.Speed;
    }

    /// <summary>
    /// 敵にダメージを与える
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage)
    {
        _hp -= damage;
        GameManager.Instance.AddEnemyDamageLog($"{name}に{damage}ダメージ");
        if (_hp <= 0)
        {
            Death();
        }
    }

    /// <summary>
    /// 敵が死ぬ
    /// </summary>
    public void Death()
    {
        ExpActiveCheck();
        Destroy();
        GameManager.Instance.AddEnemyDeathLog($"{name}が倒れた");
        if(_inactiveExpList.Count > 0)
        {
            _inactiveExpList[0].Instantiate(transform.position);
            _inactiveExpList[0].Set(_data.Exp);
            _activeExpList.Add(_inactiveExpList[0]);
            _inactiveExpList.RemoveAt(0);
        }
        else
        {
            _activeExpList[0].Set(_data.Exp + _activeExpList[0].Point);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }

    /// <summary>
    /// プレイヤーに攻撃する
    /// </summary>
    void Attack()
    {
        Player.Instance.Damage(_data.Atk);
    }

    public void SetUp()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _cc = GetComponent<CircleCollider2D>();
        _cc.enabled = false;
        _sr.enabled = false;
        _rb.Sleep();
        isActive = false;
        GameManager.Instance.OnPause += OnPause;
        GameManager.Instance.OnResume += OnResume;
    }

    public void Instantiate(Vector3 position)
    {
        _data = EnemysManager.Instance.GetData(GameManager.Instance.Degree);
        _cc.enabled = true;
        _sr.enabled = true;
        _rb.WakeUp();

        transform.position = position;
        _cc.radius = _data.Radius;
        _sr.sprite = _data.Sprite;
        _hp = _data.HP;
        isActive = true;
    }

    public void Destroy()
    {
        _cc.enabled = false;
        _sr.enabled = false;
        _rb.Sleep();
        isActive = false;
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

