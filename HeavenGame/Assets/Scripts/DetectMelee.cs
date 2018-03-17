using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMelee : MonoBehaviour {
    private BoxCollider2D hitbox;
    private Melee melee;
    private bool isMelee = false;

	// Use this for initialization
	void Start () {
        hitbox = GetComponent<BoxCollider2D>();
        melee = GetComponentInParent<Melee>();
	}
	
	// Update is called once per frame
	void Update () {
        isMelee = melee.IsMelee();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleEnemyCollision(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        HandleEnemyCollision(collision);
    }

    private void HandleEnemyCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && isMelee)
        {
            Debug.Log("hit enemy");
            Enemy enemy = collision.gameObject.GetComponentInParent<Enemy>();
            if (melee != null && melee.IsMelee())
            {
                enemy.health -= melee.GetDamage();
            }
        }
    }


}
