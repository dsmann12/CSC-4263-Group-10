using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {
    public uint limit = 10;
    public uint amount = 10;
    public bool isOutOfAmmo = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        isOutOfAmmo = (amount <= 0) ? true : false;
	}
}
