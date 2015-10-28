using UnityEngine;
using System.Collections;

public class CS_multiPlayerTank2FireController : MonoBehaviour {
	public int bulletSpeed = 700;
	public float bulletAngle = 0;
	public GameObject _bullet;
	public GameObject lunchPoint;



	static CS_multiPlayerTank2FireController _instance;
	
	
	public static CS_multiPlayerTank2FireController Instance{ // singletone
		get {
			if (_instance == null) _instance = GameObject.Find("TankTwoCylinder").GetComponent < CS_multiPlayerTank2FireController > ();
			
			return _instance;
		}
	}
	
	
	
	// Update is called once per frame
	void Update() {
		Aim();
		Fire();
	}
	
	void Fire() {
		if (CS_MultiPlayerAppWrap.currentPlayer == Define_Vars.Player.Two && CS_MultiPlayerAppWrap.Instance.isMyTurn == true&& CS_MultiPlayerAppWrap.freeze == false) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				Debug.Log ("Fire");
				FireAction (bulletSpeed);
				print ("from fire : "+Define_Vars.tankPositionForJson);
				//	CS_MultiPlayerAppWrap.Instance.PlayerLogic (bulletSpeed, bulletAngle, _bullet.transform.position, tankPositionForJson, aimPositionForJson);
				CS_MultiPlayerAppWrap.Instance.PlayerLogic (Define_Vars.bulletSpeedForJson , Define_Vars.bulletAngleForJson,Define_Vars.tankPositionForJson);
			}
		}
	}

	void Aim() {
		if (CS_MultiPlayerAppWrap.currentPlayer == Define_Vars.Player.Two && CS_MultiPlayerAppWrap.Instance.isMyTurn == true && CS_MultiPlayerAppWrap.freeze == false) {
			if (Input.GetKey (KeyCode.E)) {
				bulletSpeed += 200;
				if (bulletSpeed > 50000)
					bulletSpeed = 50000;
			}
			if (Input.GetKey (KeyCode.Q)) {
				bulletSpeed -= 200;
				if (bulletSpeed < 200)
					bulletSpeed = 200;
			}
		
			if (Input.GetKey (KeyCode.W)) {
				bulletAngle = Mathf.Clamp (bulletAngle + 1, -80, 80);
				AimAction (new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, -bulletAngle));
			}
			if (Input.GetKey (KeyCode.S)) {
				bulletAngle = Mathf.Clamp (bulletAngle - 1, -80, 80);
				AimAction (new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, -bulletAngle));
			}
			Define_Vars.bulletSpeedForJson = bulletSpeed;
			Define_Vars.bulletAngleForJson = bulletAngle;
		}
	}

	public void Aim(float bulletAngle) {
		AimAction (new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, -bulletAngle));
		Define_Vars.bulletSpeedForJson = bulletSpeed;
		Define_Vars.bulletAngleForJson = bulletAngle;
	}

	public void AimAction(Vector3 Vec){
		transform.localEulerAngles = Vec;
		//CS_multiPlayerTank1FireController.tankPositionForJson = Vec;
	}
	
	public void FireAction(int _bulletSpeed){
		_bullet = Instantiate(Resources.Load("Bullet4")) as GameObject; //create bullet
		_bullet.name = "Bullet";
		lunchPoint = GameObject.FindGameObjectWithTag("LunchPoint");
		Vector3 _v3 = lunchPoint.transform.position;
		_bullet.transform.position = new Vector3(_v3.x, _v3.y, _v3.z);
		_bullet.transform.GetComponent < Rigidbody > ().AddForce((transform.up * _bulletSpeed));
		GameObject.Find("CannonSound").GetComponent<AudioSource>().Play();
		CS_MultiPlayerAppWrap.freeze = true;
	}
}
