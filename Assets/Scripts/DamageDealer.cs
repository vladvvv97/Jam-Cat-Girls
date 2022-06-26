using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : Character
{

    override protected void Awake()
    {
        base.Awake();
        Health = GameManager.Instance.DamageDealer.Health;
        AttackDamage = GameManager.Instance.DamageDealer.AttackDamage;
        Resistance = GameManager.Instance.DamageDealer.Resistance;
        DefenceBonus = GameManager.Instance.DamageDealer.DefenceBonus;
        SkillValue = GameManager.Instance.DamageDealer.SkillValue;
        ClassType = GameManager.Instance.DamageDealer.classType;
        AttackType = GameManager.Instance.DamageDealer.attackType;
        initialHealth = Health;
        initialAttackDamage = AttackDamage;
        initialResistance = Resistance;
        initialDefenceBonus = DefenceBonus;
        initialSkillValue = SkillValue;

        HealthNow = Health / 2;
    }

    override protected void Start()
    {
        base.Start();
    }
}
