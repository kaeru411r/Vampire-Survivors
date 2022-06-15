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
    const int _maxAmount = 100;
    /// <summary>飛ばす弾のオブジェクトプール</summary>
    List<HomingBullet> _inactiveBullets = new List<HomingBullet>();
    /// <summary>飛んでる弾のオブジェクトプール</summary>
    List<HomingBullet> _activeBullets = new List<HomingBullet>();


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
    /// <summary>補正強度</summary>
    float _correctionStrength;
    /// <summary>発射後経過時間</summary>
    float _time;


    HomingData[] _levelTable =
    {
        new HomingData(1, 30, 20, 3, 1.5f),
        new HomingData(2, 30, 20, 3, 1.5f),
    };

    public void Delete()
    {
        throw new System.NotImplementedException();
    }

    public void LevelUp()
    {
        StatusSet();
        if(_time < _coolTime * GameData.Instance.CoolTimeFact)
        {
            _time = _coolTime;
        }
    }

    public void SetUp()
    {
        StatusSet();
        _time = _coolTime;
        HomingBullet bullet = Resources.Load<HomingBullet>(_bulletName);
        GameObject root = new GameObject();
        root.name = "Bullets";
        for (int i = 0; i < _maxAmount; i++)
        {
            HomingBullet b = Object.Instantiate(bullet, root.transform);
            b.SetUp();
            _inactiveBullets.Add(b);
        }
    }

    void StatusSet()
    {
        if (_level < _levelTable.Length)
        {
            _amount = _levelTable[_level].Amount;
            _atk = _levelTable[_level].Atk;
            _coolTime = _levelTable[_level].CoolTime;
            _speed = _levelTable[_level].Speed;
            _correctionStrength = _levelTable[_level].CorrectionStrength;
            _level++;
            if (_level == _levelTable.Length)
            {
                _isLevelMax = true;
            }
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
                        BulletDestroy(_activeBullets[0]);
                    }
                    _inactiveBullets[0].Instantiate(pos);
                    float t = Random.Range(0, Mathf.PI * 2);
                    Vector2 dir = new Vector2(Mathf.Sin(t), Mathf.Cos(t));
                    _inactiveBullets[0].Fire(es[i].transform, dir, _atk, _speed, _correctionStrength);
                    _activeBullets.Add(_inactiveBullets[0]);
                    _inactiveBullets.RemoveAt(0);
                }
            }
            _time -= ct;
        }
    } 

    public void BulletDestroy(HomingBullet bullet)
    {
        _activeBullets.Remove(bullet);
        _inactiveBullets.Add(bullet);
    }
}


public struct HomingData
{
    public HomingData(int amount, float atk, float speed, float coolTime, float correctionStrength)
    {
        Amount = amount;
        Atk = atk;
        Speed = speed;
        CoolTime = coolTime;
        CorrectionStrength = correctionStrength;

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
}
