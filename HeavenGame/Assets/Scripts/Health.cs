using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public float amount = 2;
    public bool takingDamage = false;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        anim.SetBool("TakingDamage", takingDamage);

        if (IsDead())
        {
            Destroy(gameObject);
        }
    }

    public void AddHealth(float h)
    {
        amount += h;
    }

    public void DecreaseHelth(float h)
    {
        amount -= h;
    }

    public bool IsDead()
    {
        return (amount <= 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Enemy" && !takingDamage)
        {
            Enemy enemy = obj.GetComponent<Enemy>();
            amount -= enemy.damage;
            takingDamage = true;
        }
    }

    private void HandleEnemyCollision(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Enemy" && !takingDamage)
        {
            Enemy enemy = obj.GetComponent<Enemy>();
            amount -= enemy.damage;
            takingDamage = true;
        }
    }

    private void InvincibilityOff()
    {
        takingDamage = false;
    }
}
