using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; private set => _instance = value; }

    public TankData Tank;
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
        public float Health;
        public float AttackDamage;
        [Range(0, 100)] public int Resistance;
        [Range(0, 100)] public int DefenceBonus;
    }
    [System.Serializable]
    public class EnemyData
    {
        public float Health;
        public float AttackDamage;
        [Range(0, 100)] public int Resistance;
        [Range(0, 100)] public int DefenceBonus;
    }
}
