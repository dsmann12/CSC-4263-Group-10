using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackalAudio : MonoBehaviour {
    public float unalertedGrowlRate = 20f;
    public float alertedGrowlRate = 6f;
	public bool hidden = false;
	
	private Enemy enemy;
    private AudioSource[] audioSources;
	private float audioTimer;
	private float nextGrowl;
	private bool alerted;
	
	// Use this for initialization
	void Start () {
        audioSources = GetComponents<AudioSource>();
		nextGrowl = Random.Range(3f, 20f);
		audioTimer = 0;
		enemy = GetComponent<Enemy>();
		alerted = enemy.DetectedPlayer();
	}
	
	// Update is called once per frame
	void Update () {
        audioTimer += Time.deltaTime;
		if(alerted != enemy.DetectedPlayer()){
			alerted = !alerted;

            if (alerted)
            {
                //Stop unalerted growl and play alerted growl
                audioSources[0].Stop();
                int randi = Random.Range(1, audioSources.Length);
                audioSources[randi].Play();
                audioTimer = 0;
                nextGrowl = Random.Range(alertedGrowlRate - 1f, alertedGrowlRate + 1f);
            }
		}
		else{
            if (audioTimer > nextGrowl)
            {
				if(alerted){
					int randi = Random.Range(1, audioSources.Length);
					audioSources[randi].Play();
					audioTimer = 0;
					nextGrowl = Random.Range(alertedGrowlRate - 1f, alertedGrowlRate + 1f);
				}
				else if(!hidden) {
					audioSources[0].Play();
					audioTimer = 0;
					nextGrowl = Random.Range(unalertedGrowlRate, unalertedGrowlRate + 2f);
				}
			}
		}
	}
}
