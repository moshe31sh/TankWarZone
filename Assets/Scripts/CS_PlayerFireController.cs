using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CS_PlayerFireController: MonoBehaviour {
	public int bulletSpeed = 700;
	public float bulletAngle = 0;
	public GameObject _bullet;
	public GameObject lunchPoint;

	
	
	
	
	// Update is called once per frame
	void Update() {
		Aim();
		Fire();
		
	}
	
	void Fire() {
		if (CS_GameLogic.Instance.freezePlayerTwo == false) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				CS_GameLogic.Instance.ChangePlayer();
				Debug.Log("Fire");
				_bullet = Instantiate(Resources.Load("Bullet2")) as GameObject; //create bullet
				_bullet.name = "Bullet";
				lunchPoint = GameObject.FindGameObjectWithTag("LunchPoint");
				Vector3 _v3 = lunchPoint.transform.position;
				_bullet.transform.position = new Vector3(_v3.x, _v3.y, _v3.z);
				_bullet.transform.GetComponent < Rigidbody > ().AddForce((transform.up * bulletSpeed));
				GameObject.Find("CannonSound").GetComponent<AudioSource>().Play();

				CS_GameLogic.Instance.FreezeControl(Define_Vars.Player.One, true);
				CS_GameLogic.Instance.FreezeControl(Define_Vars.Player.Two, true);
			}
		}
	}
	
	void Aim() {
		if (CS_GameLogic.Instance.freezePlayerTwo == false) {
			if (Input.GetKey(KeyCode.E)) {
				bulletSpeed += 200;
				if (bulletSpeed > 50000) bulletSpeed = 50000;
			}
			if (Input.GetKey(KeyCode.Q)) {
				bulletSpeed -= 200;
				if (bulletSpeed < 200) bulletSpeed = 200;
			}
			
			if (Input.GetKey(KeyCode.W)) {
				bulletAngle = Mathf.Clamp(bulletAngle + 1, -80, 80);
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -bulletAngle);
				
			}
			if (Input.GetKey(KeyCode.S)) {
				bulletAngle = Mathf.Clamp(bulletAngle - 1, -80, 80);
				transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -bulletAngle);
			}
			CS_GameLogic.Instance.InitStatusBar(bulletSpeed, bulletAngle);
		}
	}
	
	
}