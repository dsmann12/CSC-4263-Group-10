using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {
    float speed = 10f;
    float despawnDistance = 40f;
    Vector3 startPos;
    Vector3 Velocity;
	// Use this for initialization
	void Start () {
        startPos = this.transform.position;
        float angle = this.transform.eulerAngles.z;
        float xSpeed = Mathf.Cos(Mathf.Deg2Rad * angle)*speed;
        float ySpeed = Mathf.Sin(Mathf.Deg2Rad * angle)*speed;
        Velocity = new Vector3(xSpeed, ySpeed);
        GetComponent<Rigidbody2D>().velocity = Velocity;
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().gravityScale = .5f;
    }
    // Update is called once per frame
    void Update () {
        if(Vector3.Distance(startPos,this.transform.position) > despawnDistance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //GetComponent<Rigidbody2D>().velocity = Velocity;
        }
	}
}
