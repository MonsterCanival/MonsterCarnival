using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battleable : MonoBehaviour {

    public int HP;
    public int Power;
    public double Speed;

    public double DelayAttack;

    protected Animator ConditionAnimator;

    public BehaviableState Behaviable;

    private void Awake()
    {
        HP = 1;
        Power = 0;  
        Speed = 0;
        DelayAttack = double.PositiveInfinity;

        ConditionAnimator = gameObject.GetComponent<Animator>();
        ConditionAnimator.SetInteger("Condition", 0);


        Behaviable = new BehaviableState(1);
    }

    public void Attack(GameObject target)
    {   
        if(Behaviable.bCanAttack == true)
        {
            Behaviable.bCanAttack = false;
            Invoke("Behaviable.SetBCanAttackTrue", (float) DelayAttack);
            Damage(target, Power);

            ConditionAnimator.SetInteger("Condition", 2);
            
        }
    }

    public void Damage(GameObject target, int damagePower)
    {
        target.GetComponent<Battleable>().Hit(damagePower);
    }

    public void Hit(int damagePower)
    {
        print("HIT - " + gameObject + " at " + damagePower);
        if(HP - damagePower > 0)
        {
            HP -= damagePower;
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        Behaviable.bCanControl = false;
    }
}
