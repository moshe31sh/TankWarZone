using UnityEngine;
using System.Collections;

public class CS_OpponentHit: MonoBehaviour { // collision detection

	void Update(){
		if (transform.position.x > 350 || transform.position.x < 135 ||transform.position.y < 100 || transform.position.y > 200) {
			Destroy (gameObject);
			GameObject.Find("ColisionSound").GetComponent<AudioSource>().Play();
			CS_GameLogic.Instance.FreezeControl(Define_Vars.Player.Two, false); // unfreeze other player

		}
	}

	void OnCollisionEnter(Collision c) {
		if (c.transform.gameObject.tag == "Surface" || c.transform.gameObject.tag == "Tank1" || c.transform.gameObject.tag == "ColliderManager") {
			GameObject.Find("HitGround").GetComponent<AudioSource>().Play();

			Destroy(gameObject);
		}else if (c.transform.gameObject.tag == "Tank2") {
			Destroy(gameObject);
			print("hit");
			CS_GameLogic.Instance.UpDateScore(Define_Vars.Player.Two); // update score if tank hit
			GameObject.Find("ColisionSound").GetComponent<AudioSource>().Play();
						
		}

		CS_GameLogic.Instance.FreezeControl(Define_Vars.Player.Two, false); // unfreeze other player
		
	}
}