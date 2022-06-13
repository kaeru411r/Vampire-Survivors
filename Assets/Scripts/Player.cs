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
    }

    private void Update()
    {
        Move();
    }

    /// <summary>
    /// プレイヤーの移動関数
    /// </summary>
    private void Move()
    {
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _rb.velocity = dir * GameData.Instance.MoveSpeed;
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
}
