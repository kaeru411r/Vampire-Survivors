using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// �v���C���[�̑���A�Ǘ�������R���|�[�l���g
/// </summary>
public class Player : MonoBehaviour
{
    [Tooltip("�ő�HP")]
    [SerializeField] float _maxHp;

    //��b�X�e�[�^�X
    [Tooltip("��b�ړ����x")]
    [SerializeField] float _speed;
    [Tooltip("��b�U����")]
    [SerializeField] float _atk;
    [Tooltip("��b�h���")]
    [SerializeField] float _def;
    [Tooltip("��b�^")]
    [SerializeField] float _luck;
    [Tooltip("��b�o���l�{��")]
    [SerializeField] float _expFact;
    [Tooltip("��b�N�[���^�C���{��")]
    [SerializeField] float _ctFact;

    /// <summary>HP</summary>
    float _hp;
    /// <summary>�X�L�����X�g</summary>
    List<ISkill> _skills = new List<ISkill> ();

    //�X�e�[�^�X�␳�l
    float _exp;
    float _atkCorrection;
    float _expFactCorrection;
    float _ctFactCorrection;
    float _defCorrection;
    float _luckCorrection;

    // Start is called before the first frame update
    void Start()
    {
        _hp = _maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSkill(int id)
    {
        int hs = 0;
        _skills.ForEach(s => hs += s.ID == (SkillDef)id ? 1 : 0);
        if (hs != 0)
        {
            switch (id)
            {
                default:
                    break;
            }
        }
    }
}
