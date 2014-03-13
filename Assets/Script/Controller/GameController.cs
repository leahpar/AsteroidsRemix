using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List

public class GameController : MonoBehaviour
{
	// asteroids to spawn
	public GameObject asteroidsPrefab;
	public GameObject satellitesPrefab; 


	// player prefab
	public GameObject playerPrefab;
	private GameObject player;
	public GameObject shieldPrefab;
	private GameObject shield;

	// game's components
	private HUDController hud;
	private MenuController menu;
	private MusicController music;
	private CaracController carac;

	// Game's variables
	public int   score = 0;
	private bool gameOver;
	public static int enemiesCount;

	public static int level;

	private float playerCoeffBase = 0;
	// coeff
	private float globalCoeffBase = 0;
	public static float globalCoeff, playerCoeff, playerCoeff2;	/* XX */
	// current game's variable
	public int asteroidKilled;
	public int timePlayed;


	// Waves' options
	public float spawnWait = 1.5f;
	public float waveWait  = 3.0f;


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


	IEnumerator RunGame()
	{
		level = 1;
		while (true)
		{
			// Run level
			hud.SetSplash("Level " + level, 4.0f);
			yield return new WaitForSeconds (waveWait);

			enemiesCount = 0;

			StartCoroutine("SpawnAsteroids");
			StartCoroutine("SpawnSatellites");
			StartCoroutine("SpawnBonuses");
			
			// wait for waves to be initialized
			yield return new WaitForSeconds (1.0f);

			// wait for enemies to be destroyed
			while (enemiesCount > 0)
			{
				yield return 0;
			}

			level++;
		}
	}

	/*
	IEnumerator RunLevel()
	{
		StartCoroutine("SpawnAsteroids");
		StartCoroutine("SpawnSatellites");
		StartCoroutine("SpawnBonuses");

		// wait for waves to be initialized
		yield return new WaitForSeconds (1.0f);

		while (enemiesCount > 0)
		{
			yield return 0;
		}
		levelDone = true;
	}
	*/

	IEnumerator SpawnAsteroids()
	{
		int cpt = 0;
		int enemies = 10 + level / 2;
		enemiesCount += enemies;

		GameObject spawn;
		AsteroidController enemiController;
		Vector2 enemiPosition;

		while (cpt < enemies)
		{
			// Set a random position to spawn
			enemiPosition = new Vector2((Random.value > 0.5f) ? WorldController.xMax : -WorldController.xMax,
			                            Random.Range(-WorldController.yMax, WorldController.yMax));
			// Instantiace asteroid
			spawn = Instantiate (asteroidsPrefab, enemiPosition, Quaternion.identity) as GameObject;
			// Random start
			enemiController = spawn.GetComponent<AsteroidController>();
			// TODO : spawnController.health *= xxx; // Health + 20% per level

			enemiController.SetDebris(level / 2);
			enemiController.RandomStart();

			// TODO : difficulty...
			yield return new WaitForSeconds(15.0f / (float)enemies);
			cpt++;
		}
		yield return 0;
	}

	IEnumerator SpawnSatellites()
	{
		int cpt = 0;
		int enemies = level / 4;
		enemiesCount += enemies;

		GameObject spawn;
		AsteroidController enemiController;
		Vector2 enemiPosition;
		
		while (cpt < enemies)
		{
			// Set a random position to spawn
			enemiPosition = Random.insideUnitCircle.normalized * WorldController.yMax;
			// Instantiace satellite
			spawn = Instantiate (satellitesPrefab, enemiPosition, Quaternion.identity) as GameObject;
			// Random start
			enemiController = spawn.GetComponent<AsteroidController>();
			// TODO : spawnController.health *= xxx; // Health + 20% per level
			enemiController.SetDebris(level);
			enemiController.SetDebrisRate(10.0f / (float)level);
			enemiController.RandomStart();
			
			// TODO : difficulty...
			yield return new WaitForSeconds(15.0f / (float)enemies);
			cpt++;
		}
		yield return 0;

	}

	IEnumerator SpawnBonuses()
	{
		// ...
		yield return 0;

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
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(GameObject e in enemies)
		{
			if (e) Destroy(e);
		}

		// start music
		music.StartMusic();

		// add a player !
		player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		//shield = Instantiate(shieldPrefab, Vector2.zero, Quaternion.identity) as GameObject;

		// and run the game
		StartCoroutine ("RunGame");
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
		if (player) EventController.KillAndDestroy(player);
		if (shield) EventController.KillAndDestroy(shield);

		// stop spawing waves
		StopCoroutine("RunGame");

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

