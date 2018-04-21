using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour {
    public float health;
    public uint ammo;
    public uint magic;


	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);

        GameObject wanderer = GameObject.Find("Wanderer");
        health = wanderer.GetComponent<Health>().amount;
        ammo = wanderer.GetComponent<Ammo>().amount;
        magic = wanderer.GetComponent<Magic>().amount;
    }
}
