using UnityEngine;
using System.Collections;

public class EventController : MonoBehaviour
{
	public static void KillAndDestroy(GameObject go)
	{
		go.BroadcastMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
		Object.Destroy(go);
	}
	
	public static void MakeDamage(GameObject go, float damage)
	{
		go.BroadcastMessage("OnDamage", damage, SendMessageOptions.DontRequireReceiver);
	}
	
}

