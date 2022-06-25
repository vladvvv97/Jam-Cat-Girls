using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    public static readonly UnityEvent<Character> OnChosed = new UnityEvent<Character>();
    public static readonly UnityEvent<Enemy> OnAttackEnemy = new UnityEvent<Enemy>();
    public static readonly UnityEvent OnTurnEnds = new UnityEvent();

    public static void InvokeOnChosenEvent(Character character)
    {
        OnChosed?.Invoke(character);
    }

    public static void InvokeOnAttackEvent(Enemy enemy)
    {
        OnAttackEnemy?.Invoke(enemy);
    }
    public static void InvokeOnTurnEndsEvent()
    {
        OnTurnEnds?.Invoke();
    }
}
