using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMelee : MonoBehaviour {
    private BoxCollider2D hitbox;
    private Melee melee;
    private bool isMelee = false;
    private float hitDelay = 1.0f;
    public float lastHit = 0.0f;

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

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    HandleEnemyCollision(collision);
    //}

    private void HandleEnemyCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && isMelee)
        {
            lastHit = 0.0f;
            Debug.Log("hit enemy");
            Enemy enemy = collision.gameObject.GetComponentInParent<Enemy>();
            if (melee != null && melee.IsMelee())
            {
                Rigidbody2D colrb = collision.gameObject.GetComponentInParent<Rigidbody2D>();
                colrb.velocity = new Vector2(0.0f, colrb.velocity.y);
                colrb.AddForce(GameObject.FindWithTag("Player").transform.localScale.x * Vector2.right * 20.0f, ForceMode2D.Impulse);
                enemy.health -= melee.GetDamage();
            }
        }
    }


}
