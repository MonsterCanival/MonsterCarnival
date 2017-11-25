using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealKit : MonoBehaviour {
    float pre_x;
    float pre_y;
    int healHP = 30;
    bool take_HealKit;
    float timeSpan;
    float checkTime;

	// Use this for initialization
	void Start () {
        timeSpan = 0;
        checkTime = 5;
        take_HealKit = false;
        pre_x = gameObject.transform.position.x;
        pre_y = gameObject.transform.position.y;
	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player"&&take_HealKit==false)
        {
            other.gameObject.GetComponent<Player>().Heal(healHP);
            take_HealKit = true;
            transform.position = new Vector2(300, 300);
            //gameObject.SetActive(false);
        }
    }
	// Update is called once per frame
	void Update () {
        if (take_HealKit)
            timeSpan += Time.deltaTime;
        if(timeSpan>checkTime)
        {
            timeSpan = 0;
            take_HealKit = false;
            transform.position = new Vector2(pre_x, pre_y);
        }
	}
}
