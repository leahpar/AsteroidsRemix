using UnityEngine;
using System.Collections;

public class ShotController : MonoBehaviour
{
	
	public float speed;
	public float damage;
	
	void Start ()
	{
		Explosion ex  = gameObject.AddComponent("Explosion") as Explosion;
		ex.color = Color.white;
		ex.explosionType = Explosion.Shot;
		ex.lifeTime = 1.0f;
    
		rigidbody2D.AddForce(transform.up);
		rigidbody2D.velocity = transform.up * speed;
	}
	
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy")
		{
			EventController.KillAndDestroy(gameObject);
			EventController.MakeDamage(other.gameObject, damage);
		}
		else if (other.tag == "Boundary")
		{
			EventController.KillAndDestroy(gameObject);
		}
	}
}
