using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerUI : MonoBehaviour {
    public Text points;
    private static int counterPlayer2 = 0;
    private static int counterPlayer1 = 0;

    // Use this for initialization
    void Start()
    {
        writePoints(gameObject.GetComponent<playerControlls>().player);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void increasePointsCounter(int player)
    {
        if(player == 0)
        {
            counterPlayer1++;
        }
        else
        {
            counterPlayer2++;
        }        
        writePoints(player);
    }

    public void writePoints(int player)
    {
        if (player == 0)
        {
            points.text = "Punkte: " + counterPlayer1.ToString();
        }
        else
        {
            points.text = "Punkte: " + counterPlayer2.ToString();
        }        
    }

	public int hasPlayerWon()
	{
		int winner = -1;

		if (counterPlayer1 >= 3)
		{
			winner = 1;
		}
		else if (counterPlayer2 >= 3)
		{
			winner = 2;
		}

		return winner;
	}

	public bool resetScore()
	{
		counterPlayer1 = 0;
		counterPlayer2 = 0;

		return counterPlayer1 == 0 && counterPlayer2 == 0;
	}
}
