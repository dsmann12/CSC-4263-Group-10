using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : MonoBehaviour {
    public enum ResourceType { HEALTH, AMMO, MAGIC, STORY }
    public uint value = 10;
    public ResourceType type;
    public GameObject panel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player hit resource pickup");
            GameObject player = collision.gameObject;
            switch(type)
            {
                case ResourceType.HEALTH:
                    Health health = player.GetComponent<Health>();
                    health.AddHealth(value);
                    break;
                case ResourceType.AMMO:
                    Ammo ammo = player.GetComponent<Ammo>();
                    ammo.AddAmmo(value);
                    break;
                case ResourceType.MAGIC:
                    Magic magic = player.GetComponent<Magic>();
                    magic.AddMagic(value);
                    break;
                case ResourceType.STORY:
                    panel.SetActive(true);
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}
