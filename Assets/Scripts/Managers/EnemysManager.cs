using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// エネミーのステータスを統括管理するクラス
/// </summary>
public class EnemysManager : MonoBehaviour
{
    static public EnemysManager Instance;

    [Tooltip("エネミーが湧く範囲")]
    [SerializeField] Erea _spawnErea;
    [Tooltip("エネミーが湧く半径")]
    [SerializeField] float _spawnRadius;
    [Tooltip("エネミーの限界量")]
    [SerializeField] int _maxEnemyAmount;
    [Tooltip("エネミーのステータスと出現率を入れる配列を時間分に入れる配列")]
    [SerializeField] List<DegreeEnemySpawn> _enemySpawnList;
    [Tooltip("エネミーの原型")]
    [SerializeField] Enemy _baseEnemy;

    /// <summary>有効なエネミーのリスト</summary>
    List<Enemy> _activeEnemyList = new List<Enemy>();
    /// <summary>無効なエネミーのリスト</summary>
    List<Enemy> _inactiveEnemyList = new List<Enemy>();
    /// <summary>今ポーズ中か否か</summary>
    bool _isPause;

    float _enemySpawnTime;

    /// <summary>有効なエネミーのリスト</summary>
    public Enemy[] ActiveEnemyArray { get => _activeEnemyList.ToArray();}

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EnemySetUp();
        GameManager.Instance.OnPause += OnPause;
        GameManager.Instance.OnResume += OnResume;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnPause -= OnPause;
        GameManager.Instance.OnResume -= OnResume;
    }

    private void Update()
    {
        if (!_isPause)
        {
            EnemyGenerate();
        }
    }

    /// <summary>
    /// エネミーの下準備をする
    /// </summary>
    void EnemySetUp()
    {
        var root = new GameObject();
        root.name = "Enemys";
        for (int i = 0; i < _maxEnemyAmount; i++)
        {
            Enemy e = Instantiate(_baseEnemy, root.transform);
            e.SetUp();
            _inactiveEnemyList.Add(e);
        }
    }

    /// <summary>
    /// エネミーが分ける条件を満たしているかチェックし、満たしていれば湧かす
    /// </summary>
    void EnemyGenerate()
    {
        ActiveCheck();
        int degree = Mathf.Min(_enemySpawnList.Count - 1, GameManager.Instance.Degree);
        while (SpawnConditionCheck())
        {
            float t = Random.Range(0, Mathf.PI * 2);
            Vector3 pos = new Vector3(Mathf.Sin(t), Mathf.Cos(t)) * _spawnRadius + Player.Instance.transform.position;
            EnemyInstantiate(pos);
            _enemySpawnTime -= _enemySpawnList[degree].Time;
        }

        _enemySpawnTime += Time.deltaTime;
    }

    void ActiveCheck()
    {
        for(int i = 0; i < _activeEnemyList.Count; i++)
        {
            if (!_activeEnemyList[i].IsActive)
            {
                _inactiveEnemyList.Add(_activeEnemyList[i]);
                _activeEnemyList.Remove(_activeEnemyList[i]);
                i--;
            }
        }
    }

    /// <summary>
    /// エネミーを湧かす
    /// </summary>
    /// <param name="position"></param>
    void EnemyInstantiate(Vector3 position)
    {
        _inactiveEnemyList[0].Instantiate(position);
        _activeEnemyList.Add(_inactiveEnemyList[0]);
        _inactiveEnemyList.RemoveAt(0);
    }

    public void EnemyDestroy(Enemy enemy)
    {
    }

    bool SpawnConditionCheck()
    {
        int degree = Mathf.Min(_enemySpawnList.Count - 1, GameManager.Instance.Degree);
        if (_enemySpawnTime > _enemySpawnList[degree].Time)
        {
            if (_inactiveEnemyList.Count > 0)
            {
                if (_enemySpawnList[degree].MaxAmount > _activeEnemyList.Count)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 現在のエネミーのデータを返す
    /// </summary>
    /// <param name="degree"></param>
    /// <returns></returns>
    public EnemyData GetData(int degree)
    {
        NullCheck();
        var data = _enemySpawnList[Mathf.Min(degree, _enemySpawnList.Count - 1)];
        int all = data.EnemySpawnsList.Sum(e => e.Probability);
        int num = Random.Range(0, all);
        for (int i = 0; i < data.EnemySpawnsList.Count; i++)
        {
            num -= data.EnemySpawnsList[i].Probability;
            if (num < 0)
            {
                return data.EnemySpawnsList[i].Data;
            }
        }
        return null;
    }

    /// <summary>
    /// EnemySpawnsListにnullやカウントが0のものが無いか確かめる。
    /// あれば消す
    /// </summary>
    void NullCheck()
    {
        for (int i = 0; i < _enemySpawnList.Count; i++)
        {
            _enemySpawnList[i].NullCheck();
            if (_enemySpawnList[i].EnemySpawnsList == null || _enemySpawnList[i].EnemySpawnsList.Count == 0)
            {
                _enemySpawnList.RemoveAt(i);
                i--;
            }
        }
    }

    public void OnPause()
    {
        _isPause = true;
    }

    public void OnResume()
    {
        _isPause = false;
    }
}


/// <summary>
/// エネミーのデータと出現確立
/// </summary>
[System.Serializable]
struct EnemySpawn
{
    [Tooltip("エネミーのデータ")]
    public EnemyData Data;
    [Tooltip("湧く比率")]
    public int Probability;
}


/// <summary>
/// インスペクター上で二次元配列の表示を実現するための型
/// </summary>
[System.Serializable]
struct DegreeEnemySpawn
{
    [Tooltip("インスペクター上で二次元配列の表示を実現するための型")]
    public List<EnemySpawn> EnemySpawnsList;
    [Tooltip("この段階で湧く敵の最大値")]
    public int MaxAmount;
    [Tooltip("敵の湧く頻度(秒)")]
    public float Time;

    /// <summary>
    /// 配列内のdataにNullが合ったら削除
    /// </summary>
    public void NullCheck()
    {
        for (int i = 0; i < EnemySpawnsList.Count; i++)
        {
            if (EnemySpawnsList[i].Data == null)
            {
                EnemySpawnsList.RemoveAt(i);
            }
        }
    }
}
