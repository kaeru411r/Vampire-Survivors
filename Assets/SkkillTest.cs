using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkkillTest : MonoBehaviour
{
    bool isLevelUp;
    // Start is called before the first frame update
    void Start()
    {
        //SkillManager.Instance.AddSkill(SkillID.Homing);
        //SkillManager.Instance.AddSkill(SkillID.Gun);
        for (int i = 0; i < 10; i++)
        {
            SkillManager.Instance.AddSkill(SkillID.Homing);
            //SkillManager.Instance.AddSkill(SkillID.Gun);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
