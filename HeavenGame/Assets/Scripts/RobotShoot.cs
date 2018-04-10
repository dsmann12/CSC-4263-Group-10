using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotShoot : MonoBehaviour {
    public Transform projectileSpawn;
    public GameObject projectile;
    public float delay;
    public float fireRate;
    public float reloadTime;
    public uint fireLimit;
    private bool isFiring = false;
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void Update() {
        if (enemy.DetectedPlayer() && !isFiring)
        {
            isFiring = true;
            StartCoroutine(Shoot());
        } else if (!enemy.DetectedPlayer())
        {
            StopCoroutine(Shoot());
            isFiring = false;
        }
	}

    void Fire()
    {
        GameObject obj = Instantiate(projectile, projectileSpawn.transform.position, projectile.transform.rotation) as GameObject;
        obj.transform.parent = projectileSpawn.transform;
    }

    // Coroutine
    // wait for seconds to start (gun loading delay)
    // shoot and wait for seconds after every shot. Waitforseconds(fireRate)
    // 16 shots
    // wait for seconds reload
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(delay);
        while(true)
        {
            for(uint i = 0; i < fireLimit; ++i)
            {
                Fire();
                yield return new WaitForSeconds(fireRate);
            }

            yield return new WaitForSeconds(reloadTime);
        }
        
    }
}
