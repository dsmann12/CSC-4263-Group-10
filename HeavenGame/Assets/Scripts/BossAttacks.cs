using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    AudioSource[] sources;

    public float beamCooldown = 15;
    float beamCooldownCounter;
    public float bombCooldown = 5;
    float bombCooldownCounter;
    public float wallCooldown = 10;
    float wallCooldownCounter;
    // Use this for initialization
    void Start()
    {
        beamCooldownCounter = 0;
        bombCooldownCounter = 0;
        wallCooldownCounter = 0;
        sources = GetComponents<AudioSource>();
    }
    void spawnBeam()
    {
        GameObject beam = Instantiate(Resources.Load("BossBeam")) as GameObject;

        beam.transform.position = this.transform.position;
        sources[1].Play();
        /*
        GameObject beam = new GameObject();
        beam.AddComponent<SpriteRenderer>();
        beam.transform.localScale = new Vector3(.1f, .1f);
        SpriteRenderer renderer = beam.GetComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>("Beam");

        beam.AddComponent<BoxCollider2D>();
        beam.GetComponent<BoxCollider2D>().isTrigger = true;
        beam.transform.position = this.transform.position;
        beam.AddComponent<BossBeam>();
        */

    }
    void spawnBomb()
    {

        GameObject bomb = Instantiate(Resources.Load("BossOrb")) as GameObject;

        bomb.transform.position = this.transform.position;
        /*
        GameObject bomb = new GameObject();
        bomb.transform.localScale = new Vector3(.1f, .1f);
        bomb.AddComponent<SpriteRenderer>();
        SpriteRenderer renderer = bomb.GetComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>("Orb");

        bomb.AddComponent<CircleCollider2D>();
        bomb.transform.position = this.transform.position;
        bomb.AddComponent<BossBomb>();
        bomb.layer = 11;
        */
    }
    void spawnWall()
    {
        GameObject wall = Instantiate(Resources.Load("BossWall")) as GameObject;

        wall.transform.position = this.transform.position;
        /*
        GameObject wall = new GameObject();
        wall.transform.localScale = new Vector3(.1f, .1f);
        wall.AddComponent<SpriteRenderer>();
        SpriteRenderer renderer = wall.GetComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>("Wall");
        wall.AddComponent<BoxCollider2D>();
        BoxCollider2D collider = wall.GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
        wall.transform.position = this.transform.position;
        wall.AddComponent<BossWall>();
        AudioSource audio = wall.AddComponent<AudioSource>();
        audio.clip = Resources.Load("WallSmoke") as AudioClip;
        audio.Play();
        */
    }
    void updateCoooldownCounters()
    {
        bombCooldownCounter += Time.deltaTime;
        beamCooldownCounter += Time.deltaTime;
        wallCooldownCounter += Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        updateCoooldownCounters();
        if (bombCooldownCounter > bombCooldown)
        {
            spawnBomb();
            bombCooldownCounter = 0;
        }
        if (wallCooldownCounter > wallCooldown)
        {
            spawnWall();
            wallCooldownCounter = 0;
        }
        if (beamCooldownCounter > beamCooldown)
        {
            spawnBeam();
            beamCooldownCounter = 0;
        }
    }
}
