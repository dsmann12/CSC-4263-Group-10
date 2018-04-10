using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileMove : MonoBehaviour {
    public float speed = 40.0f;
    public float lifetime = 5f;
    public float despawnDistance = 40f;
    private Enemy enemy;
    private Rigidbody2D rb;
    private Vector2 startPos;
    private float spawnTime;

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
    }
	// Use this for initialization
	void Start () {
        enemy = GetComponentInParent<Enemy>();
        startPos = this.transform.position;
        spawnTime = Time.time;
	}

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(startPos, this.transform.position) > despawnDistance || Time.time - spawnTime > lifetime)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        float xspeed = (enemy.facingRight) ? speed : -1 * speed;
        rb.velocity = new Vector2(xspeed, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy bullet collision");
        if (collision.gameObject.tag == "Player")
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if (!health.takingDamage)
            {
                health.DecreaseHelth(enemy.damage); // replace with damage
                health.takingDamage = true;
            }
            Destroy(gameObject);
        }
    }
}
