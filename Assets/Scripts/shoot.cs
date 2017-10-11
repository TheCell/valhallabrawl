using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject pinselPrefab;
    public Transform pinselSpawn;
	public GameObject playerCamera;
	public int player = 0;
	public float shootCooldownSeconds = 100.0f;
	private float lastShot = 0.0f;
	private bool canFire = true;

    // Use this for initialization
    void Start ()
	{
		lastShot = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float timeDelta = (Time.realtimeSinceStartup - lastShot - shootCooldownSeconds);
		if (timeDelta > 0.0f)
		{
			canFire = true;
		}
		
		if (player == 0 && ( Input.GetButton("Fire1p1") || Input.GetAxis("Fire1p1") > 0.9f) && canFire)
		{
			fire();
		}
		else if (player == 1 && ( Input.GetButton("Fire1p2") || Input.GetAxis("Fire1p2") > 0.9f) && canFire)
		{
			fire();
		}
    }
    private void fire()
    {
		canFire = false;
		lastShot = Time.realtimeSinceStartup;

		/*
		GameObject pinsel = (GameObject)Instantiate(
			pinselPrefab,
			pinselSpawn.position,
			pinselSpawn.rotation);
		*/

		GameObject pinsel = (GameObject)Instantiate(
			pinselPrefab,
			pinselSpawn.position,
			playerCamera.transform.rotation);

		//Add velocity to the pinsel
		pinsel.GetComponent<Rigidbody>().velocity = pinsel.transform.forward * 60;
        //Destroy the pinsel
        Destroy(pinsel, 5.0f);
    }
}
