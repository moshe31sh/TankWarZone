using UnityEngine;
using System.Collections;

public class CS_multiPlayerTank2Controller : MonoBehaviour {
	private bool isMoving = false;
	public Vector3 moveLeft = new Vector3(-1, 0, 0);
	public Vector3 moveRight = new Vector3(1, 0, 0);
	public float speed = 0.1f; //play with it for faster movment
	
	static CS_multiPlayerTank2Controller _instance;
	
	
	public static CS_multiPlayerTank2Controller Instance // singletone
	{
		get {
			if (_instance == null) _instance = GameObject.Find("Tank_2").GetComponent < CS_multiPlayerTank2Controller > ();
			
			return _instance;
		}
	}
	
	
	void Update() {
		Movment();
	}
	
	void Movment() // player one movement control
	{
		if (CS_MultiPlayerAppWrap.currentPlayer == Define_Vars.Player.Two && CS_MultiPlayerAppWrap.Instance.isMyTurn == true && CS_MultiPlayerAppWrap.freeze == false) {
			if (Input.GetKey (KeyCode.D) && isMoving == false) {
				Action (new Vector3 (transform.position.x + speed, transform.position.y, transform.position.z));
			} else if (Input.GetKey (KeyCode.A) && isMoving == false) {
			
				Action (new Vector3 (transform.position.x - speed, transform.position.y, transform.position.z));
			} else {
				Action (new Vector3 (transform.position.x, transform.position.y, transform.position.z));
			}
		}
	}
	
	public void Action(Vector3 Vec){
		transform.position  = Vec;
		Define_Vars.tankPositionForJson = Vec;
	}

}
