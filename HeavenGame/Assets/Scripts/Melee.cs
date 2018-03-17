using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {
    public float damage = 1f;
    private bool isMelee = false;
    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right click");
            isMelee = true;
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
