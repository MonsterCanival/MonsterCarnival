using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Battleable {

    Vector3 RandomDirection;
    public BehaveState Behave;

    private void Awake()
    {
        base.Awake();
        HP = 20;
        Power = 3;
        Speed = 0.5d;

        DelayAttack = 1.0d;

        RandomDirection.x = 0;
        RandomDirection.y = 0;
        RandomDirection.z = 0;
        Behave = new BehaveState(States.NEUTRAL);

    }

    private void Start()
    {
        StartCoroutine("RandomizeVector");
    }

    private void Update()
    {
        if(Behave.Current == States.NEUTRAL)
        {
            Move();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            RandomizeVector();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Object" && Behave.Current == States.MOVE_ATTACK)
        {
            Behave.Current = States.ATTACK;
            Attack(collision.gameObject);
        }
    }

    IEnumerator RandomizeVector()
    {
        WaitForSeconds WFS = new WaitForSeconds(2.0f);
        while (true)
        {
            if(Behave.Current == States.NEUTRAL)
            {
                RandomDirection.x = Random.Range(-1.0f, 1.0f);
                RandomDirection.y = Random.Range(-1.0f, 1.0f);
            }
            else if(Behave.Current == States.IDLE)
            {
                RandomDirection.x = 0;
                RandomDirection.y = 0;
            }
            yield return WFS;
        }
    }

    private void OnEnable()
    {
        transform.position = new Vector3(Random.Range(-8.5f, 8.5f), Random.Range(-5.0f, 5.0f), 0);
        Awake();
    }

    public override void Attack(GameObject target)
    {
        if (Behaviable.bCanAttack == true)
        {
            Behaviable.bCanAttack = false;
            Invoke("InvokeSetBCanAttackTrue", (float)DelayAttack);
            Damage(target, Power);

            ConditionAnimator.SetInteger("Condition", 2);
        }
    }

    public override void Damage(GameObject target, int damagePower)
    {
        print(target);
        print(damagePower);
        target.GetComponent<Battleable>().Hit(damagePower);
    }

    public override void Hit(int damagePower)
    {
        if (HP - damagePower > 0)
        {
            HP -= damagePower;
        }
        else
        {
            Die();
        }
    }
    public override void Die()
    {
        GameObject poolObject = GameObject.FindWithTag("Respawn");
        if (poolObject != null)
        {
            gameObject.transform.SetParent(poolObject.transform);
            poolObject.GetComponent<MinionPool>().PushPool(gameObject);
        }
    }

    public void Move()
    {
        Vector3 moveDirection = new Vector3(0,0,0);

        moveDirection.x = RandomDirection.x * (float)Speed * Time.deltaTime;
        moveDirection.y = RandomDirection.y * (float)Speed * Time.deltaTime;
        transform.Translate(moveDirection);
    }
    
}
