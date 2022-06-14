using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkkillTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SkillManager.Instance.AddSkill(SkillID.Gun);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
