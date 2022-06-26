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
    public EnemyData Enemy;
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
    public class EnemyData
    {
        public AttackType attackType;
        public float Health;
        public float AttackDamage;
        [Range(0, 100)] public int Resistance;
        [Range(0, 100)] public int DefenceBonus;
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
