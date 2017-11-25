using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	public void StartButton () {
        Invoke("startgame", .5f);
	}
    void startgame()
    {
        SceneManager.LoadScene("Game");
    }
}
