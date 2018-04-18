using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBomb : MonoBehaviour
{
    Vector3 movementVector;
    float xSpeed;
    float ySpeed;
    float lifetime = 10;
    // Use this for initialization
    void Start()
    {
        movementVector = getMovementVector();
        float angle = Vector3.Angle(new Vector3(1, 0), movementVector);
        this.transform.localEulerAngles = (new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, angle));
        xSpeed = -movementVector.x / 100;
        ySpeed = -movementVector.y / 100;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.amount -= 1;
            Destroy(this.gameObject);
        }
    }
    Vector3 getMovementVector()
    {
        GameObject player = findPlayer();
        Vector3 playerPos = player.transform.position;
        Vector3 movementVec = new Vector3(this.transform.position.x - playerPos.x, this.transform.position.y - playerPos.y);
        return movementVec;

    }
    GameObject findPlayer()
    {
        GameObject[] objs = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objs)
        {
            if (obj.tag == "Player")
            {
                return obj;
            }
        }
        return null;
    }
    void moveTowardsTarget()
    {
        float newX = this.transform.position.x + xSpeed;
        float newY = this.transform.position.y + ySpeed;
        this.transform.position = new Vector3(newX, newY);
    }
    // Update is called once per frame
    void Update()
    {
        if (lifetime > 0)
        {
            moveTowardsTarget();
            lifetime -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
