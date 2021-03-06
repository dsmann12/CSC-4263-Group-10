﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWall : MonoBehaviour
{
    float expansionSpeed = .01f;
    float xMovement = 15;
    float movementSpeed = .01f;
    float lifetime = 10;
    float shrinkSpeed = .01f;
    bool isExpansionFinished = false;
    bool isMovementFinished = false;
    int direction = 1;


    // Get collider
    BoxCollider2D col;

    // Use this for initialization
    void Start()
    {
        direction = findDirection();

        // Get box collider and disable it until exanded
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
    }

    int findDirection()
    {
        GameObject player = null;
        GameObject[] objs = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objs)
        {
            if (obj.tag == "Player")
            {
                player = obj;
            }
        }
        if (player.transform.position.x < this.transform.position.x)
        {
            return 1;
        }
        else
        { 
            return -1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Rigidbody2D rgdBody = collision.gameObject.GetComponent<Rigidbody2D>();
            rgdBody.drag = 1.5f;
            rgdBody.gravityScale = 5;
        } else if (collision.gameObject.tag == "Player")
        {
            GameObject player = collision.gameObject;
            Health playerHealth = player.GetComponent<Health>();
            playerHealth.DecreaseHelth(1.0f);
        }

    }
    void expand()
    {
        float newX = this.transform.localScale.x;
        float newY = this.transform.localScale.y;
        bool isXDone = false;
        bool isYDone = false;
        if (newX < 1.5)
        {
            newX += expansionSpeed;
        }
        else
        {
            isXDone = true;
        }
        if (newY < 2.5)
        {
            newY += expansionSpeed;
        }
        else
        {
            isYDone = true;
        }
        this.transform.localScale = new Vector3(newX, newY);
        expansionSpeed += .005f;
        if (isYDone && isXDone)
        {
            isExpansionFinished = true;
            col.enabled = true;
        }
    }
    void move()
    {
        if (xMovement > 0)
        {
            this.transform.position = new Vector3(this.transform.position.x - (movementSpeed * direction), this.transform.position.y);
        }
        else
        {
            isMovementFinished = true;
        }
        xMovement -= movementSpeed;
        movementSpeed += .03f;
    }
    void shrink()
    {
        float newX = this.transform.localScale.x;
        if (newX <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            newX -= shrinkSpeed;
            this.transform.localScale = new Vector3(newX, this.transform.localScale.y);
            shrinkSpeed += .01f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isExpansionFinished)
            expand();
        if (!isMovementFinished)
            move();
        if (lifetime < 0)
        {
            shrink();
        }
        else
        {
            lifetime -= Time.deltaTime;
        }
    }
}
