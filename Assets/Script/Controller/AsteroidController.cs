using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour
{
	public float speedOffsetMin;
	public float speedOffsetMax;
	private float speedDelta;

	public float orbitOffsetMin;
	public float orbitOffsetMax;
	private float orbitDelta;

	public float maxRotation;

	// Cylindrical coordinates
	private float d;
	private float d0;
	private float a;
	/*
	 * x = d.cos(a);
	 * y = d.sin(a);
	 * 
	 * d = sqrt(x2 + y2);
	 * a = atan(y/x);
	 */

	public float health = 100.0f;
	public Color color  = Color.white;
	public float damage = 50.0f;
	public int   score  = 100;
	public string explosion = Explosion.Basic;


	public int killCount; // count kill or not

	void Start()
	{
		gameObject.GetComponent<SpriteRenderer>().color = color;

		Health healthObj = gameObject.AddComponent("Health") as Health;
		healthObj.health = health; // * GameController.globalCoeff;

		Explosion explObj = gameObject.AddComponent("Explosion") as Explosion;
		explObj.color = color;
		explObj.explosionType = explosion;
		explObj.lifeTime = 3.0f;

		Score scoreObj = gameObject.AddComponent("Score") as Score;
		scoreObj.score = (int)(score * GameController.playerCoeff);

		damage = damage * GameController.globalCoeff;

		// init cylindrical coordinates
		d = Mathf.Sqrt(Mathf.Pow(transform.position.x, 2) +
		               Mathf.Pow(transform.position.y, 2));
		d0 = d;
		a = Mathf.Atan(transform.position.y / transform.position.x);
		if (transform.position.x < 0) a += Mathf.PI;

		// init random attractive velocity & orbiting velocity
		float r = Random.value;
		if (r > 0.5f) r = 1; else r = -1;
		speedDelta = Random.Range(speedOffsetMin, speedOffsetMax);
		orbitDelta = Random.Range(orbitOffsetMin, orbitOffsetMax) * r / 10.0f;
	}

	void Update()
	{
		// attractive force
		d -= speedDelta * Time.deltaTime;

		// orbiting force
		a += orbitDelta * Time.deltaTime * (1.0f + d0/d);

		// new position
		Vector2 pos = new Vector2(d * Mathf.Cos(a), d * Mathf.Sin(a));
		transform.position = pos;
	}

	public void RandomStart()
	{
		rigidbody2D.AddTorque(Random.value * maxRotation);

		color = new Color(Random.value, Random.value, Random.value);
		//Debug.Log("r " + color.r + " g " + color.g + " b " + color.b + " > " + (2.0f * color.r + 3.0f * color.g + 1.0f * color.b));
		if (2.0f * color.r + 3.0f * color.g + 1.0f * color.b < 1.0f)
		{
			color.r += 0.1f;
			color.g += 0.1f;
			color.b += 0.1f;
		}
	}

	public void FixedStart(Quaternion rotation, Vector2 velocity)
	{
		rigidbody2D.AddTorque(Random.value * maxRotation);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			EventController.KillAndDestroy(gameObject);
			EventController.MakeDamage(other.gameObject, damage);
		}
	}

	public void SetDebris(int debris)
	{
		Debris d = gameObject.GetComponent<Debris>();
		if (d) d.debrisCount = debris;
	}

	public void SetDebrisRate(float debrisRate)
	{
		DebrisGenerator d = gameObject.GetComponent<DebrisGenerator>();
		if (d) d.debrisRate = debrisRate;
	}

}


