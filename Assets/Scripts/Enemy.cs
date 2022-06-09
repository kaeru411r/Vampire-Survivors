using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] List<EnemyData> _detaList = new List<EnemyData>();

    Rigidbody2D _rb;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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
}


[System.Serializable]
struct EnemyData
{
    public float hp;
    public float speed;
    public float atk;
}