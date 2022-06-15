﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gunで飛ばす弾の制御コンポーネント
/// </summary>
[RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IObjectPool
{
    /// <summary>生存可能時間</summary>
    float _lifeTime = 30f;
    /// <summary>基礎攻撃力</summary>
    float _atk;
    /// <summary>移動速度</summary>
    float _speed;
    /// <summary>貫通可能回数</summary>
    int _frequencyPiercing;
    /// <summary>飛翔方向</summary>
    Vector3 _dirction;
    /// <summary>ヒットしたエネミーのリスト</summary>
    List<Enemy> _hitEnemyList = new List<Enemy>();
    /// <summary>オブジェクトが有効か</summary>
    bool _isActive;
    /// <summary>リジットボディ</summary>
    Rigidbody2D _rb;
    /// <summary>コライダー</summary>
    CircleCollider2D _cc;
    /// <summary>スプライトレンダラー</summary>
    SpriteRenderer _sr;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( _isActive)
        {
            transform.position += _dirction * _speed * Time.deltaTime;
        }
    }

    /// <summary>セットアップ</summary>
    /// <param name="atk"></param>
    /// <param name="speed"></param>
    /// <param name="Piercing"></param>
    public void Fire(Vector2 direction, float atk, float speed, int Piercing)
    {
        _atk = atk;
        _speed = speed;
        _frequencyPiercing = Piercing;
        _dirction = direction;
    }

    /// <summary>セットアップ</summary>
    /// <param name="atk"></param>
    /// <param name="speed"></param>
    public void Fire(Vector2 direction, float atk, float speed)
    {
        _atk = atk;
        _speed = speed;
        _dirction = direction;
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
    }

    public void Instantiate(Vector3 position)
    {
        _isActive = true;
        transform.position = position;
        _rb.WakeUp();
        _sr.enabled = true;
        _cc.enabled = true;
        _frequencyPiercing = 1;
    }

    public void Destroy()
    {
        _hitEnemyList.Clear();
        _rb.Sleep();
        _sr.enabled = false;
        _cc.enabled = false;
        _isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != Player.Instance.gameObject && !collision.CompareTag(gameObject.tag))
        {
            Debug.Log(collision);
            if (_frequencyPiercing > 0)
            {
                Enemy e = collision.GetComponent<Enemy>();
                if (e && !_hitEnemyList.Contains(e))
                {
                    e.Damage((_atk + GameData.Instance.BaseAtk) * GameData.Instance.AtkFact);
                    _hitEnemyList.Add(e);
                }
                _frequencyPiercing--;
                if (_frequencyPiercing == 0)
                {
                    Debug.Log("destroy");
                    Gun.Instance.BulletDestroy(this);
                    Destroy();
                }
            }
        }
    }
}
