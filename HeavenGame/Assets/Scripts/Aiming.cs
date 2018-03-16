using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour {
    Camera cam;
    bool leftFacing;
    Vector3 mousePos;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
        leftFacing = false;
	}
    public bool isLeftFacing()
    {
        return leftFacing;
    }
	void adjustAim()
    {
        Vector3 thisPos = this.transform.position;
        try
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 armToMouseVector = new Vector3(mousePos.x - thisPos.x, mousePos.y - thisPos.y, thisPos.z);
            Vector3 xAxis = new Vector3(1, 0, 0);       
            float angle = Vector3.Angle(xAxis, armToMouseVector.normalized);
            if (angle > 100 && !leftFacing)
            {
                leftFacing = true;
                GetComponentInParent<Turning>().Invoke("turnLeft",0);
            }
            else if (angle<80 && leftFacing)
            {
                leftFacing = false;
                GetComponentInParent<Turning>().Invoke("turnRight", 0);
            }
            if (leftFacing)
            {
                if (armToMouseVector.y > 0)
                {
                    angle = -angle;
                }
                angle += 180;
            }
            else
            {
                if (armToMouseVector.y < 0)
                {
                    angle = -angle;
                }
            }
            this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y, angle);
        }
        catch (System.Exception e) { }

    }
	// Update is called once per frame
	void Update () {
        if(mousePos!=cam.ScreenToWorldPoint(Input.mousePosition))
            adjustAim();
	}
}
