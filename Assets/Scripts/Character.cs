using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform hpBar;

    protected SpriteRenderer sr;

    protected float Health;
    private float attackDamage;
    private int resistance;
    private int defenceBonus;

    protected bool alreadyUsed = false;
    private float healthNow;
    public bool AlreadyUsed { get => alreadyUsed; set => alreadyUsed = value; }
    public float HealthNow { get => healthNow; set { healthNow = value; if (healthNow <= 0) { Dead(); } } }

    public float AttackDamage { get => attackDamage; set => attackDamage = value; }
    public int Resistance { get => resistance; set => resistance = value; }
    public int DefenceBonus { get => defenceBonus; set => defenceBonus = value; }

    virtual protected void Awake()
    {
        
    }

    virtual protected void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        hpBar.localScale = new Vector3(HealthNow / Health, 1, 1);
    }

    protected void OnMouseUp()
    {
        EventsManager.InvokeOnChosenEvent(this);
    }

    public void ChangeColor(Color color)
    {
        sr.color = color;
    }

    protected void UpdateHP()
    {
        hpBar.localScale = new Vector3(HealthNow / Health, 1, 1);
    }
    protected void Dead()
    {
        throw new NotImplementedException();
    }
}
