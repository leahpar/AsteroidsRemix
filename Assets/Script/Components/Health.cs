using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
	public float health = 100.0f;
	public float regeneration = 0.0f;

	private float maxHealth;

	void Start()
	{
		maxHealth = health;
	}

	void Update()
	{
		if (health <= 0)
		{
			EventController.KillAndDestroy(gameObject);
		}
		health += regeneration * Time.deltaTime;
		if (health > maxHealth)
		{
			health = maxHealth;
		}
	}

	void OnDamage(float damage)
	{
		health -= damage;
		if (health < 0) health = 0;
	}

}
