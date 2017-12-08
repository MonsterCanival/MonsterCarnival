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
        checkTime = 15;
        empty = false;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            empty = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        JungleMonster checkJungle;
        if ((checkJungle = other.gameObject.GetComponent<JungleMonster>()) != null)
        {
            if(checkJungle.GetType() == typeof(JungleMonster))
            {
                empty = true;
            }
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
            Instantiate(GameObject.Find("JungleMonster"),
                new Vector2(gameObject.transform.position.x+Random.Range(-0.2f,0.2f), gameObject.transform.position.y+Random.Range(-0.25f,0.25f)),
                Quaternion.identity);
            empty = false;
        }
	}
}
