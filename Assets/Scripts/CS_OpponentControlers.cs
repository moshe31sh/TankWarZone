using UnityEngine;
using System.Collections;

public class CS_OpponentControlers : MonoBehaviour {

	private bool isMoving = false;
	public Vector3 moveLeft = new Vector3(-1,0,0);
	public Vector3 moveRight = new Vector3(1,0,0);	
	public float speed = 0.1f; //play with it for faster movment

	public GameObject lunchPoint;
	
	
	void Update ()
	{
		//Movment ();
		Aim ();
	}
	
	
	void Movment () // Opponent  movements
	{
		if (CS_GameLogic.Instance.freezePlayerOne == false) {
			Debug.Log ("Movment");
			if (Input.GetKey (KeyCode.D) && isMoving == false) {
				
				transform.position = new Vector3 (transform.position.x + speed, transform.position.y, transform.position.z);
			} else if (Input.GetKey (KeyCode.A) && isMoving == false) {
				
				transform.position = new Vector3 (transform.position.x - speed, transform.position.y, transform.position.z);
			} else {
				transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
			}
		}
	}

	void Aim ()//Aim cannon
	{
		int bulletSpeed = 5000;
		float bulletAngle = 0;

		GameObject _bullet;
			if (CS_GameLogic.Instance.freezePlayerOne == false) {
			bulletSpeed += Random.Range (1000, 20000); //  random speed
			bulletAngle+=Random.Range (-79, 0);//random angle
			bulletAngle = Mathf.Clamp(bulletAngle + 1,-80,0);
			CS_GameLogic.Instance.InitStatusBar (bulletSpeed , bulletAngle); // update score
			CS_GameLogic.Instance.ChangePlayer ();
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -bulletAngle);
			_bullet = Instantiate (Resources.Load ("Bullet1")) as GameObject; // create bullet
			_bullet.name = "Bullet";
			lunchPoint = GameObject.FindGameObjectWithTag("LunchPoint1");
			Vector3 _v3 = lunchPoint.transform.position; 
			_bullet.transform.position = new Vector3 (_v3.x, _v3.y, _v3.z);	
			_bullet.transform.GetComponent<Rigidbody> ().AddForce ((transform.up * bulletSpeed));
			GameObject.Find("CannonSound").GetComponent<AudioSource>().Play();

			CS_GameLogic.Instance.FreezeControl (Define_Vars.Player.One, true);
			CS_GameLogic.Instance.FreezeControl (Define_Vars.Player.Two, true);

		}
	}
}

