using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	private Vector2 target;
	private float   angle;

	private float nextFire;
	public  float fireRate = 0.1f;
	public GameObject shot;
	public Transform[] shotSpawns;
	private int shotCount = 3;

	private Health healthObj;
	static public float health;

	public delegate void DamageAction();
	public static event DamageAction OnDamageAction;

	public delegate void GameOverAction();
	public static event GameOverAction OnGameOverAction;

	void Start ()
	{
		healthObj = gameObject.AddComponent("Health") as Health;
		healthObj.health = 200.0f;
		healthObj.regeneration = 5.0f;
		
		Explosion ex  = gameObject.AddComponent("Explosion") as Explosion;
		ex.color = Color.white;
		ex.explosionType = Explosion.Player;
		ex.lifeTime = 5.0f;

	}
	
	void Update ()
	{
		int i;

		// Rotate player
		target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		if (target.sqrMagnitude > 0)
		{
			angle = Mathf.Atan2(target.x, target.y) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
		}

		// FIRE !
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			//audio.Play();
			nextFire = Time.time + fireRate;
			for (i=0; i<shotCount; i++)
			{
				Instantiate(shot, shotSpawns[i].position, shotSpawns[i].rotation);
			}
		}

		health = healthObj.health;

		if (health <= 0)
		{
			OnGameOverAction();
		}
	}

	void OnDamage()
	{
		OnDamageAction();
	}
}
