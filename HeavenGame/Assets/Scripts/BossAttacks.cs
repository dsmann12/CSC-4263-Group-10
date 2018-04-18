using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{

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
    }
    void spawnBeam()
    {
        GameObject beam = new GameObject();
        beam.AddComponent<SpriteRenderer>();
        beam.transform.localScale = new Vector3(.1f, .1f);
        SpriteRenderer renderer = beam.GetComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>("Beam");

        beam.AddComponent<BoxCollider2D>();
        beam.GetComponent<BoxCollider2D>().isTrigger = true;
        beam.transform.position = this.transform.position;
        beam.AddComponent<BossBeam>();

    }
    void spawnBomb()
    {
        GameObject bomb = new GameObject();
        bomb.transform.localScale = new Vector3(.1f, .1f);
        bomb.AddComponent<SpriteRenderer>();
        SpriteRenderer renderer = bomb.GetComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>("Orb");

        bomb.AddComponent<CircleCollider2D>();
        bomb.transform.position = this.transform.position;
        bomb.AddComponent<BossBomb>();
    }
    void spawnWall()
    {
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
