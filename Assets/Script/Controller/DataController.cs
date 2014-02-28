using UnityEngine;
using System.Collections;

public class DataController : MonoBehaviour
{

	// version
	public static float version = 0.1f;

	// Global score
	public static int   GlobalScore		= 0;

	// Options
	public static int   OptMusic		= 0;
	public static int   OptSound		= 0;

	// upgrades
	public static int   UpLife			= 0;
	public static int   UpRegen			= 0;
	public static int   UpDmg			= 0;
	public static int   UpFireRate 		= 0;
	public static int   UpShot			= 9;


	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
		// uncomment to reset prefs
		// PlayerPrefs.DeleteAll();
		// SaveData();
		LoadData();
		GlobalScore += 1000000;
	}

	void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus) SaveData();
	}

	void OnApplicationQuit()
	{
		SaveData();
	}

	void SaveData()
	{
		PlayerPrefs.SetInt("score", 		GlobalScore);
		PlayerPrefs.SetInt("OptMusic", 		OptMusic);
		PlayerPrefs.SetInt("OptSound", 		OptSound);
		PlayerPrefs.SetInt("UpLife", 		UpLife);
		PlayerPrefs.SetInt("UpRegen", 		UpRegen);
		PlayerPrefs.SetInt("UpDmg", 		UpDmg);
		PlayerPrefs.SetInt("UpFireRate", 	UpFireRate);
		PlayerPrefs.SetInt("UpShot", 		UpShot);
		PlayerPrefs.Save();
	}

	void LoadData()
	{
		GlobalScore = PlayerPrefs.GetInt("score", 		GlobalScore);
		OptMusic 	= PlayerPrefs.GetInt("OptMusic",	OptMusic);
		OptSound 	= PlayerPrefs.GetInt("OptSound",	OptSound);
		UpLife 		= PlayerPrefs.GetInt("UpLife",		UpLife);
		UpRegen		= PlayerPrefs.GetInt("UpRegen", 	UpRegen);
		UpDmg 		= PlayerPrefs.GetInt("UpDmg", 		UpDmg);
		UpFireRate 	= PlayerPrefs.GetInt("UpFireRate", 	UpFireRate);
		UpShot 		= PlayerPrefs.GetInt("UpShot", 		UpShot);
	}
}


