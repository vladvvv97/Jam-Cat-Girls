using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Character
{
    override protected void Awake()
    {
        base.Awake();
        Health = GameManager.Instance.Tank.Health;
        AttackDamage = GameManager.Instance.Tank.AttackDamage;
        Resistance = GameManager.Instance.Tank.Resistance;
        DefenceBonus = GameManager.Instance.Tank.DefenceBonus;
        SkillValue = GameManager.Instance.Tank.SkillValue;
        ClassType = GameManager.Instance.Tank.classType;
        AttackType = GameManager.Instance.Tank.attackType;
        initialHealth = Health; 
        initialAttackDamage = AttackDamage;
        initialResistance = Resistance;
        initialDefenceBonus = DefenceBonus;
        initialSkillValue = SkillValue;

        HealthNow = Health;
    }

    override protected void Start()
    {
        base.Start();
    }

}
