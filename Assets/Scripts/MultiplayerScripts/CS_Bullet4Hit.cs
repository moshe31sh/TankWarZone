using UnityEngine;
using System.Collections;

public class CS_Bullet4Hit : MonoBehaviour {

	void Update(){
		if (transform.position.x > 350 || transform.position.x < 135 ||transform.position.y < 100 || transform.position.y > 200) {
			Destroy (gameObject);
			GameObject.Find("ColisionSound").GetComponent<AudioSource>().Play();
			CS_MultiPlayerAppWrap.freeze = false;
			
		}
	}



	void OnCollisionEnter(Collision c) {
		if (c.transform.gameObject.tag == "Surface" || c.transform.gameObject.tag == "Tank2") {
			CS_MultiPlayerAppWrap.freeze = false;
			GameObject.Find("HitGround").GetComponent<AudioSource>().Play();
			Destroy(gameObject);
		}
		
		if (c.transform.gameObject.tag == "Tank1") {
			Destroy(gameObject);
			CS_MultiPlayerAppWrap.playerOneLife--;
			print("hit");
			GameObject.Find("ColisionSound").GetComponent<AudioSource>().Play();
			CS_MultiPlayerAppWrap.freeze = false;

		}
		

	}
}
