using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour
{
	public static float xMax, yMax;


	public GameObject leftBoundary;
	public GameObject rightBoundary;
	public GameObject topBoundary;
	public GameObject bottomBoundary;

	public GameObject leftBoundaryQuad;
	public GameObject rightBoundaryQuad;
	public GameObject topBoundaryQuad;
	public GameObject bottomBoundaryQuad;

	public GameObject starfield;


	void Start()
	{
		Vector3 v, s;

		v = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		xMax = v.x;
		yMax = v.y;

		v = leftBoundary.transform.position;
		v.x = -xMax;
		leftBoundary.transform.position = v;
		s = new Vector3(0, 2 * yMax, 1);
		leftBoundaryQuad.transform.position = v;
		leftBoundaryQuad.transform.localScale = s;

		v = rightBoundary.transform.position;
		v.x = xMax;
		rightBoundary.transform.position = v;
		s = new Vector3(0, 2 * yMax, 1);
		rightBoundaryQuad.transform.position = v;
		rightBoundaryQuad.transform.localScale = s;

		v = topBoundary.transform.position;
		v.y = yMax;
		topBoundary.transform.position = v;
		s = new Vector3(2 * xMax, 0, 1);
		topBoundaryQuad.transform.position = v;
		topBoundaryQuad.transform.localScale = s;

		v = bottomBoundary.transform.position;
		v.y = -yMax;
		bottomBoundary.transform.position = v;
		s = new Vector3(2 * xMax, 0, 1);
		bottomBoundaryQuad.transform.position = v;
		bottomBoundaryQuad.transform.localScale = s;

		v.x = 2.0f * xMax;
		v.y = 2.0f * yMax;
		starfield.transform.localScale = v;
		ParticleSystem[] p = starfield.GetComponentsInChildren<ParticleSystem>();
		int i;
		for (i=0; i<p.Length; i++)
		{
			p[i].Play();
		}
	}


}
