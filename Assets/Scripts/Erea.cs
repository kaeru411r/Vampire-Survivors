using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Erea
{
    [Tooltip("上の限界")]
    public float TopLimit;
    [Tooltip("下の限界")]
    public float UnderLimit;
    [Tooltip("右の限界")]
    public float RightLimit;
    [Tooltip("左の限界")]
    public float LeftLimit;
}
