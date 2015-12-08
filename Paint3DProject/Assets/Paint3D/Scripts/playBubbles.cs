using UnityEngine;
using System.Collections;

public class playBubbles : MonoBehaviour {

	public float playTime;
	public float startTime;
	public AudioClip[] bubbleSounds;

	// Use this for initialization
	void Start () {
		startTime = GetComponent<ParticleSystem> ().startLifetime;
		playTime = GetComponent<ParticleSystem> ().duration;
			InvokeRepeating("PlayBubbles", startTime, playTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PlayBubbles()
	{
		AudioClip randBub = bubbleSounds[Random.Range(0,bubbleSounds.Length)];
		GetComponent<AudioSource>().clip = randBub;
		GetComponent<AudioSource>().Play();

	}
}
