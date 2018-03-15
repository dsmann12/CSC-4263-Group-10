using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public float health = 1;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (IsDead())
        {
            Destroy(gameObject);
        }
    }

    public void AddHealth(float h)
    {
        health += h;
    }

    public void DecreaseHelth(float h)
    {
        health -= h;
    }

    public bool IsDead()
    {
        return (health <= 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Enemy")
        {
            Enemy enemy = obj.GetComponent<Enemy>();
            health -= enemy.damage;
        }
    }
}
