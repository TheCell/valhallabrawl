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
            players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                if (player != collision.gameObject)
                {
                    player.GetComponent<playerUI>().increasePointsCounter(player.GetComponent<Movement>().player);
                }
            }
            SceneManager.LoadScene("MikeTestScene", LoadSceneMode.Single);
        }
          /*  GameObject[] players = GameObject.FindGameObjectsWithTag("Player2");
            players[0].GetComponent<player2UI>().increasePointsCounter();
            //Don't destroy, make death animation
            SceneManager.LoadScene("MikeTestScene", LoadSceneMode.Single);*/
            // Destroy(collision.gameObject, timerToDestroyPlayer);
      //  }
       /* else if(collision.gameObject.tag == "Player2")
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player1");
            Debug.Log(players[0].GetComponent<player1UI>().points.text);
            players[0].GetComponent<player1UI>().increasePointsCounter();
            //SceneManager.LoadScene("MikeTestScene", LoadSceneMode.Single);
        }*/
      }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
