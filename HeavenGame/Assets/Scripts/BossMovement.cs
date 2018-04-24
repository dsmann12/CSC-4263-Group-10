using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    Vector3 dashTarget;
    Rect movementArea;
    public float xMovementRange = 15;
    public float yMovementRange = 15;
    public float health = 30;
    float movementAngle;
    bool isDashing;
    float dashCooldown = 1.5f;
    float dashCooldownTimer;
    float wobbleAngle;
    float wobbleAngleChangeCooldown = .5f;
    float wobbleAngleChangeTimer;
    public GameObject panel;
    // Use this for initialization
    void Start()
    {
        dashTarget = this.transform.position;
        movementAngle = 0;
        isDashing = false;
        dashCooldownTimer = 0;
        wobbleAngleChangeTimer = 0;
        wobbleAngle = chooseRandomDirection() * Mathf.Deg2Rad;
        Vector2 movementAreaPos = new Vector2(this.transform.position.x - (xMovementRange / 2), this.transform.position.y - (yMovementRange / 2));
        Vector2 movementAreaDimensions = new Vector2(xMovementRange, yMovementRange);
        movementArea = new Rect(movementAreaPos, movementAreaDimensions);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "PlayerBullet")
        {
            Projectile proj = obj.GetComponent<Projectile>();

            if (proj)
            {
                health -= proj.GetDamage();
            }
            Destroy(obj);
            if (health == 0)
            {
                Destroy(this.gameObject);
                panel.SetActive(true);
            }
        }
    }
    void setDashTarget()
    {
        do
        {
            movementAngle = chooseRandomDirection();
            movementAngle = Mathf.Deg2Rad * movementAngle;
            float xComponent = 8 * Mathf.Cos(movementAngle);
            float yComponent = 8 * Mathf.Sin(movementAngle);
            dashTarget = new Vector3(this.transform.position.x + xComponent, this.transform.position.y + yComponent);
            isDashing = true;
        } while (!movementArea.Contains(new Vector2(dashTarget.x, dashTarget.y)));

    }
    void moveTowardsDashTarget()
    {
        float newX = this.transform.position.x + Mathf.Cos(movementAngle) * .2f;
        float newY = this.transform.position.y + Mathf.Sin(movementAngle) * .2f;
        this.transform.position = new Vector3(newX, newY);
    }
    float chooseRandomDirection()
    {
        return Random.Range(0, 360);
    }
    void wobble()
    {
        float xAdjustment = Mathf.Cos(wobbleAngle) * .01f;
        float yAdjustment = Mathf.Sin(wobbleAngle) * .01f;
        this.transform.position = new Vector3(this.transform.position.x + xAdjustment, this.transform.position.y + yAdjustment);
    }
    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            if (Mathf.Abs(this.transform.position.x - dashTarget.x) < .1 && Mathf.Abs(this.transform.position.y - dashTarget.y) < .1)
            {
                isDashing = false;
            }
            else
            {
                moveTowardsDashTarget();
            }
        }
        else
        {
            wobble();
            dashCooldownTimer += Time.deltaTime;
            wobbleAngleChangeTimer += Time.deltaTime;
        }
        if (wobbleAngleChangeTimer > wobbleAngleChangeCooldown)
        {
            wobbleAngle = chooseRandomDirection() * Mathf.Deg2Rad;
            wobbleAngleChangeTimer = 0;
        }
        if (dashCooldownTimer > dashCooldown)
        {
            setDashTarget();
            dashCooldownTimer = 0;
        }

    }
}
