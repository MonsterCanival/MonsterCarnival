using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealKit : MonoBehaviour {

    int healHP = 30;
    float timeSpan;
    float checkTime;

	// Use this for initialization
	void Start () {
        timeSpan = 0;
        checkTime = 10;
	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            GameObject.Find("Player").GetComponent<Player>().Heal(healHP);
            gameObject.GetComponent<HealKit>().enabled = false;
            gameObject.SetActive(false);
        }
    }
	// Update is called once per frame
	void Update () {
        if (gameObject.GetComponent<HealKit>().enabled == false)
            timeSpan += Time.deltaTime;
        if (timeSpan > checkTime)
        {
            timeSpan = 0;
            gameObject.GetComponent<HealKit>().enabled = true;
            gameObject.SetActive(true);
        }
	}
}
