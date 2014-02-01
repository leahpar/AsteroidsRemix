using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List

public class GameController : MonoBehaviour
{

	public  GameObject[] asteroidsPrefab;
	private List<GameObject> asteroids;
  
	public  GameObject[] enemiesPrefab;
	private List<GameObject> enemies;

	public  GameObject[] bonusesPrefab;
	private List<GameObject> bonuses;

	private GUIController gui;

	// Game's variables
	private int   score = 0;

	public float startWait = 2.0f;
	public float spawnWait = 1.5f;
	public float waveWait  = 5.0f;

	private bool gameOver;


	void Start()
	{
		gui = gameObject.GetComponent<GUIController>();

		StartCoroutine ("SpawnAsteroids");
	}
	

	IEnumerator SpawnAsteroids ()
	{
		GameObject spawn;
		AsteroidController spawnController;
		Vector2 spawnPosition;
		int totalSpawns, currentSpawn;

		gameOver = false;

		yield return new WaitForSeconds (startWait);

		while (!gameOver)
		{
			totalSpawns = Random.Range(10, 20);
			currentSpawn = 0;
			while (currentSpawn < totalSpawns)
			{
				spawnPosition = new Vector2((Random.value > 0.5) ? WorldController.xMax : -WorldController.xMax,
				                            Random.Range(-WorldController.yMax, WorldController.yMax));
					
				spawn = Instantiate (asteroidsPrefab[Random.Range(0, asteroidsPrefab.Length)],
				                     spawnPosition,
				                     Quaternion.identity) as GameObject;
				spawnController = spawn.GetComponent<AsteroidController>();
				spawnController.RandomStart();

				currentSpawn++;
				yield return new WaitForSeconds (Random.Range(spawnWait/2, spawnWait));
			}
			yield return new WaitForSeconds (waveWait);
		}
	}

	public void AddScore(int points)
	{
		gui.AddScore(points);
		score += points;
	}


	void GameOver()
	{
		gameOver = true;
	}
	
	void OnEnable()  { PlayerController.OnGameOverAction += GameOver; }
	void OnDisable() { PlayerController.OnGameOverAction -= GameOver; }
}

