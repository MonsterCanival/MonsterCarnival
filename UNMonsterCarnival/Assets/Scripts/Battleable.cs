using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Battleable : MonoBehaviour {

    public int HP;
    public int Power;
    public double Speed;

    public double DelayAttack;

    BehaviableState Behaviable;

    private void Awake()
    {
        HP = 1;
        Power = 0;  
        Speed = 0;

        DelayAttack = double.PositiveInfinity;

        Behaviable = new BehaviableState(1);
    }

    public void Attack(GameObject target)
    {   
        if(Behaviable.bCanAttack == true)
        {
            Damage(target, Power);
        }
    }

    public void Damage(GameObject target, int damagePower)
    {
        target.GetComponent<Battleable>().Hit(damagePower);
    }

    public void Hit(int damagePower)
    {
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
        gameObject.SetActive(false);
    }
}
