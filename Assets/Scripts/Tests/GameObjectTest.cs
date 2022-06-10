using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectTest : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemy);
    }

    // Update is called once per frame
    void Update()
    {

        //enemy.SetUp();
        //enemy.Instantiate(transform.position);
    }
}
