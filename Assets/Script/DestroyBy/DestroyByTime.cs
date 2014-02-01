using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{
	public float time = 2.0f;

	void Start ()
	{
		Destroy(gameObject, time);
	}
}
