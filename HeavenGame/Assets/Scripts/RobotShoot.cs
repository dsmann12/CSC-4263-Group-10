using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotShoot : MonoBehaviour {
    public Transform projectileSpawn;
    public GameObject projectile;
    public float delay;
    public float fireRate;
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
            InvokeRepeating("Fire", delay, fireRate);
        } else if (!enemy.DetectedPlayer())
        {
            CancelInvoke();
            isFiring = false;
        }
	}

    void Fire()
    {
        isFiring = true;
        GameObject obj = Instantiate(projectile, projectileSpawn.transform.position, projectile.transform.rotation) as GameObject;
        obj.transform.parent = projectileSpawn.transform;

    }
}
