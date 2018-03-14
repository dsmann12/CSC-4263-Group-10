using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {
    bool leftFacing;
    GameObject player;
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
        bullet.AddComponent<BulletMovement>();
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
        }
	}
}
