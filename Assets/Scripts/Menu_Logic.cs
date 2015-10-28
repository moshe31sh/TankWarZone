using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Menu_Logic: MonoBehaviour {
	
	public Dictionary < string, GameObject > unityUIObjects;
	
	// Use this for initialization
	void Start() {
		Init();
		MainMenu();
	}
	
	
	void Init() {
		unityUIObjects = new Dictionary < string, GameObject > ();
		GameObject[] objects = GameObject.FindGameObjectsWithTag("UnityUIObjects");
		foreach(GameObject g in objects)
			unityUIObjects.Add(g.name, g);
		GameObject.Find("MenuSound").GetComponent<AudioSource>().Play();

		
	}
	
	
	
	
	void MainMenu() //call the main menu
	{
		
		Debug.Log("MainMenu is on");
		MenuMod("MainMenuBoard", Define_Vars.ON);
		for (int i = 0; i < Define_Vars.menus.Length; i++) {
			if (Define_Vars.menus[i] != "MainMenuBoard") MenuMod(Define_Vars.menus[i], Define_Vars.OFF);
		}
		unityUIObjects["MainMenuMultiPlayerButton"].GetComponent < Button > ().onClick.AddListener(delegate {
			MultiPlayerMenu();
		});
		unityUIObjects["MainMenuSinglePlayerButton"].GetComponent < Button > ().onClick.AddListener(delegate {
			Loading();
		});
		unityUIObjects["MainMenuStudentInfoButton"].GetComponent < Button > ().onClick.AddListener(delegate {
			StudentInfo();
		});
		unityUIObjects["MainMenuOptionsButton"].GetComponent < Button > ().onClick.AddListener(delegate {
			Options();
		});
		
		
	}
	
	void MultiPlayerMenu() //call multiplayer game
	{
		Debug.Log("MultiPlayer is on");
		MenuMod("MultiPlayerBoard", Define_Vars.ON);
		for (int i = 0; i < Define_Vars.menus.Length; i++) {
			if (Define_Vars.menus[i] != "MultiPlayerBoard") MenuMod(Define_Vars.menus[i], Define_Vars.OFF);
		}
		unityUIObjects["MultiPlayerBackButton"].GetComponent < Button > ().onClick.AddListener(delegate {
			MainMenu();
		});
		unityUIObjects["MultiPlayerOptionsButton"].GetComponent < Button > ().onClick.AddListener(delegate {
			Options();
		});
		unityUIObjects["MultiPlayerPlayButton"].GetComponent < Button > ().onClick.AddListener(delegate {
			LoadMultiPlayer();
		});
		Define_Vars.cash = unityUIObjects["MultiPlayerSlider"].GetComponent < Slider > ();
		Define_Vars.cash.onValueChanged.AddListener(delegate {
			PrintSlider("MultiPlayerCashText", Define_Vars.cash.value);
		});
	}
	
	
	
	void StudentInfo() //call to student information screen
	{
		Debug.Log("StudentInfo is on");
		MenuMod("StudentInfoBoard", Define_Vars.ON);
		for (int i = 0; i < Define_Vars.menus.Length; i++) {
			if (Define_Vars.menus[i] != "StudentInfoBoard") MenuMod(Define_Vars.menus[i], Define_Vars.OFF);
		}
		unityUIObjects["StudentInfoBackButton"].GetComponent < Button > ().onClick.AddListener(delegate {
			MainMenu();
		});
		unityUIObjects["LinkToSiteButton"].GetComponent < Button > ().onClick.AddListener(delegate {
			OpenWebSite("https://github.com/moshe31sh/TankWarZone.git");
		});
		unityUIObjects["StudentInfoOptionsButton"].GetComponent < Button > ().onClick.AddListener(delegate {
			Options();
		});
	}
	
	void Options() //call to game option
	{
		Debug.Log("OptionsBoard is on");
		MenuMod("OptionsBoard", Define_Vars.ON);
		for (int i = 0; i < Define_Vars.menus.Length; i++) {
			if (Define_Vars.menus[i] != "OptionsBoard") MenuMod(Define_Vars.menus[i], Define_Vars.OFF);
		}
		unityUIObjects["OptionsBackButton"].GetComponent < Button > ().onClick.AddListener(delegate {
			MainMenu();
		});
		Define_Vars.music = unityUIObjects["OptionsMusicSlider"].GetComponent < Slider > ();
		Define_Vars.music.onValueChanged.AddListener(delegate {
			PrintSlider("musicHandleText", Define_Vars.music.value);
		});
		Define_Vars.sfx = unityUIObjects["OptionsSfxSlider"].GetComponent < Slider > ();
		Define_Vars.sfx.onValueChanged.AddListener(delegate {
			PrintSlider("SfxHandleText", Define_Vars.sfx.value);
		});
	}
	
	
	void PrintSlider(string str, float val) //print slider change to the screen
	{
		Text cashText = unityUIObjects[str].GetComponent < Text > ();
		cashText.text = Mathf.RoundToInt(val).ToString();
	}
	
	void Loading() //loading indicator
	{
		Debug.Log("LoadingBoard is on");
		MenuMod("LoadingBoard", Define_Vars.ON);
		for (int i = 0; i < Define_Vars.menus.Length; i++) {
			if (Define_Vars.menus[i] != "LoadingBoard") MenuMod(Define_Vars.menus[i], Define_Vars.OFF);
		}
		Application.LoadLevel("level1_sence");
	}
	
	void OpenWebSite(string address) {
		System.Diagnostics.Process.Start(address);
	}
	
	public void MenuMod(string menuName, bool mod) //show current menu 
	{
		
		unityUIObjects[menuName].SetActive(mod);
	}

	void LoadMultiPlayer(){
		Application.LoadLevel("MultiPlayer_sence");
	}
}