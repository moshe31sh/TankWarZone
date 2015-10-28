using UnityEngine;
using System.Collections;

public class CS_multiPlayerTank1Controller : MonoBehaviour {
	private bool isMoving = false;
	public Vector3 moveLeft = new Vector3(-1, 0, 0);
	public Vector3 moveRight = new Vector3(1, 0, 0);
	public float speed = 0.1f; //play with it for faster movment

	static CS_multiPlayerTank1Controller _instance;


	public static CS_multiPlayerTank1Controller Instance // singletone
	{
		get {
			if (_instance == null) _instance = GameObject.Find("Tank_1").GetComponent < CS_multiPlayerTank1Controller > ();
			
			return _instance;
		}
	}
	
	
	void Update() {
		Movment();
	}
	
	
	void Movment() // player one movement control
	{
		if (CS_MultiPlayerAppWrap.currentPlayer == Define_Vars.Player.One && CS_MultiPlayerAppWrap.Instance.isMyTurn == true && CS_MultiPlayerAppWrap.freeze == false) {
			if (Input.GetKey (KeyCode.D) && isMoving == false) {
				Action (new Vector3 (transform.position.x + speed, transform.position.y, transform.position.z));
			} else if (Input.GetKey (KeyCode.A) && isMoving == false) {
				
				Action (new Vector3 (transform.position.x - speed, transform.position.y, transform.position.z));
			} else {
				Action (new Vector3 (transform.position.x, transform.position.y, transform.position.z));
			}
		}
	}

	public void Action(Vector3 Vec){//start move
		transform.position  = Vec;
		Define_Vars.tankPositionForJson = Vec;
	}

}
