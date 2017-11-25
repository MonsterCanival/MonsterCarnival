using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Battleable{

    List<GameObject> JungleTarget;
    List<GameObject> AttackMinionTarget;
    List<GameObject> PlayerTarget;
    List<GameObject> NeutralMinionTarget;

    GameObject MainTarget;

    void Awake()
    {
        HP = 300;
        Power = 20;

        DelayAttack = 2.0d;

        JungleTarget = new List<GameObject>();
        AttackMinionTarget = new List<GameObject>();
        PlayerTarget = new List<GameObject>();
        NeutralMinionTarget = new List<GameObject>();

        MainTarget = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(MainTarget == null)
        {
            SetMainTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Minion checkMinionBehave;
            if(collision.gameObject.GetComponent<JungleMonster>() != null)
            {
                JungleTarget.Add(collision.gameObject);
            }
            else if((checkMinionBehave = collision.gameObject.GetComponent<Minion>()) != null)
            {
                if(checkMinionBehave.Behave.Current == States.ATTACK || checkMinionBehave.Behave.Current == States.MOVE_ATTACK)
                {
                    AttackMinionTarget.Add(collision.gameObject);
                }
                else
                {
                    NeutralMinionTarget.Add(collision.gameObject);
                }
            }
        }
        else if(collision.tag == "Player")
        {
            PlayerTarget.Add(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(MainTarget != null) {
            Attack(MainTarget);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(MainTarget == collision.gameObject)
        {
            MainTarget = null;
        }
        if (collision.tag == "Enemy")
        {
            Minion checkMinionBehave;
            if (collision.gameObject.GetComponent<JungleMonster>() != null)
            {
                JungleTarget.Remove(collision.gameObject);
            }
            else if ((checkMinionBehave = collision.gameObject.GetComponent<Minion>()) != null)
            {
                if (checkMinionBehave.Behave.Current == States.ATTACK || checkMinionBehave.Behave.Current == States.MOVE_ATTACK)
                {
                    AttackMinionTarget.Remove(collision.gameObject);
                }
                else
                {
                    NeutralMinionTarget.Remove(collision.gameObject);
                }
            }
        }
        else if (collision.tag == "Player")
        {
            PlayerTarget.Remove(collision.gameObject);
        }

    }

    public void SetMainTarget()
    {
        if(JungleTarget.Count > 0)
        {
            MainTarget = JungleTarget[0];
            return;
        }
        else if(AttackMinionTarget.Count > 0)
        {
            MainTarget = AttackMinionTarget[0];
            return;
        }
        else if(PlayerTarget.Count > 0)
        {
            MainTarget = PlayerTarget[0];
        }
        else if(NeutralMinionTarget.Count > 0)
        {
            MainTarget = NeutralMinionTarget[0];
        }
        else
        {
            MainTarget = null;
        }
    }
}
