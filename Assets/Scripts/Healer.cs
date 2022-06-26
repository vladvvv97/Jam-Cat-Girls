using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : Character
{
    override protected void Awake()
    {
        base.Awake();
        Health = GameManager.Instance.Healer.Health;
        AttackDamage = GameManager.Instance.Healer.AttackDamage;
        Resistance = GameManager.Instance.Healer.Resistance;
        DefenceBonus = GameManager.Instance.Healer.DefenceBonus;
        SkillValue = GameManager.Instance.Healer.SkillValue;
        ClassType = GameManager.Instance.Healer.classType;
        AttackType = GameManager.Instance.Healer.attackType;
        initialHealth = Health;
        initialAttackDamage = AttackDamage;
        initialResistance = Resistance;
        initialDefenceBonus = DefenceBonus;

        HealthNow = Health / 2;
    }

    override protected void Start()
    {
        base.Start();
    }
}
