using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̑���A�Ǘ�������R���|�[�l���g
/// </summary>
public class Player : SingletonMonoBehaviour<Player>
{

    //��b�X�e�[�^�X
    [Tooltip("��b�ő�HP")]
    [SerializeField] float _baseMaxHp;
    [Tooltip("��b�ړ����x")]
    [SerializeField] float _baseSpeed;
    [Tooltip("��b�U����")]
    [SerializeField] float _baseAtk;
    [Tooltip("��b�h���")]
    [SerializeField] float _baseDef;
    [Tooltip("��b�^")]
    [SerializeField] float _baseLuck;
    [Tooltip("��b�o���l�{��")]
    [SerializeField] float _baseExpFact;
    [Tooltip("��b�N�[���^�C���{��")]
    [SerializeField] float _baseCtFact;

    /// <summary>�ő�HP</summary>
    float _maxHp;
    /// <summary>���݂̎c��HP</summary>
    float _hp;
    /// <summary>�X�L�����X�g</summary>
    [SerializeField] List<ISkill> _skills = new List<ISkill>();

    //�X�e�[�^�X�␳�l
    float _maxHPPlus;
    float _exp;
    float _atkCorrection;
    float _expFactCorrection;
    float _ctFactCorrection;
    float _defCorrection;
    float _luckCorrection;

    public float MaxHPPlus { get => _maxHPPlus; set => _maxHPPlus = value; }
    public float Exp { get => _exp; set => _exp = value; }
    public float AtkCorrection { get => _atkCorrection; set => _atkCorrection = value; }
    public float ExpFactCorrection { get => _expFactCorrection; set => _expFactCorrection = value; }
    public float CtFactCorrection { get => _ctFactCorrection; set => _ctFactCorrection = value; }
    public float DefCorrection { get => _defCorrection; set => _defCorrection = value; }
    public float LuckCorrection { get => _luckCorrection; set => _luckCorrection = value; }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _hp = _maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        _skills.ForEach(s => s.Update());
    }


    public void Damage(float damage)
    {
        _hp -= damage;
        GameManager.Instance.AddPlayerDamageLog($"�v���C���[��{damage}�_���[�W���󂯂�");
        if (_hp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        GameManager.Instance.AddPlayerDeathLog($"�v���C���[�����S����");
    }
}
