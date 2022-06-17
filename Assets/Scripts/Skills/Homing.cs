using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// ホーミング弾のスキル
/// </summary>
public class Horming : ISkill
{
    static Horming instance = new Horming();
    private Horming() { }
    public static Horming Instance { get { return instance; } }

    public SkillID ID => SkillID.Homing;

    public SkillType Type => SkillType.Active;

    public bool IsLevelMax => _isLevelMax;


    /// <summary>スキルがレベルアップ可能か</summary>
    bool _isLevelMax;
    /// <summary>飛ばす弾のプレハブの名前</summary>
    const string _bulletName = "HomingBullet";
    const int _maxAmount = 10000;
    /// <summary>飛ばす弾のオブジェクトプール</summary>
    List<HomingBullet> _inactiveBullets = new List<HomingBullet>();
    /// <summary>飛んでる弾のオブジェクトプール</summary>
    List<HomingBullet> _activeBullets = new List<HomingBullet>();


    /// <summary>現在のスキルのレベル</summary>
    int _level = 0;
    /// <summary>発射後経過時間</summary>
    float _time;
    /// <summary>今ポーズ中か否か</summary>
    bool _isPause;


    HomingData[] _levelTable =
    {
        new HomingData(3, 100, 20, 1f, 20f, 0.7f),
        new HomingData(4, 100, 20, 1f, 20f, 0.7f),
        new HomingData(20, 100, 30, 0.5f, 20f, 0.7f),
        new HomingData(20, 100, 30, 0.1f, 20f, 1f),
        new HomingData(1, 100, 20, 0.005f, 20, 0.5f),
    };
    public void Delete()
    {
        _level = 0;
        _isLevelMax = false;
    }

    public void LevelUp()
    {
        StatusSet();
        if (_time < _levelTable[_level].CoolTime * GameData.Instance.CoolTimeFact)
        {
            _time = _levelTable[_level].CoolTime;
        }
    }

    public void SetUp()
    {
        _level = -1;
        StatusSet();
        _time = _levelTable[_level].CoolTime;
        HomingBullet bullet = Resources.Load<HomingBullet>(_bulletName);
        GameObject root = new GameObject();
        root.name = "HomingBullets";
        for (int i = 0; i < _maxAmount; i++)
        {
            HomingBullet b = Object.Instantiate(bullet, root.transform);
            b.SetUp();
            _inactiveBullets.Add(b);
        }
        GameManager.Instance.OnPause += OnPause;
        GameManager.Instance.OnResume += OnResume;
    }

    void StatusSet()
    {
        if (!_isLevelMax)
        {
            _level++;
            if (_level == _levelTable.Length - 1)
            {
                _isLevelMax = true;
            }
        }
    }

    public void Update()
    {
        if (!_isPause)
        {
            ActiveCheck();
            _time += Time.deltaTime;
            float ct = _levelTable[_level].CoolTime * GameData.Instance.CoolTimeFact;
            for (; _time > ct; _time -= ct)
                //if (_time > ct)
            {
                Enemy[] es = EnemysManager.Instance.ActiveEnemyArray;
                if (es.Length > 0)
                {
                    Vector3 pos = Player.Instance.transform.position;
                    es = es.OrderBy(e => Random.value).ToArray();
                    for (int i1 = 0, i2 = 0; i1 < _levelTable[_level].Amount + GameData.Instance.Amount; i1++, i2++)
                    {
                        if (!(es.Length > i2))
                        {
                            i2 = 0;
                        }
                        if (_inactiveBullets.Count == 0)
                        {
                            BulletDestroy(_activeBullets[0]);
                        }
                        _inactiveBullets[0].Instantiate(pos);
                        float t = Random.Range(0, Mathf.PI * 2);
                        Vector2 dir = new Vector2(Mathf.Sin(t), Mathf.Cos(t));
                        _inactiveBullets[0].Fire(es[i2], dir, _levelTable[_level]);
                        _activeBullets.Add(_inactiveBullets[0]);
                        _inactiveBullets.RemoveAt(0);
                    }
                }
            }
        }
    }

    public void BulletDestroy(HomingBullet bullet)
    {
        _activeBullets.Remove(bullet);
        _inactiveBullets.Add(bullet);
    }

    void ActiveCheck()
    {
        for (int i = 0; i < _activeBullets.Count; i++)
        {
            if (!_activeBullets[i].IsActive)
            {
                _inactiveBullets.Add(_activeBullets[i]);
                _activeBullets.RemoveAt(i);
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


public struct HomingData
{
    public HomingData(int amount, float atk, float speed, float coolTime, float correctionStrength, float radius)
    {
        Amount = amount;
        Atk = atk;
        Speed = speed;
        CoolTime = coolTime;
        CorrectionStrength = correctionStrength;
        Radius = radius;
    }
    /// <summary>だす弾の基礎値</summary>
    public int Amount;
    /// <summary>弾のダメージ</summary>
    public float Atk;
    /// <summary>弾のスピード</summary>
    public float Speed;
    /// <summary>クールタイムの基礎値</summary>
    public float CoolTime;
    /// <summary>補正強度</summary>
    public float CorrectionStrength;
    /// <summary>爆破半径</summary>
    public float Radius;
}
