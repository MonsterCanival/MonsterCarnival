using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Battleable {

    Vector3 RandomDirection;
    
    private void Awake()
    {
        HP = 20;
        Power = 5;
        Speed = 0.5d;

        DelayAttack = 1.0d;

        RandomDirection.x = 0;
        RandomDirection.y = 0;
        RandomDirection.z = 0;
    }

    private void Start()
    {
        StartCoroutine("Move");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            RandomizeVector();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Object")
        {
            Attack(collision.gameObject);
        }
    }

    private void RandomizeVector()
    {
        RandomDirection.x = Random.Range(-1, 1);
        RandomDirection.y = Random.Range(-1, 1);
        RandomDirection.z = Random.Range(-1, 1);

        RandomDirection.Normalize();
    }

    IEnumerator Move()
    {
        WaitForSeconds WFS = new WaitForSeconds(5.0f);
        while (true)
        {
            transform.Translate(RandomDirection);
            yield return WFS;
        }
    }

    public void Attack(GameObject target)
    {
        Damage(target, Power);
    }
    
}
