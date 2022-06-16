using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Gun : ISkill
{
    static Gun instance = new Gun();
    private Gun() { }
    public static Gun Instance { get { return instance; } }

    public SkillID ID => SkillID.Gun;

    public SkillType Type => SkillType.Active;

    public bool IsLevelMax => _isLevelMax;

    /// <summary>飛ばす弾のプレハブの名前</summary>
    const string _bulletName = "GunBullet";
    const int _maxAmount = 100;
    /// <summary>飛ばす弾のオブジェクトプール</summary>
    List<Bullet> _inactiveBullets = new List<Bullet>();
    /// <summary>飛んでる弾のオブジェクトプール</summary>
    List<Bullet> _activeBullets = new List<Bullet>();

    /// <summary>スキルがレベルアップ可能か</summary>
    bool _isLevelMax = false;

    /// <summary>現在のスキルのレベル</summary>
    int _level = 0;
    /// <summary>だす弾の基礎値</summary>
    int _amount;
    /// <summary>弾のダメージ</summary>
    float _atk;
    /// <summary>弾のスピード</summary>
    float _speed;
    /// <summary>クールタイムの基礎値</summary>
    float _coolTime;
    /// <summary>発射後経過時間</summary>
    float _time;
    /// <summary>今ポーズ中か否か</summary>
    bool _isPause;

    GunData[] _levelTable =
    {
        new GunData(1, 30, 1, 1),
        new GunData(100, 100, 2, 1),
    };

    public void Delete()
    {
        _isLevelMax = false;
        _level = 0;
    }

    public void LevelUp()
    {
        StatusSet();
    }

    public void SetUp()
    {
        StatusSet();
        Bullet bullet = Resources.Load<Bullet>(_bulletName);
        GameObject root = new GameObject();
        root.name = "Bullets";
        for (int i = 0; i < _maxAmount; i++)
        {
            Bullet b = Object.Instantiate(bullet, root.transform);
            b.SetUp();
            _inactiveBullets.Add(b);
        }
        GameManager.Instance.OnPause += OnPause;
        GameManager.Instance.OnResume += OnResume;
    }

    void StatusSet()
    {
        if (_level < _levelTable.Length)
        {
            _amount = _levelTable[_level].Amount;
            _atk = _levelTable[_level].Atk;
            _coolTime = _levelTable[_level].CoolTime;
            _speed = _levelTable[_level].Speed;
            _level++;
            if (_level == _levelTable.Length)
            {
                _isLevelMax = true;
            }
        }
    }

    public void Update()
    {
        if (!_isPause)
        {
            _time += Time.deltaTime;
            float ct = _coolTime * GameData.Instance.CoolTimeFact;
            if (_time > ct)
            {
                Enemy[] es = EnemysManager.Instance.ActiveEnemyArray;
                if (es.Length > 0)
                {
                    Debug.Log("撃った");
                    Vector3 pos = Player.Instance.transform.position;
                    es = es.OrderBy(e => Vector2.Distance(e.transform.position, Player.Instance.transform.position)).ToArray();
                    for (int i1 = 0, i2 = 0; i1 < _amount + GameData.Instance.Amount; i1++, i2++)
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
                        _inactiveBullets[0].Fire((es[i2].transform.position - pos).normalized, _atk, _speed);
                        _activeBullets.Add(_inactiveBullets[0]);
                        _inactiveBullets.RemoveAt(0);

                    }
                    _time -= ct;
                }
            }
        }
    }

    public void BulletDestroy(Bullet bullet)
    {
        _activeBullets.Remove(bullet);
        _inactiveBullets.Add(bullet);
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
/// Gunのスペック
/// </summary>
public struct GunData
{
    public GunData(int amount, float atk, float speed, float coolTime)
    {
        Amount = amount;
        Atk = atk;
        Speed = speed;
        CoolTime = coolTime;
    }
    /// <summary>だす弾の基礎値</summary>
    public int Amount;
    /// <summary>弾のダメージ</summary>
    public float Atk;
    /// <summary>弾のスピード</summary>
    public float Speed;
    /// <summary>クールタイムの基礎値</summary>
    public float CoolTime;
}
