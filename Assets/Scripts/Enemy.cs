using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour , IObjectPool
{
    [SerializeField] static List<EnemyData> _detaList = new List<EnemyData>();

    Rigidbody2D _rb;
    SpriteRenderer _sr;
    CircleCollider2D _cc;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    /// <summary>
    /// 敵が動く
    /// </summary>
    private void Move()
    {
    }

    /// <summary>
    /// 敵にダメージを与える
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage)
    {
        //_hp -= damage;
        //GameManager.Instance.AddEnemyDamageLog($"{name}に{damage}ダメージ");
        //if(_hp <= 0)
        //{
        //    Death();
        //}
    }

    /// <summary>
    /// 敵が死ぬ
    /// </summary>
    public void Death()
    {
        GameManager.Instance.AddEnemyDeathLog($"{name}が倒れた");
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
        //Player.Instance.Damage(_atk);
    }

    public void SetUp()
    {
        throw new System.NotImplementedException();
    }

    public void Instantiate()
    {
        throw new System.NotImplementedException();
    }

    public void Destroy()
    {
        throw new System.NotImplementedException();
    }
}

