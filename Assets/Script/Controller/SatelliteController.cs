using UnityEngine;
using System.Collections;

public class SatelliteController : MonoBehaviour
{
	public float maxSpeed;
	public float maxRotation;

	public float health = 50.0f;
	public Color color  = Color.white;
	public float damage = 50.0f;
	public int   score  = 100;
	public string explosion = Explosion.Basic;

	public int killCount; // count kill or not

	void Start()
	{
		gameObject.GetComponent<SpriteRenderer>().color = color;

		Health healthObj = gameObject.AddComponent("Health") as Health;
		healthObj.health = health * GameController.globalCoeff;

		Explosion explObj  = gameObject.AddComponent("Explosion") as Explosion;
		explObj.color = color;
		explObj.explosionType = explosion;
		explObj.lifeTime = 3.0f;

		Score scoreObj = gameObject.AddComponent("Score") as Score;
		scoreObj.score = (int)(score * GameController.playerCoeff);

		damage = damage * GameController.globalCoeff;
	}

	public void RandomStart()
	{
		Vector2 v = new Vector2(-transform.position.x, -transform.position.y);
		rigidbody2D.AddForce(v * Random.Range(maxSpeed/3, maxSpeed) + Random.insideUnitCircle);
		rigidbody2D.AddTorque(Random.value * maxRotation);

		color = new Color(Random.value, Random.value, Random.value);
  }

	public void FixedStart(Quaternion rotation, Vector2 velocity)
	{
		rigidbody2D.velocity = velocity + Random.insideUnitCircle * maxSpeed;
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

}


