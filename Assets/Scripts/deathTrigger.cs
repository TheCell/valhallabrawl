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
                    Debug.Log(player);
                    player.GetComponent<playerUI>().increasePointsCounter(player.GetComponent<Movement>().player);
                }
            }
            SceneManager.LoadScene("Arena", LoadSceneMode.Single);
        }         
      }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
