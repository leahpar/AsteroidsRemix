using UnityEngine;
using System.Collections;

public class Debris : MonoBehaviour
{
	public GameObject debris;
	public int debrisCount = 3;

	void OnDeath()
	{
		GameObject d;
		AsteroidController a;
		int i;
		for (i=0; i<debrisCount; i++)
		{
			d = Instantiate(debris, transform.position, Quaternion.identity) as GameObject;
			a = d.gameObject.GetComponent<AsteroidController>();
			a.color = gameObject.GetComponent<SpriteRenderer>().color;
			a.FixedStart(transform.rotation, rigidbody2D.velocity);
		}
	}
}

