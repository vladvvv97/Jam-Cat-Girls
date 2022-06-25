using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "Game Data", order = 51)]
public class GameData : ScriptableObject
{
    public ManaData Mana;
    public GlobalData Global;
    public UnitsData Units;
    public EnemyUnitsData EnemyUnits;
    public BuffsData Buffs;
    public SpellsData Spells;
}

[System.Serializable]
public class ManaData
{
    public float ManaRegenerationRate;
    public int ManaPerTick;
    public int ManaStartValue;
}
[System.Serializable]
public class GlobalData
{
    public int MaxNumCardsInHand;
    public float MeleeAtackRange;
    public float MinAttackSpeed;
}
[System.Serializable]
public class UnitsData
{
    [Header("Swordsman")]
    public float SwordsmanHealth;
    public float SwordsmanDamage;
    public float SwordsmanAttackSpeed;
    public float SwordsmanAttackDistance;
    public float SwordsmanDamageAbsorption;
    public int SwordsmanManaCost;

    [Header("Crossbowman")]
    public float CrossbowmanHealth;
    public float CrossbowmanDamage;
    public float CrossbowmanAttackSpeed;
    public float CrossbowmanArrowSpeed;
    public float CrossbowmanArmorPenetration;
    public int CrossbowmanManaCost;
}
[System.Serializable]
public class EnemyUnitsData
{
    [Header("Skeleton")]
    public float SkeletonHealth;
    public float SkeletonDamage;
    public float SkeletonAttackSpeed;
    public float SkeletonAttackDistance;
    public float SkeletonSpeed;
    public float SkeletonArmor;
}
[System.Serializable]
public class BuffsData
{
    [Header("Heal")]
    public float HealQuantity;
    public int HealCardManaCost;

    [Header("Rage")]
    public float RageAttackSpeed;
    public float RageDuration;
    public int RageCardManaCost;
}
[System.Serializable]
public class SpellsData
{
    [Header("FireExplosion")]
    public int FireExplosionWidth;
    public int FireExplosionHeight;
    public float FireExplosionDamage;
    public float FireExplosionDOTDamage;
    public int FireExplosionDuration;
    public int FireExplosionManaCost;

    [Header("IceBlast")]
    public int IceBlastWidth;
    public int IceBlastHeight;
    public float IceBlastDamage;
    public float IceBlastSpeedDebuff;
    public float IceBlastDuration;
    public int IceBlastManaCost;
}
