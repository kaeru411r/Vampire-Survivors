using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusTest : MonoBehaviour
{
    [SerializeField] float _speed;
    // Start is called before the first frame update
    void Awake()
    {
        GameData.Instance.BaseMaxHP = 20000;
    }

    // Update is called once per frame
    void Update()
    {
        GameData.Instance.BaseMoveSpeed = _speed;
    }
}
