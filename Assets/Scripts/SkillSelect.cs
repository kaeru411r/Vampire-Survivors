using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelect : MonoBehaviour
{
    Canvas _ca;
    private void Start()
    {
        _ca = GetComponent<Canvas>();
    }
    public void AddHP()
    {
        SkillManager.Instance.AddSkill(SkillID.AddHP);
        GameManager.Instance.Resume();
        Player.Instance.S();
    }
    public void Homing()
    {
        SkillManager.Instance.AddSkill(SkillID.Homing);
        GameManager.Instance.Resume();
        Player.Instance.S();
    }

    public void Gun()
    {
        SkillManager.Instance.AddSkill(SkillID.Gun);
        GameManager.Instance.Resume();
        Player.Instance.S();
    }
}
