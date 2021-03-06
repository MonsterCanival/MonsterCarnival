﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Battleable : MonoBehaviour {

    public int HP;
    public int Power;
    public double Speed;

    public double DelayAttack;

    protected Animator ConditionAnimator;

    public BehaviableState Behaviable;

    protected void Awake()
    {
        HP = 1;
        Power = 0;  
        Speed = 0;
        DelayAttack = double.PositiveInfinity;

        ConditionAnimator = gameObject.GetComponent<Animator>();
        ConditionAnimator.SetInteger("Condition", 0);


        Behaviable = new BehaviableState(1);
    }

    public abstract void Attack(GameObject target);

    public abstract void Damage(GameObject target, int damagePower);

    public abstract void Hit(int damagePower);

    public virtual void Die()
    {
        print(gameObject + "is Die");
        gameObject.SetActive(false);
            
    }



    public virtual void InvokeSetBCanControlTrue()
    {
        Behaviable.SetBCanControlTrue();
        ConditionAnimator.SetInteger("Condition", 0);
    }
    public virtual void InvokeSetBCanAttackTrue()
    {
        Behaviable.SetBCanAttackTrue();
        ConditionAnimator.SetInteger("Condition", 0);
    }
    public virtual void InvokeSetBCanSingleSkillTrue()
    {
        Behaviable.SetbCanSingleSkillTrue();
        ConditionAnimator.SetInteger("Condition", 0);
    }
    public virtual void InvokeSetBCanMultipleSkillTrue()
    {
        Behaviable.SetbCanMultipleSkillTrue();
        ConditionAnimator.SetInteger("Condition", 0);
    }
    public virtual void InvokeSetBCanSpeedSkillTrue()
    {
        Behaviable.SetbCanSpeedSkillTrue();
        ConditionAnimator.SetInteger("Condition", 0);
    }
}
