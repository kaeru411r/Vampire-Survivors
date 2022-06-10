using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour , IObjectPool
{
    Rigidbody2D _rb;
    SpriteRenderer _sr;
    CircleCollider2D _cc;

    EnemyData _data;
    float _hp;


    private void Start()
    {
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
    }

    public void Destroy()
    {
        _cc.enabled = false;
        _sr.enabled = false;
        _rb.Sleep();
    }
}

