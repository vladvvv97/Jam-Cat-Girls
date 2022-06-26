using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject HP;
    [SerializeField] private Transform hpBar;
    [SerializeField] private GameObject hpChangeEffect;
    [SerializeField] private TextMeshPro hpChangeEffectTmp;

    protected SpriteRenderer sr;

    protected float Health;
    private float attackDamage;
    private int resistance;
    private int defenceBonus;
    private float skillValue;
    public float initialHealth;
    public float initialAttackDamage;
    public int initialResistance;
    public int initialDefenceBonus;
    public float initialSkillValue;

    private GameManager.AttackType attackType;
    private GameManager.ClassType classType;

    protected bool alreadyUsed = false;
    private bool isDead = false;
    private float healthNow;
    public bool AlreadyUsed { get => alreadyUsed; set => alreadyUsed = value; }
    public float HealthNow { get => healthNow; set { healthNow = value; if (healthNow <= 0) { Dead(); } if (healthNow > Health) { healthNow = Health; } } }

    public float AttackDamage { get => attackDamage; set => attackDamage = value; }
    public int Resistance { get => resistance; set => resistance = value; }
    public int DefenceBonus { get => defenceBonus; set => defenceBonus = value; }
    public GameManager.AttackType AttackType { get => attackType; set => attackType = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public float SkillValue { get => skillValue; set => skillValue = value; }
    public GameManager.ClassType ClassType { get => classType; set => classType = value; }

    virtual protected void Awake()
    {
        
    }

    virtual protected void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        hpBar.localScale = new Vector3(HealthNow / Health, 1, 1);

        EventsManager.OnHealthChanges.AddListener(UpdateHP);
    }

    protected void OnMouseUp()
    {
        EventsManager.InvokeOnChosenEvent(this);
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
        AlreadyUsed = true;
        IsDead = true;
        Debug.Log("Dead");
        sr.enabled = false;
        HP.SetActive(false);
    }

}
