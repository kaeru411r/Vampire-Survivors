using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    [Tooltip("ゲームの終了までの時間(分)")]
    [SerializeField] int _gameTime;

    /// <summary>エネミーのダメージログ</summary>
    StringBuilder _enemyDamageLog = new StringBuilder();
    /// <summary>エネミーの死亡ログ</summary>
    StringBuilder _enemyDeathLog = new StringBuilder();
    /// <summary>プレイヤーのダメージログ</summary>
    StringBuilder _playerDamageLog = new StringBuilder();
    /// <summary>プレイヤーの死亡ログ</summary>
    StringBuilder _playerDeathLog = new StringBuilder();
    /// <summary>現在のプレイ時間(秒)</summary>
    float _playTime;

    public System.Action OnPause;
    public System.Action OnResume;


    /// <summary>ゲームの進行段階</summary>
    public int Degree { get { return (int)Mathf.Floor(_playTime * 60); }}
    /// <summary>ゲームの終了までの時間(分)</summary>
    public int GameTime { get => _gameTime; set => _gameTime = value; }

    private void Awake()
    {
        Instance = this;
    }

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
        PlayerDamageLog();
        PlayerDeathLog();
    }

    /// <summary>
    /// 敵のダメージログの追加
    /// </summary>
    /// <param name="str"></param>
    public void AddEnemyDamageLog(string str)
    {
        if (_enemyDamageLog.Length == 0)
        {
            _enemyDamageLog.AppendLine("ダメージログ");
        }
        _enemyDamageLog.AppendLine(str);
    }

    /// <summary>
    /// 敵の死亡ログの追加
    /// </summary>
    /// <param name="str"></param>
    public void AddEnemyDeathLog(string str)
    {
        if (_enemyDeathLog.Length == 0)
        {
            _enemyDamageLog.AppendLine("死亡ログ");
        }
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
    }

    /// <summary>
    /// プレイヤーのダメージログの追加
    /// </summary>
    /// <param name="str"></param>
    public void AddPlayerDamageLog(string str)
    {
        if (_playerDamageLog.Length == 0)
        {
            _playerDamageLog.AppendLine("ダメージログ");
        }
        _playerDamageLog.AppendLine(str);
    }

    /// <summary>
    /// プレイヤーの死亡ログの追加
    /// </summary>
    /// <param name="str"></param>
    public void AddPlayerDeathLog(string str)
    {
        if (_playerDeathLog.Length == 0)
        {
            _playerDeathLog.AppendLine("死亡ログ");
        }
        _playerDeathLog.AppendLine(str);
    }

    /// <summary>
    /// プレイヤーのダメージログの出力
    /// </summary>
    void PlayerDamageLog()
    {
        if (_playerDamageLog.Length > 0)
        {
            Debug.Log(_playerDamageLog);
        }
        _playerDamageLog.Clear();
    }

    /// <summary>
    /// プレイヤーの死亡ログの出力
    /// </summary>
    void PlayerDeathLog()
    {
        if (_playerDeathLog.Length > 0)
        {
            Debug.Log(_playerDeathLog);
        }
        _playerDeathLog.Clear();
    }

    public void Pause()
    {
        OnPause.Invoke();
    }

    public void Resume()
    {
        OnResume.Invoke();
    }

}
