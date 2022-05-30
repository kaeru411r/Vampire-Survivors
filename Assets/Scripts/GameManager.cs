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
    /// �G�̃_���[�W���O�̒ǉ�
    /// </summary>
    /// <param name="str"></param>
    public void AddEnemyDamageLog(string str)
    {
        _enemyDamageLog.AppendLine(str);
    }

    /// <summary>
    /// �G�̎��S���O�̒ǉ�
    /// </summary>
    /// <param name="str"></param>
    public void AddEnemyDeathLog(string str)
    {
        _enemyDeathLog.AppendLine(str);
    }

    /// <summary>
    /// �G�̃_���[�W���O�̏o��
    /// </summary>
    void EnemyDamageLog()
    {
        if (_enemyDamageLog.Length > 0)
        {
            Debug.Log(_enemyDamageLog);
        }
        _enemyDamageLog.Clear();
        _enemyDamageLog.AppendLine("�_���[�W���O");
    }

    /// <summary>
    /// �G�̎��S���O�̏o��
    /// </summary>
    void EnemyDeathLog()
    {
        if (_enemyDeathLog.Length > 0)
        {
            Debug.Log(_enemyDeathLog);
        }
        _enemyDeathLog.Clear();
        _enemyDamageLog.AppendLine("���S���O");
    }

}
