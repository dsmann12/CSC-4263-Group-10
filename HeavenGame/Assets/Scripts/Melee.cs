using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {
    public float damage = 1f;
    private bool isMelee = false;
    public Collider2D trigger;
    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        //trigger = GetComponentInChildren<BoxCollider2D>();
        trigger.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1) && !isMelee)
        {
            Debug.Log("Right click");
            isMelee = true;
            trigger.enabled = true;
        } 

        if (!isMelee)
        {
            trigger.enabled = false;
        }

        anim.SetBool("IsMelee", isMelee);
	}

    public float GetDamage()
    {
        return damage;
    }

    public bool IsMelee()
    {
        return isMelee;
    }

    private void StopMelee()
    {
        isMelee = false;
    }
}
