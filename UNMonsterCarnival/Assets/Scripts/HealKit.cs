using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealKit : MonoBehaviour {

    int healHP = 30;
    bool take_HealKit;
    float timeSpan;
    float checkTime;

	// Use this for initialization
	void Start () {
        take_HealKit = false;
        timeSpan = 0;
        checkTime = 10;
	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player"&&take_HealKit==false)
        {
            GameObject.Find("Player").GetComponent<Player>().Heal(healHP);
            take_HealKit = true;
            //안보이게하기
        }
    }
	// Update is called once per frame
	void Update () {
        if (take_HealKit == true)
        {
            timeSpan += Time.deltaTime;
        }
        if (timeSpan > checkTime)
        {
            timeSpan = 0;
            take_HealKit = false;
            //다시 보이게
        }
	}
}
