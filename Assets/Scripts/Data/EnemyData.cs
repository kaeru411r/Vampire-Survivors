﻿using UnityEngine;
using System.Linq;


/// <summary>
/// エネミーのステータスを保管する型
/// </summary>
public class EnemyData : MonoBehaviour
{
    public float HP;
    public float Speed;
    public float Atk;
    public float Radius;
    public Sprite Sprite;
    public EnemyType Type;
    public ExpTable[] ExpTable;
    public float Exp
    {
        get
        {
            int all = ExpTable.Sum(e => e.Probability);
            int value = Random.Range(0, all + 1);
            for (int i = 0; i < ExpTable.Length; i++)
            {
                if (value < ExpTable[i].Probability)
                {
                    return ExpTable[i].Exp;
                }
                value -= ExpTable[i].Probability;
            }
            return 0;
        }
    }
    [HideInInspector]
    public Vector2 Direction;
}

[System.Serializable]
public class ExpTable
{
    public float Exp;
    public int Probability;
}

/// <summary>
/// エネミーの行動パターン
/// </summary>
public enum EnemyType
{
    /// <summary>プレイヤーにまっすぐ向かっていく</summary>
    Charge,
    /// <summary>直進する</summary>
    Straight,
}