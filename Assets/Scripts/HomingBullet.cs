﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// 自動追尾弾の制御コンポーネント
/// </summary>
[RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class HomingBullet : MonoBehaviour, IObjectPool
{

    /// <summary>オブジェクトが有効か</summary>
    bool _isActive;
    /// <summary>生存可能時間</summary>
    float _lifeTime = 30f;
    /// <summary>生存時間</summary>
    float _time;
    /// <summary>基礎攻撃力</summary>
    float _atk;
    /// <summary>移動速度</summary>
    float _speed;
    /// <summary>ターゲットのTransform</summary>
    Enemy _target;
    /// <summary>補正強度</summary>
    float _correctionStrength;
    /// <summary>爆破半径</summary>
    float _radius;
    /// <summary>現在の飛翔方向</summary>
    Vector3 _dirction;
    /// <summary>リジットボディ</summary>
    Rigidbody2D _rb;
    /// <summary>コライダー</summary>
    CircleCollider2D _cc;
    /// <summary>スプライトレンダラー</summary>
    SpriteRenderer _sr;

    public void Destroy()
    {
        _rb.Sleep();
        _sr.enabled = false;
        _cc.enabled = false;
        _isActive = false;
    }

    public void Instantiate(Vector3 position)
    {
        _isActive = true;
        transform.position = position;
        _rb.WakeUp();
        _sr.enabled = true;
        _cc.enabled = true;
        _time = _lifeTime;
    }

    public void SetUp()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cc = GetComponent<CircleCollider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _rb.gravityScale = 0;
        _rb.freezeRotation = true;
        _rb.Sleep();
        _sr.enabled = false;
        _cc.isTrigger = true;
        _cc.enabled = false;
        _isActive = false;
        _time = _lifeTime;
    }

    public void Fire(Enemy transform, Vector2 direction, HomingData data)
    {
        _target = transform;
        _dirction = direction.normalized;
        _atk = data.Atk;
        _speed = data.Speed;
        _correctionStrength = data.CorrectionStrength;
        _radius = data.Radius;
    }
    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            if (!_target.IsActive)
            {
                Destroy();
                return;
            }
            Vector3 cor = (_target.transform.position - transform.position) * _correctionStrength * Time.deltaTime;
            _dirction = (_dirction + cor).normalized;
            transform.position += _dirction * _speed * Time.deltaTime;
            if(Vector2.Distance(_target.transform.position, transform.position) <= _radius)
            {
                Hit();
            }
            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                Destroy();
            }
        }
    }

    /// <summary>
    /// 着弾
    /// </summary>
    void Hit()
    {
        Enemy[] es = EnemysManager.Instance.ActiveEnemyArray.
            Where(e => Vector2.Distance(e.transform.position, transform.position) <= _radius).ToArray();
        Array.ForEach(es, e => e.Damage((_atk + GameData.Instance.BaseAtk) * GameData.Instance.AtkFact));
        Destroy();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject != Player.Instance.gameObject)
    //    {
    //        Debug.Log(collision);
    //        Enemy e = collision.GetComponent<Enemy>();
    //        if (e)
    //        {
    //            e.Damage((_atk + GameData.Instance.BaseAtk) * GameData.Instance.AtkFact);
    //            Debug.Log("destroy");
    //            //Gun.Instance.BulletDestroy(this);
    //            Destroy();
    //        }
    //    }
    //}
}
