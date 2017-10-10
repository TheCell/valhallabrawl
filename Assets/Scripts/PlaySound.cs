using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
	public AudioSource startMusicSource;
	public AudioSource loopMusicSource;

	void Start()
	{
		StartCoroutine(playStartSoundAndLoopAfterwards());
	}

	IEnumerator playStartSoundAndLoopAfterwards()
	{
		startMusicSource.Play();

		yield return new WaitForSeconds(loopMusicSource.clip.length);
		print("end of intro sound");
		loopMusicSource.Play();
	}
}
