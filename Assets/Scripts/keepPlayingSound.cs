using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepPlayingSound : MonoBehaviour
{
	private AudioSource audioSources;
	// Use this for initialization
	void Start ()
	{
		audioSources = gameObject.GetComponent<AudioSource>();

		DontDestroyOnLoad(audioSources);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
