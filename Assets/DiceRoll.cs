using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    public enum DiceRollEffect
    {
        Effect1,
        Effect2,
        Effect3,
        Effect4,
        Effect5,
        Effect6
    }
    public Animator animator;
    public AnimationClip animationClip1;
    public AnimationClip animationClip2;
    public AnimationClip animationClip3;
    public AnimationClip animationClip4;
    public AnimationClip animationClip5;
    public AnimationClip animationClip6;
    public DiceRollEffect Effect;
    private Image image;
    void Awake()
    {
        image = this.GetComponent<Image>();
        image.color = new Color(0, 0, 0, 0.5f);
    }

    public IEnumerator Roll(float _delay)
    {
        image.enabled = true;
        var rnd = Random.Range(0, 6);
        switch (rnd)
        {
            case 0:
                Effect = DiceRollEffect.Effect1;
                animator.Play(animationClip1.name);
                break;
            case 1:
                Effect = DiceRollEffect.Effect2;
                animator.Play(animationClip2.name);
                break;
            case 2:
                Effect = DiceRollEffect.Effect3;
                animator.Play(animationClip3.name);
                break;
            case 3:
                Effect = DiceRollEffect.Effect4;
                animator.Play(animationClip4.name);
                break;
            case 4:
                Effect = DiceRollEffect.Effect5;
                animator.Play(animationClip5.name);
                break;
            case 5:
                Effect = DiceRollEffect.Effect6;
                animator.Play(animationClip6.name);
                break;
        }
        yield return new WaitForSeconds(_delay);
        image.enabled = false;
        yield return null;
    }
}
