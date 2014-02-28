using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
	public const string Basic  = "Basic explosion";
	public const string Shot   = "Shot explosion";
	public const string Player = "Player explosion";

	public Color  color = Color.white;
	public string explosionType = Explosion.Basic;
	public float  lifeTime = 5.0f;

	void OnDeath()
	{
		GameObject explosion = GameObject.Find(explosionType);
		GameObject e = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;

		ParticleSystem p = e.GetComponent<ParticleSystem>();
		p.startColor = color;
		p.Play();

		if (e.audio && (DataController.OptSound == 1)) e.audio.Play();

		Destroy(e, lifeTime);
	}
}
