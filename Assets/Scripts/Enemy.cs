using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject HP;
    [SerializeField] private Transform hpBar;
    [SerializeField] private GameObject hpChangeEffect;
    [SerializeField] private TextMeshPro hpChangeEffectTmp;

    protected SpriteRenderer sr;

    private float Health;
    private float attackDamage;
    private int resistance;
    private int defenceBonus;
    public float initialHealth;
    public float initialAttackDamage;
    public int initialResistance;
    public int initialDefenceBonus;
    public float initialSkillValue;

    private GameManager.AttackType attackType;
    private GameManager.ClassType classType;

    protected bool isDead = false;
    protected bool alreadyUsed = true;
    protected float healthNow;
    private float skillValue;
    public bool IsDead { get => isDead; set => isDead = value; }
    public bool AlreadyUsed { get => alreadyUsed; set => alreadyUsed = value; }
    public float HealthNow { get => healthNow; set { healthNow = value; if (healthNow <= 0) { Dead(); } } }
    public int Resistance { get => resistance; set => resistance = value; }
    public float AttackDamage { get => attackDamage; set => attackDamage = value; }
    public GameManager.AttackType AttackType { get => attackType; set => attackType = value; }
    public int DefenceBonus { get => defenceBonus; set => defenceBonus = value; }
    public float SkillValue { get => skillValue; set => skillValue = value; }
    public GameManager.ClassType ClassType { get => classType; set => classType = value; }

    virtual protected void Awake()
    {
        Health = GameManager.Instance.Enemy.Health;
        AttackDamage = GameManager.Instance.Enemy.AttackDamage;
        Resistance = GameManager.Instance.Enemy.Resistance;
        DefenceBonus = GameManager.Instance.Enemy.DefenceBonus;
        AttackType = GameManager.Instance.Enemy.attackType;
        initialHealth = Health;
        initialAttackDamage = AttackDamage;
        initialResistance = Resistance;
        initialDefenceBonus = DefenceBonus;
        initialSkillValue = SkillValue;
        HealthNow = Health;
    }

    virtual protected void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        hpBar.localScale = new Vector3(HealthNow / Health, 1, 1);
    }
    protected void OnMouseDown()
    {
        ChangeColor(Color.red);
    }
    protected void OnMouseUp()
    {
        EventsManager.InvokeOnAttackEvent(this);
        ChangeColor(Color.white);
    }

    public void ChangeColor(Color color)
    {
        sr.color = color;
    }

    public void UpdateHP(float dmg)
    {
        hpBar.localScale = new Vector3(HealthNow / Health, 1, 1);
        hpChangeEffectTmp.text = $"-{dmg}";
        StartCoroutine(ActivateHPChangeEffect());
    }

    IEnumerator ActivateHPChangeEffect()
    {
        hpChangeEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        hpChangeEffect.gameObject.SetActive(false);
        yield return null;
    }
    protected void Dead()
    {
        Debug.Log("Dead");
        sr.enabled = false;
        HP.SetActive(false);
        IsDead = true;
    }

}
