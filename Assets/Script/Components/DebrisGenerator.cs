using UnityEngine;
using System.Collections;

public class DebrisGenerator : MonoBehaviour
{
	public GameObject debris;
	public float debrisRate = 1;
	private float nextDebris;

	void Update()
	{
		if (Time.time > nextDebris)
		{
			GameObject d;
			AsteroidController a;
			d = Instantiate(debris, transform.position, Quaternion.identity) as GameObject;
			a = d.gameObject.GetComponent<AsteroidController>();
			a.color = gameObject.GetComponent<SpriteRenderer>().color;
			a.FixedStart(transform.rotation, rigidbody2D.velocity);

			nextDebris = Time.time + debrisRate;
		}
	}
}
