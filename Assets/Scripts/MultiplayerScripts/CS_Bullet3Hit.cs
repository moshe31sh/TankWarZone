using UnityEngine;
using System.Collections;

public class CS_Bullet3Hit : MonoBehaviour {

	void Update(){
		if (transform.position.x > 350 || transform.position.x < 135 ||transform.position.y < 100 || transform.position.y > 200) {
			Destroy (gameObject);
			GameObject.Find("ColisionSound").GetComponent<AudioSource>().Play();
			CS_MultiPlayerAppWrap.freeze = false;//stop player until its his turn
			
		}
	}



	void OnCollisionEnter(Collision c) {//check bullet collision with tank or surface
		if (c.transform.gameObject.tag == "Surface" || c.transform.gameObject.tag == "Tank1") {
			CS_MultiPlayerAppWrap.freeze = false;
			GameObject.Find("HitGround").GetComponent<AudioSource>().Play();

			Destroy(gameObject);
		}
		
		if (c.transform.gameObject.tag == "Tank2") {
			Destroy(gameObject);
			CS_MultiPlayerAppWrap.playerTwoLife--; 
			print("hit");
			GameObject.Find("ColisionSound").GetComponent<AudioSource>().Play();
			CS_MultiPlayerAppWrap.freeze = false;

		}

	}
}
