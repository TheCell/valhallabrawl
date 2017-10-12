using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAfterHit : MonoBehaviour
{
	public Material beforeDestroy;
	public Material afterDestroy;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void killyourself(float timerToKill)
	{
		gameObject.GetComponent<Renderer>().material = beforeDestroy;

		StartCoroutine(destroyTile(gameObject, timerToKill));
		//gameObject.GetComponent<Collider>().enabled = false;
	}

	private IEnumerator destroyTile(GameObject tile, float timerBeforeKill)
	{
		for (float t = 0.0f; t < timerBeforeKill;)
		{
			t += Time.deltaTime;
			yield return null; // return here next frame
		}

		tile.GetComponent<Renderer>().material = afterDestroy;
		tile.GetComponent<Collider>().isTrigger = true;
	}
}
