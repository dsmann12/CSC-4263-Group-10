using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {
    GameObject player;
    public float bulletCooldownPistol = .5f;
    public float bulletCooldownShotgun = 2.4f;
    float bulletCooldown;
    float cooldownTicker = 0f;
    bool onCooldown = false;
    Ammo ammo;
    AudioSource[] gunSounds;
    public enum Gun { Pistol, Shotgun}
    public Gun currGun;
    public bool hasShotgun = false;
    public float ShotgunSpreadDegrees = 5;
    SaveLoad saveLoad;


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
        Physics2D.IgnoreLayerCollision(9, 10);
        Physics2D.IgnoreLayerCollision(9, 9);

        //Get Audio Sources
        gunSounds = GetComponents<AudioSource>();
        currGun = Gun.Pistol;
        bulletCooldown = bulletCooldownPistol;

        GameObject saveData;
        saveData = GameObject.Find("SaveData");
        if (saveData != null)
        {
            saveLoad = saveData.GetComponentInParent<SaveLoad>();
            hasShotgun = saveLoad.hasShotgun;
            if (saveLoad.currGun != currGun)
                swapGun();
        }
    }


    public void SaveShooting()
    {
        saveLoad.currGun = currGun;
        saveLoad.hasShotgun = hasShotgun;
    }


	void Shoot()
    {
        float angle = this.transform.eulerAngles.z;
        float width = GetComponent<SpriteRenderer>().bounds.size.x;
        float height = GetComponent<SpriteRenderer>().bounds.size.y;
        float shotgunAdjustment = GetComponent<SpriteRenderer>().bounds.size.y / 2;
        float cos = Mathf.Cos(Mathf.Deg2Rad*angle);
        float sin = Mathf.Sin(Mathf.Deg2Rad*angle);
        float adjustmentCos = Mathf.Cos(Mathf.Deg2Rad * (angle - 90));
        float adjustmentSin = Mathf.Sin(Mathf.Deg2Rad * (angle - 90));
        float startPosX;
        float startPosY;
        if(player.transform.localScale.x == -1)
        {
            if(currGun == Gun.Shotgun)
            {
                startPosX = this.transform.position.x - cos * width + adjustmentCos*shotgunAdjustment;
                startPosY = this.transform.position.y - sin * height + adjustmentSin*shotgunAdjustment;
            }
            else
            {
                startPosX = this.transform.position.x - cos * width;
                startPosY = this.transform.position.y - sin * height;
            }
        }
        else
        {
            if(currGun == Gun.Shotgun)
            {
                startPosX = this.transform.position.x + cos * width + adjustmentCos*shotgunAdjustment;
                startPosY = this.transform.position.y + sin * height + adjustmentSin*shotgunAdjustment;
            }
            else
            {
                startPosX = this.transform.position.x + cos * width;
                startPosY = this.transform.position.y + sin * height;
            }
        }
        if(currGun == Gun.Shotgun)
        {
            for (int angleIncrement = -2; angleIncrement < 4; angleIncrement++)
            {
                GameObject bullet = new GameObject();
                bullet.layer = 9;
                bullet.AddComponent<SpriteRenderer>();
                SpriteRenderer bulletSprite = bullet.GetComponent<SpriteRenderer>();
                bulletSprite.sprite = Resources.Load<Sprite>("Bullet");
                bulletSprite.sortingOrder = 5;
                bullet.transform.position = new Vector3(startPosX, startPosY, 1);
                if (player.transform.localScale.x == -1)
                {
                    bullet.transform.eulerAngles = new Vector3(1, 1, angle + 180 + (angleIncrement) * ShotgunSpreadDegrees);
                }
                else
                {
                    bullet.transform.eulerAngles = new Vector3(1, 1, angle + (angleIncrement * ShotgunSpreadDegrees));
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
        }
        else
        {
            GameObject bullet = new GameObject();
            bullet.layer = 9;
            bullet.AddComponent<SpriteRenderer>();
            SpriteRenderer bulletSprite = bullet.GetComponent<SpriteRenderer>();
            bulletSprite.sprite = Resources.Load<Sprite>("Bullet");
            bulletSprite.sortingOrder = 5;
            bullet.transform.position = new Vector3(startPosX, startPosY, 1);
            if (player.transform.localScale.x == -1)
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

        // test bullet as trigger
        bullet.GetComponent<BoxCollider2D>().isTrigger = true;

        // add projectile component
        bullet.AddComponent<Projectile>();

            // add tag to bullet
            bullet.tag = "PlayerBullet";

            // decrement ammo
            ammo.amount -= 1;
        }


        // Play sound
        if (currGun == Gun.Pistol)
        {
            int rand = Random.Range(0, gunSounds.Length-2);
            gunSounds[rand].Play();
        }
        else
        {
            int rand = Random.Range(gunSounds.Length - 2, gunSounds.Length);
            gunSounds[rand].Play();
        }
    }

    void swapGun()
    {
        if (currGun == Gun.Pistol && hasShotgun)
        {
            currGun = Gun.Shotgun;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Shotgun");
            this.transform.localPosition = new Vector3(-.44f, 1.6f);
            bulletCooldown = bulletCooldownShotgun;
        }
        else
        {
            currGun = Gun.Pistol;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pistol");
            this.transform.localPosition = new Vector3(0, 1.43f);
            bulletCooldown = bulletCooldownPistol;
        }
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            swapGun();
        }
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
