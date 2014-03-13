using UnityEngine;
using System.Collections;

public class DataController : MonoBehaviour
{

	public const string Title 			= "Asteroids Remix";
	public const float Version 			= 0.1f;
	public const string sVersion        = "alpha1 (20140313)";
	private float 	   CurVersion 		= 0.0f;

	/*
	 * v0.1
	 * alpha1 2014-03-13
	 * 
	 * 
	 */

	// Score
	public static int   Points			= 0;
	public static int 	gPoints			= 0;

	// Statistics
	public static int	StatTime		= 0;
	public static int	StatBestTime	= 0;
	public static int	StatKills		= 0;
	public static int   StatCurKills    = 0; // not saved
	public static int	StatBestKills	= 0;

	// Options
	public static int   OptMusic		= 1;
	public static int   OptSound		= 1;

	// upgrades
	public static int   UpLife			= 0;
	public static int   UpRegen			= 0;
	public static int   UpDmg			= 0;
	public static int   UpFireRate 		= 0;
	public static int   UpShot			= 11;
	public static int   UpBonus         = 0;
	public static int   UpShield		= 0;


	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
		// uncomment to reset prefs
		//PlayerPrefs.DeleteAll();
		//SaveData();
		LoadData();
		//Points += 10000000;
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
		PlayerPrefs.SetFloat("version", 	Version);
		PlayerPrefs.SetInt("points", 		Points);
		PlayerPrefs.SetInt("gpoints", 		gPoints);
		PlayerPrefs.SetInt("OptMusic", 		OptMusic);
		PlayerPrefs.SetInt("OptSound", 		OptSound);
		PlayerPrefs.SetInt("UpLife", 		UpLife);
		PlayerPrefs.SetInt("UpRegen", 		UpRegen);
		PlayerPrefs.SetInt("UpDmg", 		UpDmg);
		PlayerPrefs.SetInt("UpFireRate", 	UpFireRate);
		PlayerPrefs.SetInt("UpShot", 		UpShot);
		PlayerPrefs.SetInt("UpBonus", 		UpBonus);
		PlayerPrefs.SetInt("UpShield", 		UpShield);
		PlayerPrefs.SetInt("StatKills", 	StatKills);
		PlayerPrefs.SetInt("StatBestKills", StatBestKills);
		PlayerPrefs.SetInt("StatTime", 		StatTime);
		PlayerPrefs.SetInt("StatBestTime", 	StatBestTime);
		PlayerPrefs.Save();
	}

	void LoadData()
	{
		CurVersion  	= PlayerPrefs.GetFloat("version",		CurVersion);
		Points 			= PlayerPrefs.GetInt("points", 			Points);
		gPoints 		= PlayerPrefs.GetInt("gpoints", 		gPoints);
		OptMusic 		= PlayerPrefs.GetInt("OptMusic",		OptMusic);
		OptSound 		= PlayerPrefs.GetInt("OptSound",		OptSound);
		UpLife 			= PlayerPrefs.GetInt("UpLife",			UpLife);
		UpRegen			= PlayerPrefs.GetInt("UpRegen", 		UpRegen);
		UpDmg 			= PlayerPrefs.GetInt("UpDmg", 			UpDmg);
		UpFireRate 		= PlayerPrefs.GetInt("UpFireRate", 		UpFireRate);
		UpShot 			= PlayerPrefs.GetInt("UpShot", 			UpShot);
		UpBonus 		= PlayerPrefs.GetInt("upBonus", 		UpBonus);
		UpShield		= PlayerPrefs.GetInt("UpShield", 		UpShield);
		StatKills   	= PlayerPrefs.GetInt("StatKills", 		StatKills);
		StatBestKills 	= PlayerPrefs.GetInt("StatBestKills", 	StatBestKills);
		StatTime 		= PlayerPrefs.GetInt("StatTime", 		StatTime);
		StatBestTime 	= PlayerPrefs.GetInt("StatBestTime", 	StatBestTime);

		if (CurVersion < Version)
		{
			UpdateData();
		}
	}

	void UpdateData()
	{

	}

	/*********** STATISTICS ******************************/
	public static void InitStatKills()
	{
		StatCurKills = StatKills;
	}
	public static void UpdateStatKills(int kill)
	{
		StatKills += kill;
		if (StatKills - StatCurKills > StatBestKills)
		{
			StatBestKills = StatKills - StatCurKills;
		}
	}
	public static void UpdateStatTime(int time)
	{
		StatTime += time;
		if (StatTime > StatBestTime) StatBestTime = StatTime;
	}
	public static string GetStatistics()
	{
		return
			"<b>Statistics</b>\n" +
			"<b>Total asteroids killed</b>\n" 	+ StatKills 	+ "\n\n" +
			"<b>Best  asteroids killed</b>\n" 	+ StatBestKills + "\n\n" +
			"<b>Total time played</b>\n" 		+ StatTime 		+ "\n\n" +
			"<b>Best time played</b>\n" 		+ StatBestTime 	+ "\n\n" ;

	}
}


