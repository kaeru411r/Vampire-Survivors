using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// プレイヤーの操作、管理をするコンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Player : MonoBehaviour
{
    static public Player Instance;

    /// <summary>現在のプレイヤーのHP</summary>
    float _hp;
    /// <summary>リジットボディ</summary>
    Rigidbody2D _rb;
    string _expTag = "Exp";
    /// <summary>1フレームで入手できるEXPの限界</summary>
    int _maxExpGet = 10;
    /// <summary>取得予定の経験値</summary>
    float _exp = 0;
    /// <summary>今ポーズ中か否か</summary>
    bool _isPause;

    /// <summary>現在のプレイヤーのHP</summary>
    public float Hp { get => _hp; set => _hp = value; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _hp = GameData.Instance.MaxHP;
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(AddExp());
        GameManager.Instance.OnPause += OnPause;
        GameManager.Instance.OnResume += OnResume;
    }

    private void Update()
    {
        if (!_isPause)
        {
            Move();
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// プレイヤーの移動関数
    /// </summary>
    private void Move()
    {
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _rb.velocity = dir * GameData.Instance.MoveSpeed;
        //transform.position += (Vector3)dir * GameData.Instance.MoveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// プレイヤーの被弾関数
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage)
    {
        _hp -= damage;
        GameManager.Instance.AddPlayerDamageLog($"プレイヤーが{damage}ダメージを受けた");
        if (_hp <= 0)
        {
            Death();
        }
    }

    /// <summary>
    /// プレイヤーの被弾関数
    /// </summary>
    void Death()
    {
        GameManager.Instance.AddPlayerDeathLog($"プレイヤーが死亡した");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_expTag))
        {
            Exp e = collision.gameObject.GetComponent<Exp>();
            if (e)
            {
                _exp += e.Point * GameData.Instance.ExpFact;
                e.Destroy();
            }
        }
    }

    IEnumerator AddExp()
    {
        float exp;
        while (true)
        {
            if (_exp >= 1)
            {
                if(_exp <= _maxExpGet)
                {
                    exp = _exp;
                    _exp = 0;
                }
                else
                {
                    exp = _maxExpGet;
                    _exp -= _maxExpGet;
                }
                if (GameData.Instance.AddExp(exp))
                {
                    Debug.Log("レベルアップ");
                }
            }
            yield return null;
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
