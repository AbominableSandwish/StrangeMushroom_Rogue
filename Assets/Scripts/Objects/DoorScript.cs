using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == (int)LAYERMASKS.PLAYER)
        {
            GameObject.Find("Scoring").GetComponent<ScoringManger>().Level_Up();
            SceneManager.LoadScene((int)Scene.STRANGE);
        }
    }
}
