using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBase : MonoBehaviour
{
    [SerializeField] float _hp;
    [SerializeField] float _moveSpeed;

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
    /// “G‚ª“®‚­
    /// </summary>
    private void Move()
    {
        Vector3 pPos = GameManager.Player.transform.position;
        Vector3 dir = (pPos - transform.position).normalized;
        _rb.velocity = dir * _moveSpeed;
    }

    /// <summary>
    /// “G‚Éƒ_ƒ[ƒW‚ğ—^‚¦‚é
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage)
    {
        _hp -= damage;
        GameManager.Instance.AddEnemyDamageLog($"{name}‚É{damage}ƒ_ƒ[ƒW");
        if(_hp <= 0)
        {
            Death();
        }
    }

    /// <summary>
    /// “G‚ª€‚Ê
    /// </summary>
    public void Death()
    {
        GameManager.Instance.AddEnemyDeathLog($"{name}‚ª“|‚ê‚½");
    }

}
