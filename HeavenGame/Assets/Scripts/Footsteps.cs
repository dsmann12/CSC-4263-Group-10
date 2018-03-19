using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour {
    AudioSource[] footsteps;

	// Use this for initialization
	void Start () {
        footsteps = GetComponents<AudioSource>();
	}
	
    void playFootstep(){
        int rand = Random.Range(0, footsteps.Length);
        footsteps[rand].Play();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
