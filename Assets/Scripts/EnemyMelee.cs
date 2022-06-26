using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    override protected void Awake()
    {
        base.Awake();
        Health = GameManager.Instance.EnemyMelee.Health;
        AttackDamage = GameManager.Instance.EnemyMelee.AttackDamage;
        Resistance = GameManager.Instance.EnemyMelee.Resistance;
        DefenceBonus = GameManager.Instance.EnemyMelee.DefenceBonus;
        //SkillValue = GameManager.Instance.EnemyMelee.SkillValue;
        //ClassType = GameManager.Instance.EnemyMelee.classType;
        AttackType = GameManager.Instance.EnemyMelee.attackType;
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
