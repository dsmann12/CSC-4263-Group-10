using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeam : MonoBehaviour
{
    float expansionSpeed = .01f;
    float shrinkSpeed = .01f;
    bool isExpansionFinished = false;
    int direction = 1;
    // Use this for initialization
    void Start()
    {
        direction = findDirection();
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
        if (collision.gameObject.tag == "Player")
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.amount -= 1;
        }
    }
    void expand()
    {
        if (Mathf.Abs(this.transform.localScale.x) > 10)
        {
            isExpansionFinished = true;
        }
        float newY = this.transform.localScale.y;
        if (newY < 1)
        {
            newY = this.transform.localScale.y + .03f;
        }
        float newX = this.transform.localScale.x + (expansionSpeed * direction);
        this.transform.localScale = new Vector3(newX, newY);
        expansionSpeed += .005f;
    }
    void shrink()
    {
        float newY = this.transform.localScale.y;
        if (newY <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            newY = this.transform.localScale.y - shrinkSpeed;
            this.transform.localScale = new Vector3(this.transform.localScale.x, newY);
            shrinkSpeed += .001f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isExpansionFinished)
        {
            expand();
        }
        else
        {
            shrink();
        }
    }
}
