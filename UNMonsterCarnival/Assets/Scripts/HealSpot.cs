using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpot : MonoBehaviour {

    float timeSpan;
    float checkTime;
    bool empty;

	// Use this for initialization
	void Start () {
        timeSpan = 0;
        checkTime = 10;
        empty = false;
	}
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            empty = true;
        }
    }

	// Update is called once per frame
	void Update () {
        //print(timeSpan);
            timeSpan += Time.deltaTime;
        if(timeSpan>checkTime)
        {
            timeSpan = 0;
            Instantiate(GameObject.Find("HealKit"), new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
            empty = false;
        }
	}
}
