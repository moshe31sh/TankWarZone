using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CS_Winner: MonoBehaviour {
	
	public Dictionary < string, GameObject > WinnerUiObjects;
	
	// Use this for initialization
	void Start() {
		Init();
		WinnerMenu();
	}
	
	
	
	
	void Init() { // Winner sence script
		WinnerUiObjects = new Dictionary < string, GameObject > ();
		GameObject[] objects = GameObject.FindGameObjectsWithTag("WinnerUIObjects");
		foreach(GameObject g in objects)
			WinnerUiObjects.Add(g.name, g);
	}
	
	void WinnerMenu() // show how is the winner
	{
		WinnerUiObjects["WinnerText"].GetComponent < Text > ().text = "The Winner is    Player " + Define_Vars.Winner.ToString();
		GameObject.Find("Winner").GetComponent<AudioSource>().Play();

		WinnerUiObjects["WinnerBackToMenuButton"].GetComponent < Button > ().onClick.AddListener(delegate {
			Application.LoadLevel("UI_Menu_sence");
		});
	}
}