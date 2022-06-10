using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IObjectPool
{
    /// <summary>
    /// オブジェクトをプールするときに呼ぶ
    /// </summary>
    void SetUp();

    /// <summary>
    /// オブジェクトを有効にするとき呼ぶ
    /// </summary>
    /// <param name="position"></param>
    void Instantiate(Vector3 position);

    /// <summary>
    /// オブジェクト無効時に呼ぶ
    /// </summary>
    void Destroy();
}
