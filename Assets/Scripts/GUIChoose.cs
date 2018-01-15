using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIChoose : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("CrossUpDown") > 0)
		{
			Debug.Log("up");
		}
		else if (Input.GetAxis("CrossUpDown") < 0)
		{
			Debug.Log("down");
		}
		else if (Input.GetAxis("CrossLeftRight") < 0)
		{
			GetComponent<StartOptions>().startMap1();
        }
        else if (Input.GetAxis("CrossLeftRight") > 0)
        {
			GetComponent<StartOptions>().startMap2();
        }
    }
}
