using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour
{
    [SerializeField] GameObject _go;
    const int _num = 100;
    GameObject _root;
    GameObject[] _gos = new GameObject[_num];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _num; i++)
        {
            _gos[i] = Instantiate(_go, _root.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_root == null)
        {
            _root = new GameObject();
            _root.name = "TestBullet";
        }
        for (int i = 0; i < _num; i++)
        {
            //_gos[i].Co
        }
    }
}
