using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinselCollision : MonoBehaviour {

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
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
