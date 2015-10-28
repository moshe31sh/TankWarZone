using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CS_PlayerControlers: MonoBehaviour {
	private bool isMoving = false;
	public Vector3 moveLeft = new Vector3(-1, 0, 0);
	public Vector3 moveRight = new Vector3(1, 0, 0);
	public float speed = 0.1f; //play with it for faster movment
	
	
	
	
	void Update() {
		Movment();
	}
	
	
	void Movment() // player one movement control
	{
		if (CS_GameLogic.Instance.freezePlayerTwo == false) {
			if (Input.GetKey(KeyCode.D) && isMoving == false) {
				transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
			} else if (Input.GetKey(KeyCode.A) && isMoving == false) {
				
				transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
			} else {
				transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			}
		}
	}
	
	
}