using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネミーのステータスを統括管理するクラス
/// </summary>
public class EnemysManager : MonoBehaviour
{
    static EnemysManager Instance;


    /// <summary>エネミーのステータスと出現率を入れる配列を時間分に入れる配列</summary>
    [SerializeField] DegreeEnemySpawn[] _enemySpawnArray;
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 現在のエネミーのデータを返す
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public EnemyData GetData()
    {
        
        var data = _enemySpawnArray[Mathf.Min(GameManager.Instance.Degree, _enemySpawnArray.Length - 1)];
        
        return null;
    }
}



[System.Serializable]
struct EnemySpawn
{
    public EnemyData data;
    public int probability;
}

[System.Serializable]
struct DegreeEnemySpawn
{
    public EnemySpawn[] enemySpawnsArray;
}
