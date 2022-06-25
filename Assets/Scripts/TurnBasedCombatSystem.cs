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

    [Range(0.0f, 5.0f)] private float _delay = 2f;
    
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

    void Update()
    {
        
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
                _chosenCharacter.ChangeColor(Color.grey);
                _chosenCharacter.AlreadyUsed = true;
                _chosenCharacter = null;
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
            element.AttackDamage = GameManager.Instance.Tank.AttackDamage;
            element.Resistance = GameManager.Instance.Tank.Resistance;
            element.DefenceBonus = GameManager.Instance.Tank.DefenceBonus;

        } ) ;
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
        }
    }
    void SetSkillAbilityType()
    {
        _chosenAbility = AbilityType.Skill;
    }
}
