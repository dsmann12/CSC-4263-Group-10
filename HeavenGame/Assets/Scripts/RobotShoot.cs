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
    private AudioSource[] audioSources;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    // Use this for initialization
    void Start () {
        audioSources = GetComponents<AudioSource>();
	}

    // Update is called once per frame
    void Update() {
        // positionDiff is positive or negative
        // if detectedPlayer face direction of player
        // if position > 0 && !facingRight, flip
        // else if position < 0 && facingRight, flip

        if (enemy.DetectedPlayer())
        {
            float positionDiff = enemy.CalculateDistanceToPlayer();
            if((positionDiff > 0 && !enemy.facingRight) || (positionDiff < 0 && enemy.facingRight))
            {
                enemy.FlipSprite();
            }

            if (!isFiring)
            {
                isFiring = true;
                StartCoroutine(Shoot());
                audioSources[2].Play();
            }
            else if (!enemy.DetectedPlayer())
            {
                StopCoroutine(Shoot());
                isFiring = false;
                audioSources[2].Stop();
            }
        }
	}

    void Fire()
    {
        GameObject obj = Instantiate(projectile, projectileSpawn.transform.position, projectile.transform.rotation) as GameObject;
        obj.transform.parent = projectileSpawn.transform;
        audioSources[0].Play();
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
