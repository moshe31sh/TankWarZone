using UnityEngine;
using System.Collections;

public class CS_PlayerHit: MonoBehaviour { // player collision detection


	void Update(){
		if (transform.position.x > 350 || transform.position.x < 135 ||transform.position.y < 100 || transform.position.y > 200) {
			Destroy (gameObject);
			GameObject.Find("ColisionSound").GetComponent<AudioSource>().Play();
			CS_GameLogic.Instance.FreezeControl(Define_Vars.Player.One, false); // unfreeze other player
			
		}
	}


	void OnCollisionEnter(Collision c) {
		if (c.transform.gameObject.tag == "Surface" || c.transform.gameObject.tag == "Tank2" || c.transform.gameObject.tag == "ColliderManager" ) {
			Destroy(gameObject);
			//CS_GameLogic.Instance.FreezeControl(Define_Vars.Player.One, false);
			GameObject.Find("HitGround").GetComponent<AudioSource>().Play();
			}
		else if (c.transform.gameObject.tag == "Tank1") {
			GameObject.Find("ColisionSound").GetComponent<AudioSource>().Play();

			print("hit");
			Destroy(gameObject);
			
			CS_GameLogic.Instance.UpDateScore(Define_Vars.Player.One);

		}

		CS_GameLogic.Instance.FreezeControl(Define_Vars.Player.One, false);
		}
}