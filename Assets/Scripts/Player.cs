using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("ÅåHP")]
    [SerializeField] float _maxHp;
    [Tooltip("îbÚ®¬x")]
    [SerializeField] float _speed;
    [Tooltip("îbUÍ")]
    [SerializeField] float _atk;
    [Tooltip("îbhäÍ")]
    [SerializeField] float _def;
    [Tooltip("îb^")]
    [SerializeField] float _luck;
    [Tooltip("îbo±l{¦")]
    [SerializeField] float _expFact;
    [Tooltip("îbN[^C{¦")]
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
