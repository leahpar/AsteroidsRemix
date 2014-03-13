using UnityEngine;
using System.Collections;
using System.Collections.Generic; // List


public class ShieldController : MonoBehaviour
{
	public Transform shotSpawn;
	public GameObject shot;
	public float fireRate;
	private float nextFire;

	private CaracController carac;

	private List<Transform> enemies;

	void Start()
	{
		carac = GameObject.FindWithTag("GameController").GetComponent<CaracController>();

		enemies = new List<Transform>();

		fireRate = carac.GetShieldFireRate();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name.Substring(0, 6) == "Debris") // <= BEARK ! Need multi-tags !
		{
			enemies.Add(other.transform);
		}
	}

	void Update()
	{
		int i;
		Transform e = null;
		float d = 99999;

		if (enemies.Count > 0 && Time.time > nextFire)
		{
			for (i=enemies.Count-1; i>=0; i--)
			{
				if (enemies[i] == null)
				{
					enemies.RemoveAt(i);
				}
				else if (enemies[i].position.sqrMagnitude < d)
				{
					d = enemies[i].position.sqrMagnitude;
					e = enemies[i];
				}
			}
		}
		if (e && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			float angle = Mathf.Atan2(e.position.x, e.position.y) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}
}
