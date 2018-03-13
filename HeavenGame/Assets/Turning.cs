using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning : MonoBehaviour {
    // Use this for initialization
    void Start () {
	}
    void turnLeft()
    {
        this.transform.localScale = new Vector3(-1, 1, 1);
        GetComponent<Movement>().Invoke("SetDashLeft", 0);
    }
    void turnRight()
    {
        this.transform.localScale = new Vector3(1, 1, 1);
        GetComponent<Movement>().Invoke("SetDashRight", 0);
    }
	// Update is called once per frame
	void Update () {
	}
}
