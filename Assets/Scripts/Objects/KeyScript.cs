using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour {

    BoardCreator board_creator;

	// Use this for initialization
	void Start () {
        board_creator =  GameObject.Find("GameManager").GetComponent<BoardCreator>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == (int)LAYERMASKS.PLAYER)
        {
            board_creator.createDoor();
            Destroy(this.gameObject);
        }
    }
}
