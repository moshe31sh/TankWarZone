using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Define_Vars: MonoBehaviour {
	
	public static readonly bool ON = true;
	public static readonly bool OFF = false;
	public static Slider cash = null;
	public static Slider music = null;
	public static Slider sfx = null;
	public static readonly string[] menus = {
		"MainMenuBoard", "LoadingBoard", "MultiPlayerBoard", "StudentInfoBoard", "OptionsBoard"
	};
	
	public enum Player {
		One, Two, Init
	};

	public static Player Winner = Player.Init;
	public static string apiKey = "f1ea6bce5d045203c2c346f9108e7f0ea592bd7b12c219a8db3fc8c4c714ccc8";
	public static string secretKey = "c5e455877837775eb9a0d5a85f6cc9fbf513f111680defa97deaa361e4d1a2c7";

	public static Vector3 tankPositionForJson;
	public static int bulletSpeedForJson ;
	public static float bulletAngleForJson ;

}