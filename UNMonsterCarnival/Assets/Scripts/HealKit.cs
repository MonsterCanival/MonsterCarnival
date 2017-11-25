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
   
	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            GameObject.Find("Player").GetComponent<Player>().Heal(healHP);
            //take_HealKit = true;
            gameObject.SetActive(false);
        }
    }
	// Update is called once per frame
	void Update () {
     
	}
}
