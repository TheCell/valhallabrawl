using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinselCollision : MonoBehaviour {
    public float timerBeforeKill = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hexagon")
        {
			collision.gameObject.GetComponent<destroyAfterHit>().killyourself(timerBeforeKill);
			
			gameObject.SetActive(false);
		}
		else if (collision.gameObject.tag == "boundary")
		{
			gameObject.SetActive(false);
			print("speer hat deathsphere erreicht");
		}
	}

    // Update is called once per frame
    void Update () {
		
	}
}
