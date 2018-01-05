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
        StartCoroutine(animateText(points, 2.0f));
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

    private IEnumerator animateText(Text textToAnimate, float durationSeconds)
    {
        Text startingText = textToAnimate;
        Vector3 startingScale = textToAnimate.transform.localScale;

        for (float t = 0.0f; t < durationSeconds;)
        {
            yield return null; // return here next frame
            // animate the color from yellow to original
            textToAnimate.color = new Color(
                Mathf.Lerp(1.0f, startingText.color.r, 1.0f / durationSeconds * t),
                Mathf.Lerp(1.0f, startingText.color.r, 1.0f / durationSeconds * t),
                Mathf.Lerp(0.0f, startingText.color.r, 1.0f / durationSeconds * t));

            // scale from big to small
            textToAnimate.transform.localScale = new Vector3(
                Mathf.Lerp(4.0f, startingScale.x, 1.0f / durationSeconds * t),
                Mathf.Lerp(4.0f, startingScale.y, 1.0f / durationSeconds * t),
                Mathf.Lerp(4.0f, startingScale.z, 1.0f / durationSeconds * t));

            // update t
            t += Time.deltaTime;
        }

        textToAnimate = startingText;
    }
}
