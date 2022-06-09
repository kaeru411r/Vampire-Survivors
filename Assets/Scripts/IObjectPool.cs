using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IObjectPool
{
    void SetUp();

    void Instantiate();

    void Destroy();
}
