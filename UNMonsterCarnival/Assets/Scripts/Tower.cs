using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    noHost,user1,user2
}

public class Tower : Battleable{
    public int towerHost;
    // Use this for initialization
    void Start()
    {
        //towerHost = noHost;
        HP = 300;
        Power = 20;
        DelayAttack = 2.0d;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
