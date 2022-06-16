using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 経験値
/// </summary>
[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class Exp : MonoBehaviour, IObjectPool
{

    [SerializeField] Sprite[] _spriteArray;
    float _point;
    SpriteRenderer _sr;
    CircleCollider2D _cc;

    bool _isActive;
    public void Set(float exp)
    {
        _point = exp;
        int i = Mathf.CeilToInt(_point) - 1;
        if(i < 0)
        {
            Destroy();
        }
        else if(i < _spriteArray.Length)
        {
            _sr.sprite = _spriteArray[i];
        }
        else
        {
            _sr.sprite = _spriteArray.Last();
        }
    }

    public void SetUp()
    {
        _sr = GetComponent<SpriteRenderer>();
        _cc = GetComponent<CircleCollider2D>();
        _sr.enabled = false;
        _cc.enabled = false;
        _isActive = false;
    }

    public void Instantiate(Vector3 position)
    {
        _sr.enabled = true;
        _cc.enabled = true;
        _isActive = true;
        transform.position = position;
    }

    public void Destroy()
    {
        _sr.enabled = false;
        _cc.enabled = false;
        _point = 0;
        _isActive = false;
    }

    /// <summary>経験値量</summary>
    public float Point { get => _point;}
    public bool IsActive { get => _isActive;}
}
