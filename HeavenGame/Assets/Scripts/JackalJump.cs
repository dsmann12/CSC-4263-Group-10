using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackalJump : MonoBehaviour
{
    public float jumpForce = 5f;
    public float maxJumpRange = 15;
    public float minJumpRange = 10;
    public float maxCeilingHeight = 18f;
    public float distanceToTopOfObject = 1.7f; //distance from raycast to top of object

    private Rigidbody2D rb;
    private GameObject player;
    private Enemy enemy;
    private bool isJumping = false;
    private bool isOnCeiling = false;
    private float lastOnCeiling = 1f; // time since last on ceiling
    private float ceilingJumpWait = 3f; // least amount of time in seconds to wait before jackal will decide to jump to ceiling
    private float ceilingJumpChance = 0.1f;
    private float randomNumber = 1.0f;


    // initialize variables on awake
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        InvokeRepeating("ChangeRandomNumber", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

        // check if jackal is jumping
        if (rb.velocity.y == 0)
        {
            isJumping = false;
        }

        // update time since last on ceiling
        lastOnCeiling += Time.deltaTime;

    }

    void FixedUpdate()
    {
        // raycast to detect ceiling
        RaycastHit2D ceilingHit = Physics2D.Raycast(transform.position, Vector2.up);
        Debug.Log("Raycast distance = " + ceilingHit.distance);


        // if not jumping, not on ceiling, if raycast hit distance is <= maxCeilingHeight, and raycast hit a wall, then handle ceiling actions
        if (!isJumping && !isOnCeiling && (ceilingHit.distance <= maxCeilingHeight) && (ceilingHit.collider.tag == "Wall"))
        {
            // if raycast distance is less than topOfObject, then object is on the ceiling
            if (ceilingHit.distance <= distanceToTopOfObject)
            {
                isOnCeiling = true;
                FlipSpriteVertically();
                rb.gravityScale = 0; // for when Jackal starts on ceiling
            }
            // only jump to ceiling if Jackal detects player, lastOnCeiling 
            else if (enemy.DetectedPlayer() && (lastOnCeiling >= ceilingJumpWait) && (randomNumber <= 0.1)) 
            {
                JumpToCeiling();
            }
        }

        // get the floor of the absolute distance to the player
        float playerDist = Mathf.Floor(Mathf.Abs(enemy.CalculateDistanceToPlayer()));

        // if player distance is <= the max range to begin a jump and >= the min range to begin a jump, and object not jumping, then jump
        if ((playerDist <= maxJumpRange && playerDist >= minJumpRange) && !isJumping)
        {
            // if jackal is on ceiling, jump down, else jump up
            Vector2 direction = (isOnCeiling) ? Vector2.down : Vector2.up;
            rb.AddForce(direction * jumpForce);
            isJumping = true;
            rb.gravityScale = 1; // set gravity to one in case jackal was on ceiling
            if (isOnCeiling)
            {
                FlipSpriteVertically();
                isOnCeiling = false;
                lastOnCeiling = 0f;
            }
        }
    }

    void JumpToCeiling()
    {
        rb.AddForce(Vector2.up * 3000f);
        rb.gravityScale = 0;
        isJumping = true;
    }

    void FlipSpriteVertically()
    {
        Vector2 localScale = transform.localScale;
        localScale.y *= -1;
        transform.localScale = localScale;
    }

    void ChangeRandomNumber()
    {
        randomNumber = Random.value;
        Debug.Log("Object: " + GetInstanceID() + " random number = " + randomNumber);
    }
}
