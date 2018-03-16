using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject enemy;
    public static int waves = 2;
    public float delay = 3f;
    bool triggered = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !triggered)
        {
            triggered = true;
            StartCoroutine(SpawnWaves(delay));
        }
    }

    IEnumerator SpawnWaves(float delay)
    {
        for (int i = 0; i < waves; i++)
        {
            Instantiate(enemy, spawn1.transform.position, spawn1.transform.rotation);
            Instantiate(enemy, spawn2.transform.position, spawn2.transform.rotation);
            yield return new WaitForSeconds(delay);
        }
    }
}
