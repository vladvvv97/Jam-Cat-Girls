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

    [SerializeField] private GameObject defenceBonusIcon;
    [SerializeField] private GameObject healIcon;
    [SerializeField] private GameObject attackBonusIcon;

    private Character _chosenCharacter;

    private AbilityType _chosenAbility;

    private bool AItrigger = true;

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
        EventsManager.OnEnemyTurn.AddListener(StartEnemiesAIcoroutine);

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

        if (_chosenCharacter)
        {
            if (_chosenCharacter.ClassType == GameManager.ClassType.Tank)
            {
                defenceBonusIcon.SetActive(true);
                healIcon.SetActive(false);
                attackBonusIcon.SetActive(false);
            }
            else if (_chosenCharacter.ClassType == GameManager.ClassType.Healer)
            {
                defenceBonusIcon.SetActive(false);
                healIcon.SetActive(true);
                attackBonusIcon.SetActive(false);
            }
            else if (_chosenCharacter.ClassType == GameManager.ClassType.DamageDealer)
            {
                defenceBonusIcon.SetActive(false);
                healIcon.SetActive(false);
                attackBonusIcon.SetActive(true);
            }
            else { return; }
        }


    }
    public void StartEnemiesAIcoroutine()
    {
        StartCoroutine(EnemiesAI());
    }
    public IEnumerator EnemiesAI()
    {     
        if (AItrigger)
        {
            AItrigger = false;
            yield return new WaitForSeconds(2.1f);
            foreach (var element in enemies)
            {
                if (element.IsDead == false)
                {
                    int rnd;
                    Character target;
                    do
                    {
                        rnd = UnityEngine.Random.Range(0, characters.Count);
                        target = characters[rnd];
                    }
                    while (characters.Exists(element => element.IsDead == false) && target.IsDead == true);

                    float damage = element.AttackDamage * ((100f - target.Resistance) / 100f);
                    target.HealthNow -= damage;
                    target.UpdateHP(damage);
                    element.AlreadyUsed = true;
                    element.ChangeColor(Color.grey);
                    yield return new WaitForSeconds(2.1f);
                }
                else { };
            }
            CheckTurnEnds();
            AItrigger = true;
            yield return null;
        }
              
    }
    void SetGlobalEffect(DiceRoll.DiceRollEffect effect)
    {
        switch (effect)
        {
            case DiceRoll.DiceRollEffect.Effect1: foreach (var element in characters) { if (element.AttackType == GameManager.AttackType.Melee) { element.AttackDamage *= GameManager.Instance.RandomEffects.MeleeDamageBonusMultiplier; } } foreach (var element in enemies) { if (element.AttackType == GameManager.AttackType.Melee) { element.AttackDamage *= GameManager.Instance.RandomEffects.MeleeDamageBonusMultiplier; } } break;
            case DiceRoll.DiceRollEffect.Effect2: foreach (var element in characters) { if (element.AttackType == GameManager.AttackType.Range) { element.AttackDamage *= GameManager.Instance.RandomEffects.RangeDamageBonusMultiplier; } } foreach (var element in enemies) { if (element.AttackType == GameManager.AttackType.Range) { element.AttackDamage *= GameManager.Instance.RandomEffects.RangeDamageBonusMultiplier; } } break;
            case DiceRoll.DiceRollEffect.Effect3: foreach (var element in characters) { if (element.AttackType == GameManager.AttackType.Melee) { element.AttackDamage *= GameManager.Instance.RandomEffects.MeleeDamageReductionMultiplier; } } foreach (var element in enemies) { if (element.AttackType == GameManager.AttackType.Melee) { element.AttackDamage *= GameManager.Instance.RandomEffects.MeleeDamageReductionMultiplier; } } break;
            case DiceRoll.DiceRollEffect.Effect4: foreach (var element in characters) { element.Resistance -= GameManager.Instance.RandomEffects.ArmorReductionValue; } foreach (var element in enemies) {element.Resistance -= GameManager.Instance.RandomEffects.ArmorReductionValue; }  break;
            case DiceRoll.DiceRollEffect.Effect5: foreach (var element in characters) { if (element.ClassType == GameManager.ClassType.Healer) { element.SkillValue *= GameManager.Instance.RandomEffects.HealBonusMultiplier; } } foreach (var element in enemies) { if (element.ClassType == GameManager.ClassType.Healer) { element.SkillValue *= GameManager.Instance.RandomEffects.HealBonusMultiplier; } } break;
            case DiceRoll.DiceRollEffect.Effect6: foreach (var element in characters) { if (element.ClassType == GameManager.ClassType.Healer) { element.SkillValue *= GameManager.Instance.RandomEffects.HealReductionMultiplier; } } foreach (var element in enemies) { if (element.ClassType == GameManager.ClassType.Healer) { element.SkillValue *= GameManager.Instance.RandomEffects.HealReductionMultiplier; } } break;
        }
    }
    void AttackEnemy(Enemy enemy)
    {
        if (_chosenCharacter && _chosenCharacter.AlreadyUsed == false)
        {
            if (_chosenAbility == AbilityType.Attack && _chosenCharacter.AttackType == GameManager.AttackType.Melee)
            {
                _chosenCharacter.AttackAnimation(enemy.transform.position.x);
                float damage = _chosenCharacter.AttackDamage * ((100f - enemy.Resistance) / 100f);
                enemy.HealthNow -= damage;
                enemy.UpdateHP(damage);               
                _chosenCharacter.AlreadyUsed = true;
                _chosenCharacter = null;
                
            }
            else if (_chosenAbility == AbilityType.Attack && _chosenCharacter.AttackType == GameManager.AttackType.Range)
            {
                _chosenCharacter.AttackAnimation();
                float damage = _chosenCharacter.AttackDamage * ((100f - enemy.Resistance) / 100f);
                enemy.HealthNow -= damage;
                enemy.UpdateHP(damage);
                _chosenCharacter.AlreadyUsed = true;          
                _chosenCharacter = null;
            }
            else if (_chosenAbility == AbilityType.Skill)
            {
                Debug.Log("Skill");
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
        
        if (characters.TrueForAll(element => element.AlreadyUsed == true) && enemies.TrueForAll(element => element.AlreadyUsed == true))
        {
            Debug.Log("Invoke");
            EventsManager.InvokeOnTurnEndsEvent();
        }
        else if (characters.TrueForAll(element => element.AlreadyUsed == true))
        {
            EventsManager.InvokeOnEnemyTurnEvent();
        }
        else
        { return; }
    }
    void CheckTurnEnds()
    {
        
        if (characters.TrueForAll(element => element.AlreadyUsed == true) && enemies.TrueForAll(element => element.AlreadyUsed == true))
        {
            Debug.Log("Invoke");
            EventsManager.InvokeOnTurnEndsEvent();
        }
        else if (characters.TrueForAll(element => element.AlreadyUsed == true))
        {
            EventsManager.InvokeOnEnemyTurnEvent();
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
        {   if (element.IsDead == false) element.AlreadyUsed = false;
            element.ChangeColor(Color.white);
            element.AttackDamage = element.initialAttackDamage;
            element.Resistance = element.initialResistance;
            element.DefenceBonus = element.initialDefenceBonus;
            element.SkillValue = element.initialSkillValue;

        } ) ;
        enemies.ForEach(element =>
        {
            if (element.IsDead == false) element.AlreadyUsed = false;
            element.ChangeColor(Color.white);
            element.AttackDamage = element.initialAttackDamage;
            element.Resistance = element.initialResistance;
            element.DefenceBonus = element.initialDefenceBonus;
            element.SkillValue = element.initialSkillValue;
        });

        diceRoll.StartCoroutine(diceRoll.Roll(_delay));
        SetGlobalEffect(diceRoll.Effect);
    }

    public void Reroll()
    {
        characters.ForEach(element =>
        {
            element.AttackDamage = element.initialAttackDamage;
            element.Resistance = element.initialResistance;
            element.DefenceBonus = element.initialDefenceBonus;
            element.SkillValue = element.initialSkillValue;

        });
        enemies.ForEach(element =>
        {
            element.AttackDamage = element.initialAttackDamage;
            element.Resistance = element.initialResistance;
            element.DefenceBonus = element.initialDefenceBonus;
            element.SkillValue = element.initialSkillValue;
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

                CheckTurnEnds();
            }
            else if (_chosenCharacter.ClassType == GameManager.ClassType.DamageDealer)
            {
                characters.ForEach(element => { element.AttackDamage += (int)_chosenCharacter.SkillValue; });

                _chosenCharacter.ChangeColor(Color.grey);
                _chosenCharacter.AlreadyUsed = true;
                _chosenCharacter = null;

                CheckTurnEnds();
            }
            else if (_chosenCharacter.ClassType == GameManager.ClassType.Healer)
            {
                characters.ForEach(element => { element.HealthNow += (int)_chosenCharacter.SkillValue; });

                characters.ForEach(element => { element.UpdateHPAfterHealing(_chosenCharacter.SkillValue); });


                _chosenCharacter.ChangeColor(Color.grey);
                _chosenCharacter.AlreadyUsed = true;
                _chosenCharacter = null;

                CheckTurnEnds();
            }
            else
            {

            }
        }
        else { }
    }
}
