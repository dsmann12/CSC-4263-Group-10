using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {
    float speed = .3f;
    Vector3 startPos;
    float travelDistance = 40;
	// Use this for initialization
	void Start () {
        startPos = this.transform.position;
	}
	void move()
    {
        float angle = this.transform.eulerAngles.z;
        float xInc = Mathf.Cos(Mathf.Deg2Rad*angle)*speed;
        float yInc = Mathf.Sin(Mathf.Deg2Rad*angle)*speed;
        this.transform.position = new Vector3(this.transform.position.x + xInc, this.transform.position.y + yInc, this.transform.position.z);
    }
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(startPos, this.transform.position) < travelDistance)
        {
            move();
        }
        else
        {
            Destroy(this.gameObject);
        }

	}
}
