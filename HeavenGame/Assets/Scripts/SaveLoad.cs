using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour {
    public float health;
    public int ammo;
    public int magic;
    public bool hasShotgun;
    public Shooting.Gun currGun;
    public string lastScene;
    public string currScene;


    // Use this for initialization
    void Start () {
        GameObject[] data = GameObject.FindGameObjectsWithTag("SaveData");
        if (data.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            GetComponent<AudioSource>().Play();

            GameObject wanderer = GameObject.Find("Wanderer");
            health = wanderer.GetComponent<Health>().amount;
            ammo = wanderer.GetComponent<Ammo>().amount;
            magic = wanderer.GetComponent<Magic>().amount;
            Shooting shooting = wanderer.transform.GetChild(2).GetComponent<Shooting>();
            hasShotgun = shooting.hasShotgun;
            currGun = shooting.currGun;
            lastScene = "";
            currScene = SceneManager.GetActiveScene().name;
        }
    }
}
