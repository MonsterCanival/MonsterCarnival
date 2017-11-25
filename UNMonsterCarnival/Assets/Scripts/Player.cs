using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Battleable {

    int SkillSinglePower;
    int SkillMultiplePower;
    double SkillSpeed;

    public double DelaySkillSingle;
    public double DelaySkillMultiple;
    public double DelaySkillSpeed;

    bool bCanSingleSkill;
    bool bCanMultipleSkill;
    bool bCanSpeedSkill;

    List<GameObject> AttackableEnemies;
    GameObject MainTarget;


    // Use this for initialization
    protected void Awake () {
        base.Awake();
        HP = 100;

        Power = 10;
        SkillSinglePower = Power * 2;
        SkillMultiplePower = Power;

        Speed = 2.0d;
        SkillSpeed = Speed * 2;

        DelayAttack = 0.5d;
        DelaySkillSingle = 2.0d;
        DelaySkillMultiple = 4.0d;
        DelaySkillSpeed = 6.0d;

        bCanSingleSkill = false;
        bCanMultipleSkill = false;
        bCanSpeedSkill = false;

        ConditionAnimator = GetComponent<Animator>();
        AttackableEnemies = new List<GameObject>();
        MainTarget = null;
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);

        SetMainTarget();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack(MainTarget);
        }
	}   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            AttackableEnemies.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            AttackableEnemies.Remove(collision.gameObject);
        }
    }

    public override void Attack(GameObject target)
    {
        print(gameObject + "calls this!");
        if (Behaviable.bCanAttack == true)
        {
            Behaviable.bCanAttack = false;
            Invoke("Behaviable.SetBCanAttackTrue", (float)DelayAttack);
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
        print("HIT - " + gameObject + " at " + damagePower);
        if (HP - damagePower > 0)
        {
            HP -= damagePower;
        }
        else
        {
            Die();
        }
    }

    void SkillSingleAttack(GameObject target)
    {
        Damage(target, SkillSinglePower);
    }

    void SkillMultipleAttack(GameObject[] targets)
    {
        for(int i = 0; i < targets.Length; i++)
        {
            Damage(targets[i], SkillMultiplePower);
        }
    }

    public void Move(float h, float v)
    {
        Vector3 MoveDirection = new Vector3();
        MoveDirection.x = h * (float)Speed * Time.deltaTime;
        MoveDirection.y = v * (float)Speed * Time.deltaTime;
        transform.Translate(MoveDirection);
        if(MoveDirection.magnitude <= 0.001)
        {
            ConditionAnimator.SetInteger("Condition", 0);
        }
        else
        {
            ConditionAnimator.SetInteger("Condition", 1);
        }
    }

    public void Heal(int HealAmount)
    {
        HP += HealAmount;
    }

    public void SetMainTarget()
    {
        ColliderDistance2D collDistance;
        double minDistance = Double.PositiveInfinity;
        for (int i = 0; i < AttackableEnemies.Count; i++)
        {
            collDistance = gameObject.GetComponent<BoxCollider2D>().Distance(AttackableEnemies[i].GetComponent<Collider2D>());
            if (minDistance > collDistance.distance)
            {
                MainTarget = AttackableEnemies[i];
                minDistance = collDistance.distance;
            }
        }
    }
}
