using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour {
    private Text ammoText;
    private Slider magicSlider;
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
            Debug.Log("Name in Children Components is " + text.name);
            if (text.name == "AmmoText")
            {
                ammoText = text;
            } else if (text.name == "HealthText")
            {
                healthText = text;
            }
        }

        magicSlider = GetComponentInChildren<Slider>();
        ammo = player.GetComponent<Ammo>();
        magic = player.GetComponent<Magic>();
        health = player.GetComponent<Health>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ammoText.text = ammo.amount.ToString();
        //magicText.text = magic.amount.ToString();
        magicSlider.value = magic.amount;
        healthText.text = health.amount.ToString();
	}
}
