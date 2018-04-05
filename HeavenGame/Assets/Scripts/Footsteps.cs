using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour {
    AudioSource[] footsteps;
	public int number = 5;

	// Use this for initialization
	void Start () {
        footsteps = GetComponents<AudioSource>();
	}
	
    void playFootstep(){
        int rand = Random.Range(0, number - 1);
        footsteps[rand].Play();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
