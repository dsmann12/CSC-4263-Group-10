using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
    public float amount = 2;
    public bool takingDamage = false;
    private Animator anim;
    GameObject saveData;
    SaveLoad saveLoad;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        saveData = GameObject.Find("SaveData");
        if (saveData != null)
        {
            saveLoad = saveData.GetComponent<SaveLoad>();
            amount = saveLoad.health;
        }
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        anim.SetBool("TakingDamage", takingDamage);

        if (IsDead())
        {
            Destroy(saveData);
            Destroy(gameObject);
            SceneManager.LoadScene("Death");
            

        }
    }

    public void SaveHealth()
    {
        saveLoad.health = amount;
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
