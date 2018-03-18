using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour {
    private Text ammoText;
    private Text magicText;
    private Text healthText;
    private Ammo ammo;
    private Magic magic;
    private Health health;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        foreach(Text text in GetComponentsInChildren<Text>())
        {
            if (text.name == "AmmoText")
            {
                ammoText = text;
            } else if (text.name == "MagicText")
            {
                magicText = text;
            } else if (text.name == "HealthText")
            {
                healthText = text;
            }
        }
        ammo = player.GetComponent<Ammo>();
        magic = player.GetComponent<Magic>();
        health = player.GetComponent<Health>();
        Debug.Log("Update UI");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ammoText.text = "Ammo: " + ammo.amount;
        magicText.text = "Magic: " + magic.amount;
        healthText.text = "Health: " + health.amount;
	}
}
