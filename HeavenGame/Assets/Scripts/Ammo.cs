﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {
    public uint limit = 10;
    public uint amount = 10;
    public bool isOutOfAmmo = false;
    GameObject saveData;
    SaveLoad saveLoad;

    // Use this for initialization
    void Start () {
        saveData = GameObject.Find("SaveData");
        if (saveData != null)
        {
            saveLoad = saveData.GetComponent<SaveLoad>();
            amount = saveLoad.ammo;
        }
    }
	
	// Update is called once per frame
	void Update () {
        isOutOfAmmo = (amount <= 0) ? true : false;
	}

    public void SaveAmmo()
    {
        saveLoad.ammo = amount;
    }

    public void AddAmmo(uint a)
    {
        amount += a;
    }

    public void DecreaseAmmo(uint a)
    {
        amount -= a;
    }
}
