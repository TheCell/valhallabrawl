using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayWinner : MonoBehaviour {
	public Text winnerIs;

	// Use this for initialization
	void Start ()
	{
		// HARD GEBASTELT, muss gleich fertig
		GameObject[] players = GameObject.FindGameObjectsWithTag("EndscreenUI");
		int winnerNumber = -2;

		foreach (GameObject player in players)
		{
			if (player.GetComponent<playerUI>().hasPlayerWon() > winnerNumber)
			{
				winnerNumber = player.GetComponent<playerUI>().hasPlayerWon();
			}

			player.GetComponent<playerUI>().resetScore();
		}

		winnerIs.text = "Winner is: " + winnerNumber;
			
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
