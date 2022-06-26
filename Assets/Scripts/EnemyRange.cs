using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : Enemy
{
    override protected void Awake()
    {
        base.Awake();
        Health = GameManager.Instance.EnemyRange.Health;
        AttackDamage = GameManager.Instance.EnemyRange.AttackDamage;
        Resistance = GameManager.Instance.EnemyRange.Resistance;
        DefenceBonus = GameManager.Instance.EnemyRange.DefenceBonus;
        //SkillValue = GameManager.Instance.EnemyRange.SkillValue;
        //ClassType = GameManager.Instance.EnemyRange.classType;
        AttackType = GameManager.Instance.EnemyRange.attackType;
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
