using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; private set => _instance = value; }

    public TankData Tank;
    public DamageDealerData DamageDealer;
    public HealerData Healer;
    public EnemyMeleeData EnemyMelee;
    public EnemyMeleeData EnemyRange;

    public RandomEffectData RandomEffects;
    void Awake()
    {
        InitalizeSingleton();
    }
    private void InitalizeSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    [System.Serializable]
    public class TankData
    {
        public ClassType classType;
        public AttackType attackType;
        public float Health;
        public float AttackDamage;
        public float SkillValue;
        [Range(0, 100)] public int Resistance;
        [Range(0, 100)] public int DefenceBonus;
    }
    [System.Serializable]
    public class DamageDealerData
    {
        public ClassType classType;
        public AttackType attackType;
        public float Health;
        public float AttackDamage;
        public float SkillValue;
        [Range(0, 100)] public int Resistance;
        [Range(0, 100)] public int DefenceBonus;
    }
    [System.Serializable]
    public class HealerData
    {
        public ClassType classType;
        public AttackType attackType;
        public float Health;
        public float AttackDamage;
        public float SkillValue;
        [Range(0, 100)] public int Resistance;
        [Range(0, 100)] public int DefenceBonus;
    }
    [System.Serializable]
    public class EnemyMeleeData
    {
        public AttackType attackType;
        public float Health;
        public float AttackDamage;
        [Range(0, 100)] public int Resistance;
        [Range(0, 100)] public int DefenceBonus;
    }
    [System.Serializable]
    public class EnemyRangeData
    {
        public AttackType attackType;
        public float Health;
        public float AttackDamage;
        [Range(0, 100)] public int Resistance;
        [Range(0, 100)] public int DefenceBonus;
    }
    [System.Serializable]
    public class RandomEffectData
    {
        [Range(1.0f, 100f)] public float MeleeDamageBonusMultiplier;
        [Range(0.0f, 1.0f)] public float MeleeDamageReductionMultiplier;
        [Range(1.0f, 100f)] public float RangeDamageBonusMultiplier;
        [Range(0, 100)]     public int ArmorReductionValue;
        [Range(1.0f, 100f)] public float HealBonusMultiplier;
        [Range(0.0f, 1.0f)] public float HealReductionMultiplier;
    }

    public enum AttackType
    {
        Melee,
        Range
    }
    public enum ClassType
    {
        Tank,
        DamageDealer,
        Healer
    }

}
