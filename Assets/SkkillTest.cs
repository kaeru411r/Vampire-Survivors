using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkkillTest : MonoBehaviour
{
    bool isLevelUp;
    // Start is called before the first frame update
    void Start()
    {
        SkillManager.Instance.AddSkill(SkillID.Homing);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > 10)
        {
            if (!isLevelUp)
            {
                isLevelUp = true;
                SkillManager.Instance.AddSkill(SkillID.Homing);
            }
        }
    }
}
