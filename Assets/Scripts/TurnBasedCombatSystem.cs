using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnBasedCombatSystem : MonoBehaviour
{
    [SerializeField] private List<Character> characters = new List<Character>(3);
    [SerializeField] private List<Enemy> enemies = new List<Enemy>(3);
    [SerializeField] private TextMeshProUGUI turnTmp;

    [SerializeField] private Button attackButton;
    [SerializeField] private Button skillButton;
    [SerializeField] private Button defendButton;

    [SerializeField] private DiceRoll diceRoll;

    [SerializeField] [Range(0.0f, 5.0f)] private float _delay = 2f;

    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lose;

    private Character _chosenCharacter;

    private AbilityType _chosenAbility;

    private int _turn = 0;
    public int Turn { get => _turn; set => _turn = value; }

    public enum AbilityType
    {
        Attack,
        Defend,
        Skill
    }
    void Awake()
    {
        EventsManager.OnChosed.AddListener(ChooseCharacter);
        EventsManager.OnAttackEnemy.AddListener(AttackEnemy);
        EventsManager.OnAttackEnemy.AddListener(CheckTurnEnds);
        EventsManager.OnTurnEnds.AddListener(InvokeNewTurn);

        attackButton.onClick.AddListener(SetAttackAbilityType);
        defendButton.onClick.AddListener(SetDefendAbilityType);
        skillButton.onClick.AddListener(SetSkillAbilityType);

    }
    void Start()
    {
        diceRoll.StartCoroutine(diceRoll.Roll(_delay));
        SetGlobalEffect(diceRoll.Effect);
    }
    void Update()
    {
        if (enemies.TrueForAll(element => element.IsDead == true)) win.gameObject.SetActive(true);
        if (characters.TrueForAll(element => element.IsDead == true)) lose.gameObject.SetActive(true);
    }
    void SetGlobalEffect(DiceRoll.DiceRollEffect effect)
    {
        switch (effect)
        {
            case DiceRoll.DiceRollEffect.Effect1: foreach (var element in characters) { if (element.AttackType == GameManager.AttackType.Melee) { element.AttackDamage *= 2; } } foreach (var element in enemies) { if (element.AttackType == GameManager.AttackType.Melee) { element.AttackDamage *= 2; } } break;
            case DiceRoll.DiceRollEffect.Effect2: foreach (var element in characters) { if (element.AttackType == GameManager.AttackType.Melee) { element.AttackDamage *= 2; } } foreach (var element in enemies) { if (element.AttackType == GameManager.AttackType.Melee) { element.AttackDamage *= 2; } } break;
            case DiceRoll.DiceRollEffect.Effect3: foreach (var element in characters) { if (element.AttackType == GameManager.AttackType.Melee) { element.AttackDamage *= 2; } } foreach (var element in enemies) { if (element.AttackType == GameManager.AttackType.Melee) { element.AttackDamage *= 2; } } break;
            case DiceRoll.DiceRollEffect.Effect4: foreach (var element in characters) { if (element.AttackType == GameManager.AttackType.Melee) { element.AttackDamage *= 2; } } foreach (var element in enemies) { if (element.AttackType == GameManager.AttackType.Melee) { element.AttackDamage *= 2; } } break;
            case DiceRoll.DiceRollEffect.Effect5: foreach (var element in characters) { if (element.AttackType == GameManager.AttackType.Melee) { element.AttackDamage *= 2; } } foreach (var element in enemies) { if (element.AttackType == GameManager.AttackType.Melee) { element.AttackDamage *= 2; } } break;
            case DiceRoll.DiceRollEffect.Effect6: foreach (var element in characters) { if (element.AttackType == GameManager.AttackType.Melee) { element.AttackDamage *= 2; } } foreach (var element in enemies) { if (element.AttackType == GameManager.AttackType.Melee) { element.AttackDamage *= 2; } } break;
        }
    }
    void AttackEnemy(Enemy enemy)
    {
        if (_chosenCharacter && _chosenCharacter.AlreadyUsed == false)
        {
            if (_chosenAbility == AbilityType.Attack)
            {
                float damage = _chosenCharacter.AttackDamage * ((100f - enemy.Resistance) / 100f);
                enemy.HealthNow -= damage;
                enemy.UpdateHP(damage);

                _chosenCharacter.ChangeColor(Color.grey);
                _chosenCharacter.AlreadyUsed = true;
                _chosenCharacter = null;
            }
            else if (_chosenAbility == AbilityType.Skill)
            {

                Debug.Log("Skill");
                //_chosenCharacter.ChangeColor(Color.grey);
                //_chosenCharacter.AlreadyUsed = true;
               // _chosenCharacter = null;
            }
            else{ return; }           
        }
    }

    void ChooseCharacter(Character character)
    {
        if (!character.AlreadyUsed)
        {
            if (_chosenCharacter) _chosenCharacter.ChangeColor(Color.white);
            character.ChangeColor(Color.green);
            _chosenCharacter = character;
        }
        else return;
    }
    void SetGameOverScreen()
    {

    }
    void CheckTurnEnds(Enemy enemy)
    {
        if (characters.Count == 0 || enemies.Count == 0)
        { return; }
        else if (characters.TrueForAll(element => element.AlreadyUsed == true) && enemies.TrueForAll(element => element.AlreadyUsed == true))
        {
            Debug.Log("Invoke");
            EventsManager.InvokeOnTurnEndsEvent();
        }
        else
        { return; }
    }
    void CheckTurnEnds()
    {
        if (characters.Count == 0 || enemies.Count == 0) // ����������
        {
            return;
        }
        else if (characters.TrueForAll(element => element.AlreadyUsed == true) && enemies.TrueForAll(element => element.AlreadyUsed == true))
        {
            Debug.Log("Invoke");
            EventsManager.InvokeOnTurnEndsEvent();
        }
        else
        { return; }
    }

    void InvokeNewTurn()
    {
        Invoke(nameof(SetTurn), _delay);
        Invoke(nameof(UpdateTurn), _delay + 0.1f);
    }
    void SetTurn()
    {
        Turn++;
        characters.ForEach(element => 
        {   element.AlreadyUsed = false;
            element.ChangeColor(Color.white);
            element.AttackDamage = element.initialAttackDamage;
            element.Resistance = element.initialResistance;
            element.DefenceBonus = element.initialDefenceBonus;

        } ) ;
        enemies.ForEach(element =>
        {
            element.AlreadyUsed = true; // �������� ����� ����� ���������� AI
            element.ChangeColor(Color.white);
            element.AttackDamage = element.initialAttackDamage;
            element.Resistance = element.initialResistance;
            element.DefenceBonus = element.initialDefenceBonus;

        });

        diceRoll.StartCoroutine(diceRoll.Roll(_delay));
        SetGlobalEffect(diceRoll.Effect);
    }
    void UpdateTurn()
    {       
        turnTmp.text = Turn.ToString();
    }

    void SetAttackAbilityType()
    {
        _chosenAbility = AbilityType.Attack;
    }
    void SetDefendAbilityType()
    {
        _chosenAbility = AbilityType.Defend;
        if (_chosenCharacter)
        {
            _chosenCharacter.Resistance += _chosenCharacter.DefenceBonus;
            _chosenCharacter.ChangeColor(Color.grey);
            _chosenCharacter.AlreadyUsed = true;
            _chosenCharacter = null;
            CheckTurnEnds();
        }
        
    }
    void SetSkillAbilityType()
    {
        _chosenAbility = AbilityType.Skill;
    }

    public void UseSkill()
    {
        if (_chosenCharacter)
        {
            if (_chosenCharacter.ClassType == GameManager.ClassType.Tank)
            {
                characters.ForEach(element => { element.Resistance += (int)_chosenCharacter.SkillValue; });

                _chosenCharacter.ChangeColor(Color.grey);
                _chosenCharacter.AlreadyUsed = true;
                _chosenCharacter = null;
            }
            else if (_chosenCharacter.ClassType == GameManager.ClassType.DamageDealer)
            {
                characters.ForEach(element => { element.AttackDamage += (int)_chosenCharacter.SkillValue; });

                _chosenCharacter.ChangeColor(Color.grey);
                _chosenCharacter.AlreadyUsed = true;
                _chosenCharacter = null;
            }
            else if (_chosenCharacter.ClassType == GameManager.ClassType.Healer)
            {
                characters.ForEach(element => { element.HealthNow += (int)_chosenCharacter.SkillValue; });

                EventsManager.InvokeOnHealthChangesEvent();

                _chosenCharacter.ChangeColor(Color.grey);
                _chosenCharacter.AlreadyUsed = true;
                _chosenCharacter = null;
            }
            else
            {

            }
        }
        else { }
    }
}
