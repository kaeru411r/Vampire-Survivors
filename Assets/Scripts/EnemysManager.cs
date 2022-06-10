using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エネミーのステータスを統括管理するクラス
/// </summary>
public class EnemysManager : MonoBehaviour
{
    static public EnemysManager Instance;


    /// <summary>エネミーのステータスと出現率を入れる配列を時間分に入れる配列</summary>
    [SerializeField] DegreeEnemySpawn[] _enemySpawnArray;
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 現在のエネミーのデータを返す
    /// </summary>
    /// <param name="degree"></param>
    /// <returns></returns>
    public EnemyData GetData(int degree)
    {
        var data = _enemySpawnArray[Mathf.Min(degree, _enemySpawnArray.Length - 1)];
        int all =  0;
        foreach(var e in data.EnemySpawnsArray)
        {
            all += e.Probability;
        }
        int num = Random.Range(0, all);
        for(int i = 0; i < data.EnemySpawnsArray.Length; i++)
        {
            num -= data.EnemySpawnsArray[i].Probability;
            if(num < 0)
            {
                return data.EnemySpawnsArray[i].Data;
            }
        }
        return null;
    }
}


/// <summary>
/// エネミーのデータと出現確立
/// </summary>
[System.Serializable]
struct EnemySpawn
{
    public EnemyData Data;
    public int Probability;
}

/// <summary>
/// インスペクター上で二次元配列の表示を実現するための型
/// </summary>
[System.Serializable]
struct DegreeEnemySpawn
{
    public EnemySpawn[] EnemySpawnsArray;
}
