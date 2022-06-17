using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gunで飛ばす弾の制御コンポーネント
/// </summary>
[RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IObjectPool
{
    /// <summary>生存可能時間</summary>
    float _lifeTime = 5f;
    /// <summary>生存時間</summary>
    float _time;
    GunData _data;
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
    /// <summary>今ポーズ中か否か</summary>
    bool _isPause;

    public bool IsActive { get => _isActive;}



    // Update is called once per frame
    void Update()
    {
        if( _isActive && !_isPause && GameManager.Instance.IsPlay)
        {
            transform.position += _dirction * _data.Speed * Time.deltaTime;
            _time -= Time.deltaTime;
            if(_time <= 0)
            {
                Destroy();
            }
        }
    }
    private void OnDisable()
    {
        GameManager.Instance.OnPause -= OnPause;
        GameManager.Instance.OnResume -= OnResume;
    }

    /// <summary>セットアップ</summary>
    /// <param name="data"></param>
    public void Fire(Vector2 direction, GunData data)
    {
        _data = data;
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
        _time = _lifeTime;
        GameManager.Instance.OnPause += OnPause;
        GameManager.Instance.OnResume += OnResume;
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
        if (collision.CompareTag("Enemy"))
        {
            if (_data.FrequencyPiercing > 0)
            {
                Enemy e = collision.GetComponent<Enemy>();
                if (e && !_hitEnemyList.Contains(e))
                {
                    e.Damage((_data.Atk + GameData.Instance.BaseAtk) * GameData.Instance.AtkFact);
                    _hitEnemyList.Add(e);
                }
                _data.FrequencyPiercing--;
                if (_data.FrequencyPiercing == 0)
                {
                    Gun.Instance.BulletDestroy(this);
                    Destroy();
                }
            }
        }
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
