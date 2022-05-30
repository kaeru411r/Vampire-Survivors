using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    Player _player;
    static public Player Player => Instance._player;

    StringBuilder _enemyDamageLog = new StringBuilder();
    StringBuilder _enemyDeathLog = new StringBuilder();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        EnemyDamageLog();
        EnemyDeathLog();
    }

    /// <summary>
    /// 敵のダメージログの追加
    /// </summary>
    /// <param name="str"></param>
    public void AddEnemyDamageLog(string str)
    {
        _enemyDamageLog.AppendLine(str);
    }

    /// <summary>
    /// 敵の死亡ログの追加
    /// </summary>
    /// <param name="str"></param>
    public void AddEnemyDeathLog(string str)
    {
        _enemyDeathLog.AppendLine(str);
    }

    /// <summary>
    /// 敵のダメージログの出力
    /// </summary>
    void EnemyDamageLog()
    {
        if (_enemyDamageLog.Length > 0)
        {
            Debug.Log(_enemyDamageLog);
        }
        _enemyDamageLog.Clear();
        _enemyDamageLog.AppendLine("ダメージログ");
    }

    /// <summary>
    /// 敵の死亡ログの出力
    /// </summary>
    void EnemyDeathLog()
    {
        if (_enemyDeathLog.Length > 0)
        {
            Debug.Log(_enemyDeathLog);
        }
        _enemyDeathLog.Clear();
        _enemyDamageLog.AppendLine("死亡ログ");
    }

}
