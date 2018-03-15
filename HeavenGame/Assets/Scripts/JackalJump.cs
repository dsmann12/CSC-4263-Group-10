using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackalJump : MonoBehaviour
{
    public float jumpForce = 5f;
    public float maxJumpRange = 15;
    public float minJumpRange = 10;

    private Rigidbody2D rb;
    private GameObject player;
    private Enemy enemy;
    private bool isJumping = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        enemy = GetComponent<Enemy>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y == 0)
        {
            isJumping = false;
        }
    }

    void FixedUpdate()
    {
        float playerDist = Mathf.Floor(Mathf.Abs(enemy.CalculateDistanceToPlayer()));
        Debug.Log("Player distance is " + playerDist);
        if ((playerDist <= maxJumpRange && playerDist >= minJumpRange) && !isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce);
            isJumping = true;
        }
    }
}
