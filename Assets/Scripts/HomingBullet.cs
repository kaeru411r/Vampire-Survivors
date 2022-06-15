using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Transform _target;
    /// <summary>補正強度</summary>
    float _correctionStrength;
    /// <summary>現在の飛翔方向</summary>
    Vector3 _dirction;
    /// <summary>弾着予想時間</summary>
    float _impactTime;
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

    public void Fire(Transform transform, Vector2 direction, float atk, float speed, float correctionStrength)
    {
        _target = transform;
        _dirction = direction.normalized;
        _atk = atk;
        _speed = speed;
        _correctionStrength = correctionStrength;
    }
    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            Vector3 cor = (_target.position - transform.position) * _correctionStrength * Time.deltaTime;
            _dirction = (_dirction + cor).normalized;
            transform.position += _dirction * _speed * Time.deltaTime;
            float d = Vector2.Distance(transform.position, _target.position);
            //_impactTime = (_target.position - transform.position).magnitude - 
            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                Destroy();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != Player.Instance.gameObject)
        {
            Debug.Log(collision);
            Enemy e = collision.GetComponent<Enemy>();
            if (e)
            {
                e.Damage((_atk + GameData.Instance.BaseAtk) * GameData.Instance.AtkFact);
                Debug.Log("destroy");
                //Gun.Instance.BulletDestroy(this);
                Destroy();
            }
        }
    }
}
