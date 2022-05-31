using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    [SerializeField] float _hp;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _atk;

    Rigidbody2D _rb;

    public float HP { get => _hp; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    /// <summary>
    /// �G������
    /// </summary>
    private void Move()
    {
        Vector3 pPos = GameManager.Player.transform.position;
        Vector3 dir = (pPos - transform.position).normalized;
        _rb.velocity = dir * _moveSpeed;
    }

    /// <summary>
    /// �G�Ƀ_���[�W��^����
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage)
    {
        _hp -= damage;
        GameManager.Instance.AddEnemyDamageLog($"{name}��{damage}�_���[�W");
        if(_hp <= 0)
        {
            Death();
        }
    }

    /// <summary>
    /// �G������
    /// </summary>
    public void Death()
    {
        GameManager.Instance.AddEnemyDeathLog($"{name}���|�ꂽ");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.Damage(_atk);
        }
    }
}
