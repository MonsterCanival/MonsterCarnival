﻿using System;
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

    Vector3 MoveDirection;
    CircleCollider2D AttackRadius;
    List<GameObject> AttackableEnemies;
    GameObject MainTargetEnemy;
    

	// Use this for initialization
	void Awake () {
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

        MoveDirection.x = 0;
        MoveDirection.y = 0;
        MoveDirection.z = 0;

        AttackRadius = this.GetComponent<CircleCollider2D>();
        AttackRadius.radius = 0.5f;
        AttackableEnemies = null;
        MainTargetEnemy = null;
	}
	
	// Update is called once per frame
	void Update () {
        print(HP);
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);

	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Enemy")
            AttackableEnemies.Add(collision.gameObject);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
            AttackableEnemies.Remove(collision.gameObject);
    }

    void GeneralAttack(GameObject target)
    {
        Damage(target, Power);
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
        MoveDirection.x = h * (float)Speed * Time.deltaTime;
        MoveDirection.y = v * (float)Speed * Time.deltaTime;
        transform.Translate(MoveDirection);
    }

    public void Heal(int HealAmount)
    {
        HP += HealAmount;
    }

    //public void SetMainTarget()
    //{
    //    ColliderDistance2D collDistance;
    //    double MinDistance = Double.PositiveInfinity;
    //    for (int i = 0; i < AttackableEnemies.Count; i++)
    //    {
    //        collDistance = gameObject.GetComponent<BoxCollider2D>().Distance(AttackableEnemies[i].GetComponent<Collider2D>());
    //        if(MinDistance > collDistance.distance)
    //        {
    //            MainTargetEnemy = AttackableEnemies[i];
    //            MinDistance = collDistance.distance;
    //        }
    //    }
    //}    
}
