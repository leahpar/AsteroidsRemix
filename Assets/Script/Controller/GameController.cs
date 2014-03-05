using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List

public class GameController : MonoBehaviour
{
	// asteroids to spawn
	public  GameObject[] asteroidsPrefab;

	// player prefab
	public GameObject playerPrefab;
	private GameObject player;

	// game's components
	private HUDController hud;
	private MenuController menu;
	private MusicController music;
	private CaracController carac;

	// Game's variables
	public int   score = 0;
	private bool gameOver;

	private float playerCoeffBase = 0;
	// coeff
	private float globalCoeffBase = 0;
	public static float globalCoeff, playerCoeff, playerCoeff2;	/* XX */
	// current game's variable
	public int asteroidKilled;
	public int timePlayed;


	// Waves' options
	public float startWait = 2.0f;
	public float spawnWait = 1.5f;
	public float waveWait  = 5.0f;


	/* 
	 * Function Start
	 * Init game's components
	 */
	void Start()
	{
		hud   = gameObject.GetComponent<HUDController>();
		menu  = gameObject.GetComponent<MenuController>();
		music = gameObject.GetComponent<MusicController>();
		carac = gameObject.GetComponent<CaracController>();
	}

	/*
	 * coroutine SpawnAsteroids
	 * Spawn asteroids in an infinite loop
	 * (until coroutine is stopped by GameOver function)
	 */
	IEnumerator SpawnAsteroids ()
	{
		GameObject spawn;
		AsteroidController spawnController;
		Vector2 spawnPosition;
		int waveSpawns, currentSpawns;

		yield return new WaitForSeconds (startWait);

		// bla bla get ready

		while (true)
		{
			waveSpawns = Random.Range(10, 20);
			currentSpawns = 0;
			while (currentSpawns < waveSpawns)
			{
				// Set a random position to spawn
				spawnPosition = new Vector2((Random.value > 0.5) ? WorldController.xMax : -WorldController.xMax,
				                            Random.Range(-WorldController.yMax, WorldController.yMax));
				// Instantiace asteroid
				spawn = Instantiate (asteroidsPrefab[Random.Range(0, asteroidsPrefab.Length)],
				                     spawnPosition,
				                     Quaternion.identity) as GameObject;
				// Init random speed and torque
				spawnController = spawn.GetComponent<AsteroidController>();
				spawnController.RandomStart();

				currentSpawns++;
				yield return new WaitForSeconds (Random.Range(spawnWait/2, spawnWait));
			}
			asteroidKilled += waveSpawns;
			yield return new WaitForSeconds (waveWait);
		}
	}

	/* 
	 * Function AddScore
	 */
	public void AddScore(int points)
	{
		hud.AddScore(points);
		score += points;
	}

	/* 
	 * Function AddKill
	 * Count enemies killed
	 */
	public void AddKill(int count)
	{
		asteroidKilled += count;
	}

	/*
	 * Function StartGame
	 * Init player, waves... and start a game
	 */
	public void StartGame()
	{
		// Reset game's variables
		ResetGlobalCoeff();
		ResetPlayerCoeff();
		score = 0;
		hud.ResetScore();
		DataController.StatCurKills = 0;

		// Destroy enemies of the last game
		GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(GameObject a in asteroids)
		{
			if (a) Destroy(a);
		}

		// start music
		music.StartMusic();

		// add a player !
		player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		// and spaws asteroids
		StartCoroutine ("SpawnAsteroids");
	}

	/*
	 * Function GameOver
	 * Stop the game
	 */
	public void GameOver()
	{
		// Resume game (if stopped)
		Time.timeScale = 1;

		// time played
		timePlayed = (int)(Time.time - globalCoeffBase);
		DataController.UpdateStatTime(timePlayed);

		// stop music
		music.StopMusic();

		// Destroy player
		EventController.KillAndDestroy(player);

		// stop spawing waves
		StopCoroutine("SpawnAsteroids");

		// Update global score
		DataController.Points += score;
		DataController.gPoints += score;

		// Set menu
		menu.state = MenuController.MENU_STATE_OVER;
	}

	/*
	 * Update coeffs
	 */
	void FixedUpdate()
	{
		playerCoeff2 = Time.time - playerCoeffBase;
		globalCoeff = 1.0f + (Time.time - globalCoeffBase) / 100.0f;
		playerCoeff = 1.0f + (Time.time - playerCoeffBase) / 100.0f;
		playerCoeff2 = Time.time - playerCoeffBase;
	}

	/*
	 * Function ResetPlayerCoeff
	 * Reset player's coeff
	 */
	void ResetPlayerCoeff()
	{
		playerCoeffBase = Time.time - carac.GetBonus();
	}

	/*
	 * function ResetGlobalCoeff
	 * Reset global coeff
	 */
	void ResetGlobalCoeff()
	{
		globalCoeffBase = Time.time;
	}


	/* EVENTS */

	void OnEnable()
	{
		PlayerController.OnDamageAction += ResetPlayerCoeff;
		PlayerController.OnGameOverAction += GameOver;
	}
	void OnDisable()
	{
		PlayerController.OnDamageAction -= ResetPlayerCoeff;
		PlayerController.OnGameOverAction -= GameOver;
	}

}

