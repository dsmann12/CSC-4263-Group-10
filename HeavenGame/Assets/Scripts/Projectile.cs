using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float damage = 1f;

    public float GetDamage()
    {
        return damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Fuck colliding in projectile");
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("Fuck colliding in projectile");
    //    if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Player")
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
