using UnityEngine;
using System.Collections;

public class DataController : MonoBehaviour
{

	public static int   score;
	public static int   music;
	public static int   sound;
	public static float life;
	public static float regen;
	public static int   turrets;
	public static string status;


	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
		LoadData();
	}

	void OnApplicationPause(bool pauseStatus)
	{
		status = "OnApplicationPause";
		if (pauseStatus) SaveData();
	}

	void OnApplicationQuit()
	{
		status = "OnApplicationQuit";
		SaveData();
	}

	void SaveData()
	{
		PlayerPrefs.SetInt("score", score);
		PlayerPrefs.SetInt("music", music);
		PlayerPrefs.SetInt("sound", sound);
		PlayerPrefs.SetFloat("life", life);
		PlayerPrefs.SetFloat("regen", regen);
		PlayerPrefs.SetInt("turrets", turrets);
		PlayerPrefs.SetString("status", status);
		PlayerPrefs.Save();
	}

	void LoadData()
	{
		score = PlayerPrefs.GetInt("score", 0);
		music = PlayerPrefs.GetInt("music", 1);
		sound = PlayerPrefs.GetInt("sound", 1);
		life  = PlayerPrefs.GetFloat("life", 0);
		regen = PlayerPrefs.GetFloat("regen", 0);
		turrets = PlayerPrefs.GetInt("turrets", 1);
		status = PlayerPrefs.GetString("status", "init");
	}
}
