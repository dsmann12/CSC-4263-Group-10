using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {
    GameObject player;
    public float bulletCooldown = .5f;
    float cooldownTicker = 0f;
    bool onCooldown = false;
    Ammo ammo;

	// Use this for initialization
	void Start () {
        GameObject[] objs = FindObjectsOfType<GameObject>();
        foreach(GameObject obj in objs)
        {
            if(obj.tag == "Player")
            {
                player = obj;
                break;
            }
        }

        // Get Ammo
        ammo = player.GetComponent<Ammo>();
	}
	void Shoot()
    {
        float angle = this.transform.eulerAngles.z;
        float width = GetComponent<SpriteRenderer>().bounds.size.x;
        float height = GetComponent<SpriteRenderer>().bounds.size.y;
        float cos = Mathf.Cos(Mathf.Deg2Rad*angle);
        float sin = Mathf.Sin(Mathf.Deg2Rad*angle);
        float startPosX;
        float startPosY;
        if(player.transform.localScale.x == -1)
        {
            startPosX = this.transform.position.x - cos * width;
            startPosY = this.transform.position.y - sin * height;
        }
        else
        {
            startPosX = this.transform.position.x + cos * width;
            startPosY = this.transform.position.y + sin * height;
        }
        
        GameObject bullet = new GameObject();
        bullet.AddComponent<SpriteRenderer>();
        SpriteRenderer bulletSprite = bullet.GetComponent<SpriteRenderer>();
        bulletSprite.sprite = Resources.Load<Sprite>("Bullet");
        bulletSprite.sortingOrder = 5;
        bullet.transform.position = new Vector3(startPosX, startPosY, 1);
        if(player.transform.localScale.x == -1)
        {
            bullet.transform.eulerAngles = new Vector3(1, 1, angle + 180);
        }
        else
        {
            bullet.transform.eulerAngles = new Vector3(1, 1, angle);
        }
        bullet.AddComponent<BoxCollider2D>();
        bullet.AddComponent<Rigidbody2D>();
        bullet.GetComponent<Rigidbody2D>().drag = 0f;
        bullet.GetComponent<Rigidbody2D>().gravityScale = 0;
        bullet.AddComponent<BulletMovement>();

        // add projectile component
        bullet.AddComponent<Projectile>();

        // add tag to bullet
        bullet.tag = "PlayerBullet";

        // decrement ammo
        ammo.amount -= 1;
    }
	// Update is called once per frame
	void Update () {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)) && !onCooldown && !ammo.isOutOfAmmo)
        {
            Shoot();
            onCooldown = true;
        }
        else if(onCooldown)
        {
            cooldownTicker += Time.deltaTime;
            if (cooldownTicker > bulletCooldown)
            {
                onCooldown = false;
                cooldownTicker = 0f;
            }
        }
	}
}
