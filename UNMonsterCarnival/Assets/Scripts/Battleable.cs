using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battleable : MonoBehaviour {

    public int HP;
    public int Power;
    public double Speed;

    public double DelayAttack;

    private void Awake()
    {
        HP = 1;
    }
    public void Damage(GameObject target, int damagePower)
    {
        target.GetComponent<Battleable>().Hit(damagePower);
    }

    public void Hit(int damagePower)
    {
        if(HP > 0)
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
