using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILife
{
    float HP { get; }
    void Damage(float damage);
}
