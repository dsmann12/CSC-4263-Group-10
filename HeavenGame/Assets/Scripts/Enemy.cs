using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public float detectionDistance = 10f;
    public float health = 1;
    public float damage = 1;

    private Rigidbody2D rb;
    private GameObject player;
    private bool detectedPlayer = false;
    private float positionDiff = int.MaxValue;
    private bool facingRight = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // check health
        if (health <= 0)
        {
            Destroy(gameObject);
        }


        // calculate distance to player and check if player is detected
        positionDiff = CalculateDistanceToPlayer();
        if (Mathf.Abs(positionDiff) <= detectionDistance)
        {
            detectedPlayer = true;

            // for when adding animation states
            Animator anim = GetComponent<Animator>();
            if (anim != null)
            {
                //anim.SetBool("detectedPlayer", detectedPlayer);
                anim.enabled = false;
            }
        }

        // flip sprite if necessary
        // based on velocity
        // should i base it on player?
        if (rb.velocity.x < 0 && facingRight)
        {
            FlipSprite();
        }
        else if (rb.velocity.x > 0 && !facingRight)
        {
            FlipSprite();
        }

    }

    void FixedUpdate()
    {
        if (detectedPlayer)
        {
            Vector2 direction = (positionDiff > 0) ? Vector2.right : Vector2.left;

            // move enemy using forces
            // keeps gravity applied
            // easy to implement inertia
            rb.AddForce(direction * speed);
            // cap velocity at speed
            if (rb.velocity.magnitude > speed)
            {
                rb.velocity = rb.velocity.normalized * speed;
            }
        }


    }

    // calculate difference between player position and enemy position
    public float CalculateDistanceToPlayer()
    {
        Vector2 playerPosition = player.transform.position;
        return playerPosition.x - this.transform.position.x;
    }

    // public method to check if detected player
    public bool DetectedPlayer()
    {
        return detectedPlayer;
    }

    // flip sprite by negating scale.x
    private void FlipSprite()
    {
        facingRight = !facingRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    // handle collision enter
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        // store tags in variable?
        if (obj.tag == "Player")
        {
            Debug.Log("Collided with player");
            rb.velocity = Vector2.zero;

            // prevent player from pushing enemy
            // magic number
            rb.mass *= 150;
        } else if (obj.tag == "PlayerBullet")
        {
            // get projectile component
            Projectile proj = obj.GetComponent<Projectile>();

            // error check
            if (proj)
            {
                health -= proj.GetDamage();
            }

            Destroy(obj);
        }
    }

    // change mass back to one when exiting collision with player
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.mass = 1;
        }
    }
}
