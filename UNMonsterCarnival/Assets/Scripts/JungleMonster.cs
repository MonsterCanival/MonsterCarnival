using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleMonster : Battleable {

    public BehaveState Behave;
    GameObject MainTarget;

    protected new void Awake()
    {
        base.Awake();
        HP = 100;
        Power = 10;
        Speed = 0.5d;

        DelayAttack = 2.5d;
        Behave = new BehaveState(States.NEUTRAL);
        MainTarget = null;
        
    }

    private void Update()
    {
        if(Behaviable.bIsOwned == true)
        {

            if (Behave.Current == States.MOVE_ATTACK && MainTarget != null)
            {
                Move();
            }

            if (MainTarget == null)
            {
                SetMainTarget();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.GetType() == typeof(BoxCollider2D))
        {

            if (collision.tag =="Object")
            {
                Tower checkNullTower = collision.GetComponent<Tower>();
                if ((checkNullTower != null) && (checkNullTower.Behaviable.bIsOwned == false))
                {
                    Attack(collision.gameObject);
                    Behave.Current = States.ATTACK;
                }
                
            }
            else if(collision.tag == "Player")
            {
                if(Behaviable.bIsOwned == false)
                {
                    Attack(collision.gameObject);
                    Behave.Current = States.ATTACK;
                }
            }
        }
    }

    private void OnEnable()
    {
        HP = 50;
        Behave.Current = States.MOVE_ATTACK;
    }

    private void OnDisable()
    {
        if(Behaviable.bIsOwned == true)
        {
            Destroy(gameObject);
        }
        else
        {
            Behaviable.bIsOwned = true;
            gameObject.SetActive(false);
            Invoke("SetActiveTrue", 3.0f);        
        }
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
            Damage(target, Power);

            ConditionAnimator.SetInteger("Condition", 2);
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
        }
        else
        {
            Die();
        }
    }

    public void Move()
    {
        Vector3 moveVector = MainTarget.transform.position - transform.position;
        moveVector.x = moveVector.normalized.x * (float)Speed * Time.deltaTime;
        moveVector.y = moveVector.normalized.y * (float)Speed * Time.deltaTime;

        transform.Translate(moveVector);
    }

    public void SetMainTarget()
    {

        Tower[] targetList;
        targetList = FindObjectsOfType<Tower>();

        double minDistance = double.PositiveInfinity;
        double currentTargetDistance;
        for(int i=0; i < targetList.Length; ++i)
        {
            
            currentTargetDistance = Vector3.Distance(transform.position, targetList[i].transform.position);
            if(currentTargetDistance < minDistance)
            {
                minDistance = currentTargetDistance;
                MainTarget = targetList[i].gameObject;
            }
        }
    }
}
 
 