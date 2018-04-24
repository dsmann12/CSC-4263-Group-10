using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour {
    public string level;
    GameObject saveData;
    SaveLoad saveLoad;
    GameObject wanderer;
    Health health;
    Ammo ammo;
    Magic magic;
    Shooting shooting;

	// Use this for initialization
	void Start () {
        wanderer = GameObject.Find("Wanderer");
        health = wanderer.GetComponent<Health>();
        ammo = wanderer.GetComponent<Ammo>();
        magic = wanderer.GetComponent<Magic>();
        shooting = wanderer.transform.GetChild(2).GetComponent<Shooting>();
        saveData = GameObject.Find("SaveData");
        if(saveData != null)
            saveLoad = saveData.GetComponent<SaveLoad>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (saveData != null)
            {
                health.SaveHealth();
                ammo.SaveAmmo();
                magic.SaveMagic();
                shooting.SaveShooting();
                saveLoad.lastScene = saveLoad.currScene;
                saveLoad.currScene = level;
            }
            Physics2D.IgnoreLayerCollision(10, 11, false);
            SceneManager.LoadScene(level);
        }
    }
}
