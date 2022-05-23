using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("Å‘åHP")]
    [SerializeField] float _maxHp;
    [Tooltip("Šî‘bˆÚ“®‘¬“x")]
    [SerializeField] float _speed;
    [Tooltip("Šî‘bUŒ‚—Í")]
    [SerializeField] float _atk;
    [Tooltip("Šî‘b–hŒä—Í")]
    [SerializeField] float _def;
    [Tooltip("Šî‘b‰^")]
    [SerializeField] float _luck;
    [Tooltip("Šî‘bŒoŒ±’l”{—¦")]
    [SerializeField] float _expFact;
    [Tooltip("Šî‘bƒN[ƒ‹ƒ^ƒCƒ€”{—¦")]
    [SerializeField] float _ctFact;

    float _hp;

    float _exp;
    float _atkCorrection;
    float _expFactCorrection;
    float _ctFactCorrection;
    float _defCorrection;
    float _luckCorrection;

    // Start is called before the first frame update
    void Start()
    {
        _hp = _maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
