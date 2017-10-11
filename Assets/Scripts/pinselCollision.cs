using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinselCollision : MonoBehaviour {
    public float timerToKillHexagon = 0.5f;
    public Material beforeDestroy;
	// Use this for initialization
	void Start () {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        // ContactPoint contact = collision.contacts[0];
        // Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        // Vector3 pos = contact.point;
        if (collision.gameObject.tag == "Hexagon")
        {
            //Destroy(gameObject);
            collision.gameObject.GetComponent<Renderer>().material = beforeDestroy; 
            Destroy(collision.gameObject, timerToKillHexagon);
			gameObject.SetActive(false);
        }
		else if (collision.gameObject.tag == "boundary")
		{
			gameObject.SetActive(false);
			print("pinsel hat deathsphere erreicht");
		}
	}

    // Update is called once per frame
    void Update () {
		
	}
}
