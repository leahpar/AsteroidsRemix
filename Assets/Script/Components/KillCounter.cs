using UnityEngine;
using System.Collections;

public class KillCounter : MonoBehaviour
{
	void OnDeath()
	{
		DataController.UpdateStatKills(1);
	}
}

