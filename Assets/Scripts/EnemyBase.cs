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

    private void Move()
    {
        Vector3 pPos = GameManager.Player.transform.position;
        Vector3 dir = (pPos - transform.position).normalized;
        _rb.velocity = dir;
    }

    public void Damage(float damage)
    {
        _hp -= damage;
        if(_hp <= 0)
        {

        }
    }

    public void Death()
    {

    }

}
