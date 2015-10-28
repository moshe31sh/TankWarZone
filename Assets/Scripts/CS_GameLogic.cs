using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CS_GameLogic: MonoBehaviour {
	
	public static int playerOneLife = 0;
	public static int playerTwoLife = 0;
	private bool isGameOver;
	public Dictionary < string, GameObject > tankObjects;
	public Dictionary < string, GameObject > unityStatusBarObjects;
	
	public Define_Vars.Player currentPlayer = Define_Vars.Player.Init;
	
	public bool freezePlayerOne;
	
	public bool freezePlayerTwo;
	
	
	static CS_GameLogic _instance;
	
	
	
	public static CS_GameLogic Instance // singletone
	{
		get {
			if (_instance == null) _instance = GameObject.Find("CS_GameLogic").GetComponent < CS_GameLogic > ();
			
			return _instance;
		}
	}
	// Use this for initialization
	void Start() {
		StartGame();
	}
	
	

	
	
	public int PlayerOneLife {
		get {
			return playerOneLife;
		}
	}
	
	public int PlayerTwoLife {
		get {
			return playerTwoLife;
		}
	}
	
	public void StartGame() // start game
	{
		Debug.Log("Start Game");
		isGameOver = false;
		FreezeControl(Define_Vars.Player.One, true);
		FreezeControl(Define_Vars.Player.Two, true);
		playerOneLife = 3;
		playerTwoLife = 3;
		if (Random.Range(1, 2) == 2) {
			
			currentPlayer = Define_Vars.Player.One;
			FreezeControl(Define_Vars.Player.One, false);
		} else {
			currentPlayer = Define_Vars.Player.Two;
			FreezeControl(Define_Vars.Player.Two, false);
		}
		
	}
	
	
	
	public void UpDateScore(Define_Vars.Player player) // update game score
	{
		print("UpDateScore");
		if (Define_Vars.Player.Two == player) playerOneLife--;
		else playerTwoLife--;
		if (playerOneLife == 0 || playerTwoLife == 0) isGameOver = true;
		CheackIfGameOver();
	}
	
	
	public void CheackIfGameOver() {
		Debug.Log("CheackIfGameOver");
		if (playerOneLife == 0 || playerTwoLife == 0) {
			isGameOver = true;
		}
		
		if (isGameOver == true) {
			FreezeControl(Define_Vars.Player.One, true);
			FreezeControl(Define_Vars.Player.Two, true);
			WinnerMenu();
			
		}
	}
	
	public void ChangePlayer() {
		print("Change player");
		if (currentPlayer == Define_Vars.Player.One) {
			currentPlayer = Define_Vars.Player.Two;
		} else {
			currentPlayer = Define_Vars.Player.One;
		}
	}
	
	public void WinnerMenu() {
		Define_Vars.Winner = currentPlayer;
		Debug.Log("WinnerMenu");
		Application.LoadLevel("Winner_sence");

	}
	
	
	
	public void FreezeControl(Define_Vars.Player player, bool cond) { // stop other player
		print("FreezeControl");
		if (player == Define_Vars.Player.One && cond == true) freezePlayerOne = true;
		if (player == Define_Vars.Player.Two && cond == true) freezePlayerTwo = true;
		if (player == Define_Vars.Player.One && cond == false) freezePlayerOne = false;
		if (player == Define_Vars.Player.Two && cond == false) freezePlayerTwo = false;
	}
	
	
	public void InitStatusBar(float bulletSpeed, float bulletAngle) { // status var info
		unityStatusBarObjects = new Dictionary < string, GameObject > ();
		GameObject[] objects = GameObject.FindGameObjectsWithTag("StatusBarObject");
		foreach(GameObject g in objects)
			unityStatusBarObjects.Add(g.name, g);
		unityStatusBarObjects["Power"].GetComponent < Text > ().text = "Power: " + (bulletSpeed / 5000);
		unityStatusBarObjects["Engle"].GetComponent < Text > ().text = "Engle: " + (bulletAngle);
		unityStatusBarObjects["CurrentPlayer"].GetComponent < Text > ().text = "current player: " + CS_GameLogic.Instance.currentPlayer.ToString();
		unityStatusBarObjects["PlayerOneHealth"].GetComponent < Text > ().text = "Health: " + PlayerOneLife.ToString();
		unityStatusBarObjects["PlayerTwoHealth"].GetComponent < Text > ().text = "Health: " + PlayerTwoLife.ToString();
		
	}
	

}