using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerUI : MonoBehaviour {
    public Text points;
    private int counter;

	// Use this for initialization
	void Start () {
        points.text = "Punkte: 0";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void increasePointsCounter()
    {
        counter++;
        points.text = "Punkte: "+counter.ToString();
    }
}