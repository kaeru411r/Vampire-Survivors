using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Gun : ISkill
{
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
    int _level;
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


    public void Delete()
    {
        _amount = 1;
        _atk = 30;
        _coolTime = 1;
        _isLevelMax = false;
        _level = 0;
    }

    public void LevelUp()
    {
        switch (_level)
        {
            case 0:
                _level++;
                _atk += 30;
                break;
            case 1:
                _level++;
                _amount += 1;
                break;
            case 2:
                _level++;
                _coolTime -= 0.5f;
                break;
            default:
                break;
        }
    }

    public void SetUp()
    {
        _amount = 1;
        _atk = 30;
        _coolTime = 1;
        _speed = 2;
        _isLevelMax = false;
        _level = 0;
        Bullet bullet = Resources.Load<Bullet>(_bulletName);
        GameObject root = new GameObject();
        root.name = "Bullets";
        for (int i = 0; i < _maxAmount; i++)
        {
            Bullet b = Object.Instantiate(bullet, root.transform);
            b.SetUp();
            _inactiveBullets.Add(b);
        }
    }

    public void Update()
    {
        _time += Time.deltaTime;
        float ct = _coolTime * GameData.Instance.CoolTimeFact;
        if (_time > ct)
        {
            Enemy[] es = EnemysManager.Instance.ActiveEnemyArray;
            Debug.Log("撃った");
            Vector3 pos = Player.Instance.transform.position;
            es = es.OrderBy(e => Vector2.Distance(e.transform.position, Player.Instance.transform.position)).ToArray();
            for (int i = 0; i < _amount + GameData.Instance.Amount; i++)
            {
                if (es.Length > i)
                {
                    if (_inactiveBullets.Count == 0)
                    {
                        _inactiveBullets.Add(_activeBullets[0]);
                        _activeBullets.RemoveAt(0);
                    }
                    _inactiveBullets[0].Instantiate(pos);
                    _inactiveBullets[0].Fire((es[i].transform.position - pos).normalized, _atk, _speed);
                    _activeBullets.Add(_inactiveBullets[0]);
                    _inactiveBullets.RemoveAt(0);
                }
            }
            _time -= ct;
        }
    }
}
