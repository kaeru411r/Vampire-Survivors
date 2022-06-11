using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// プレイヤーの操作、管理をするコンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class Player : MonoBehaviour
{
    static public Player Instance;


    float _hp;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _hp = GameData.Instance.MaxHP;
    }

    private void Update()
    {
        
    }

    private void Move()
    {
        
    }

    public void Damage(float damage)
    {
        _hp -= damage;
        GameManager.Instance.AddPlayerDamageLog($"プレイヤーが{damage}ダメージを受けた");
        if (_hp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        GameManager.Instance.AddPlayerDeathLog($"プレイヤーが死亡した");
    }
}
