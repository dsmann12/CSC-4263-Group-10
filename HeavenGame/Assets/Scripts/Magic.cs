using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour {
    public uint amount = 100;
    public uint limit = 100;
    public bool isOutOfMagic = false;
    public uint rechargeRate = 1;

	// Use this for initialization
	void Start () {
        StartCoroutine(Recharge());
	}
	
	// Update is called once per frame
	void Update () {
        isOutOfMagic = (amount <= 0) ? true : false;
	}

    IEnumerator Recharge()
    {
        while (true)
        {
            if (amount < limit)
            {
                amount += rechargeRate;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void AddMagic(uint m)
    {
        amount += m;
    }

    public void DecreaseMagic(uint m)
    {
        amount -= m;
    }
}
