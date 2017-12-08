using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Battleable {

    int SingleSkillPower;
    int MultipleSkillPower;
    double SkillSpeed;

    public double DelaySingleSkill;
    public double DelayMultipleSkill;
    public double DelaySkillSpeed;

    bool bCanSingleSkill;
    bool bCanMultipleSkill;
    bool bCanSpeedSkill;

    List<GameObject> AttackableEnemies;
    GameObject MainTarget;


    // Use this for initialization
    protected new void Awake()
    {
        base.Awake();
        HP = 250;

        Power = 1000;
        SingleSkillPower = Power * 2;
        MultipleSkillPower = Power;

        Speed = 2.0d;
        SkillSpeed = 20.0d;

        DelayAttack = 0.5d;
        DelaySingleSkill = 2.0d;
        DelayMultipleSkill = 4.0d;
        DelaySkillSpeed = 6.0d;

        bCanSingleSkill = false;
        bCanMultipleSkill = false;
        bCanSpeedSkill = false;

        ConditionAnimator = GetComponent<Animator>();
        AttackableEnemies = new List<GameObject>();
        MainTarget = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Behaviable.bCanControl == true)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Move(h, v);

            SetMainTarget();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack(MainTarget);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                SingleSkillAttack(MainTarget);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                MultipleSkillAttack(AttackableEnemies);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine("SpeedSkill");
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AttackableEnemies.Add(collision.gameObject);
        }
        else if(collision.tag == "Enemy")
        {
            if(collision.GetComponent<Battleable>().Behaviable.bIsOwned == false)
            {
                AttackableEnemies.Add(collision.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (MainTarget == collision.gameObject)
        {
            MainTarget = null;
        }

        if (collision.tag == "Player")
        {
            AttackableEnemies.Remove(collision.gameObject);
        }
        else if (collision.tag == "Enemy")
        {
            if (collision.GetComponent<Battleable>().Behaviable.bIsOwned == false)
            {
                AttackableEnemies.Remove(collision.gameObject);
            }
        }
    }

    private void OnDisable()
    {
        Behaviable.bCanControl = false;
        Invoke("InvokeSetBCanControlTrue", 10.0f);
        Invoke("SetActiveTrue", 10.0f);
    }

    private void SetActiveTrue()
    {
        gameObject.SetActive(true);
    }

    public override void Attack(GameObject target)
    {
        if (Behaviable.bCanAttack == true)
        {
            Behaviable.bCanAttack = false;
            Invoke("InvokeSetBCanAttackTrue", (float)DelayAttack);
            if (target != null)
            {
                Damage(target, Power);
            }
            ConditionAnimator.SetInteger("Condition", 2);
        }
    }

    public void SingleSkillAttack(GameObject target)
    {
        if(Behaviable.bCanSingleSkill == true)
        {
            Behaviable.bCanSingleSkill = false;
            
            Invoke("InvokeSetBCanSingleSkillTrue", (float)DelaySingleSkill);
            if(target != null)
            {
                Damage(target, SingleSkillPower);
            }
            ConditionAnimator.SetInteger("Condition", 3);
        }
    }

    public void MultipleSkillAttack(List<GameObject> targets)
    {
        if (Behaviable.bCanMultipleSkill == true)
        {
            Behaviable.bCanMultipleSkill = false;
            Invoke("InvokeSetBCanMultipleSkillTrue", (float)DelayMultipleSkill);
            for(int i=0; i<targets.Count; ++i)
            {
                Damage(targets[i], MultipleSkillPower);
            } 
            ConditionAnimator.SetInteger("Condition", 4);
        }
    }

    public IEnumerator SpeedSkill()
    {
        if (Behaviable.bCanSpeedSkill == true)
        {
            for(int i=1; i<=3; ++i)
            {
                Behaviable.bCanSpeedSkill = false;
                Invoke("InvokeSetBCanSpeedSkillTrue", (float)DelaySkillSpeed);
                Vector3 DashDirection = new Vector3(Input.GetAxisRaw("Horizontal") * (float)SkillSpeed * Time.deltaTime,
                Input.GetAxisRaw("Vertical") * (float)SkillSpeed * Time.deltaTime, 0);

                transform.Translate(DashDirection);

                WaitForSeconds WFS = new WaitForSeconds(0.1f);
                yield return WFS;
            }
            ConditionAnimator.SetInteger("Condition", 5);
        }
    }

    public override void Damage(GameObject target, int damagePower)
    {
        target.GetComponent<Battleable>().Hit(damagePower);
    }

    public override void Hit(int damagePower)
    {
        if (HP - damagePower > 0)
        {
            HP -= damagePower;
            print(HP + "HP");
        }
        else
        {
            Die();
        }
    }

    public void Move(float h, float v)
    {
        Vector3 MoveDirection = new Vector3();
        MoveDirection.x = h * (float)Speed * Time.deltaTime;
        MoveDirection.y = v * (float)Speed * Time.deltaTime;
        transform.Translate(MoveDirection);
        if (Behaviable.bCanAttack == true)
        {
            if (MoveDirection.magnitude <= 0.001)
            {
                ConditionAnimator.SetInteger("Condition", 0);
            }
            else
            {
                ConditionAnimator.SetInteger("Condition", 1);
            }
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
