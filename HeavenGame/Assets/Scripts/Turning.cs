using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning : MonoBehaviour {
    // Use this for initialization
    void Start () {
	}
    void turnLeft()
    {
        if(this.transform.localScale.x > 0) {
            this.transform.localScale = new Vector3(-1 * this.transform.localScale.x, this.transform.localScale.y, 1);
        }
    }
    void turnRight()
    {
        if(this.transform.localScale.x < 0)
        {
            this.transform.localScale = new Vector3(-1 * this.transform.localScale.x, this.transform.localScale.y, 1);
        }
    }
	// Update is called once per frame
	void Update () {
	}
}
