using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JungleCamp : MonoBehaviour {

    float timeSpan;
    float checkTime;
    bool empty;
	// Use this for initialization
	void Start () {
        timeSpan = 0;
        checkTime = 10;
        empty = false;
	}

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "JungleMonster")
        {
            empty = true;
        }
    }

    // Update is called once per frame
    void Update () {
        if (empty == true)
            timeSpan += Time.deltaTime;
        if (timeSpan > checkTime)
        {
            timeSpan = 0;
            Instantiate(GameObject.Find("JungleMonster"),
                new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
                Quaternion.identity);
            empty = false;
        }
	}
}
