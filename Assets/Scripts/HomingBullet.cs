using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// 自動追尾弾の制御コンポーネント
/// </summary>
[RequireComponent(typeof(SpriteRenderer), typeof(TrailRenderer))]
public class HomingBullet : MonoBehaviour, IObjectPool
{

    /// <summary>オブジェクトが有効か</summary>
    bool _isActive;
    /// <summary>生存可能時間</summary>
    float _lifeTime = 10f;
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
    /// <summary>ターゲットの座標</summary>
    Vector3 _targetPosition;
    /// <summary>スプライトレンダラー</summary>
    SpriteRenderer _sr;
    /// <summary>トレイルレンダラー</summary>
    TrailRenderer _tr;
    /// <summary>今ポーズ中か否か</summary>
    bool _isPause;
    float _trTime;

    public bool IsActive { get => _isActive; }

    private void OnDisable()
    {
        GameManager.Instance.OnPause -= OnPause;
        GameManager.Instance.OnResume -= OnResume;
    }
    public void Destroy()
    {
        _sr.enabled = false;
        _tr.enabled = false;
        _isActive = false;
    }

    public void Instantiate(Vector3 position)
    {
        _isActive = true;
        transform.position = position;
        _sr.enabled = true;
        _tr.enabled = true;
        _time = _lifeTime;
    }

    public void SetUp()
    {
        _sr = GetComponent<SpriteRenderer>();
        _tr = GetComponent<TrailRenderer>();
        _sr.enabled = false;
        _tr.enabled = false;
        _isActive = false;
        _time = _lifeTime;
        _trTime = _tr.time;
        GameManager.Instance.OnPause += OnPause;
        GameManager.Instance.OnResume += OnResume;
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
        if (_isActive && !_isPause)
        {
            if (_target.IsActive)
            {
                _targetPosition = _target.transform.position;
            }
            Vector3 cor = (_targetPosition - transform.position).normalized * _correctionStrength * Time.deltaTime;
            _dirction = (_dirction + cor).normalized;
            transform.position += _dirction * _speed * Time.deltaTime;
            if (Vector2.Distance(_targetPosition, transform.position) <= _radius)
            {
                Burst();
            }
            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                Burst();
            }
        }
    }

    void LockTargetPosition()
    {

    }

    /// <summary>
    /// 着弾
    /// </summary>
    void Burst()
    {
        Enemy[] es = EnemysManager.Instance.ActiveEnemyArray.
            Where(e => Vector2.Distance(e.transform.position, transform.position) <= _radius).ToArray();
        Array.ForEach(es, e => e.Damage((_atk + GameData.Instance.BaseAtk) * GameData.Instance.AtkFact));
        Destroy();
    }

    public void OnPause()
    {
        _isPause = true;
        _tr.time = float.PositiveInfinity;
    }

    public void OnResume()
    {
        _isPause = false;
        _tr.time = _trTime;
    }
}
