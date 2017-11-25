using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleMonster : Battleable {

    BehaveState Behave;
    GameObject MainTarget;

    private void Awake()
    {
        HP = 50;
        Power = 10;
        Speed = 0.2d;

        DelayAttack = 2.5d;
        Behave = new BehaveState(States.MOVE_ATTACK);
    }

    private void Update()
    {
        if(Behave.Current == States.MOVE_ATTACK)
        {
            Move();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Object")
        {
            Minion checkNullTower;
            if ((checkNullTower = collision.GetComponent<Minion>()) != null)
            {
                Attack(collision.gameObject);
            }
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
        Minion[] targetList;
        targetList = GetComponents<Minion>();

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
 
 