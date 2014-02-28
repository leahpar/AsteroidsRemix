using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{
	public GUITexture b1;
	public GUITexture b2;
	public GUITexture b3;

	public GUISkin skin;

	float originalWidth = 1900;
	float originalHeight = 1000;
	Vector3 scale;

	// Use this for initialization
	void Start ()
	{
		/*
		float scale = 2.0f;
		SetSize(b1, scale);
		SetSize(b2, scale);
		SetSize(b3, scale);
		*/
	}

	void OnGUI()
	{
		scale.x = Screen.width / originalWidth;
		scale.y = Screen.height / originalHeight;
		if (scale.x < scale.y)
			scale.y = scale.x;
		else
			scale.x = scale.y;
		scale.z = 1;

		// http://answers.unity3d.com/questions/150736/script-gui-units.html
		Matrix4x4 mat = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);

		GUI.skin = skin;
		//GUI.skin.button.fontSize = (int)((float)Screen.width * 0.038f);
		GUI.Button(new Rect(20, 20, 600, 100), "blabla");

		GUI.matrix = mat;
	}



	void SetSize(GUITexture b, float scale)
	{
		Rect r = b.pixelInset;
		b.pixelInset = new Rect(r.xMin * scale,
		                        r.yMin * scale,
		                        r.width * scale,
		                        r.height * scale);
	}
}
