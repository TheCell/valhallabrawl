using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathTrigger : MonoBehaviour {
     public float timerToDestroyPlayer = 2f;

     void OnTriggerExit(Collider collision)
      {
         if (collision.gameObject.tag == "Player")
          {
            Debug.Log(collision.gameObject.GetComponent<Movement>().player);
              Destroy(collision.gameObject, timerToDestroyPlayer);
          }
      }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
