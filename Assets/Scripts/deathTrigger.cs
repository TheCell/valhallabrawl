using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathTrigger : MonoBehaviour {
    public float timerToDestroyPlayer = 2f;

    private GameObject[] players;
     void OnTriggerExit(Collider collision)
      {
        if (collision.gameObject.tag == "Player")
        {
			int playerWon = -2;

			players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
				// increase Player points for all others beside the dead one
                if (player != collision.gameObject)
                {
                    Debug.Log(player);
                    player.GetComponent<playerUI>().increasePointsCounter(player.GetComponent<Movement>().player);
                }

				// check if player has 3 points and finish game
				if (player.GetComponent<playerUI>().hasPlayerWon() > playerWon)
				{
					playerWon = player.GetComponent<playerUI>().hasPlayerWon();
				}
			}

			print("playerWon" + playerWon);
			if (playerWon > 0)
			{
				SceneManager.LoadScene("gameEndscreen", LoadSceneMode.Single);
			}
			else
			{
				SceneManager.LoadScene("Arena", LoadSceneMode.Single);
			}
        }         
      }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
