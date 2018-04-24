using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    Rigidbody2D playerRigidBody;
    ConstantForce2D rigidBodyForce;
    enum MovementState { Standing, WalkingRight, WalkingLeft, RunningRight, RunningLeft, Jumping, Falling, Dash}
    MovementState currState;
    MovementState lastState;
    enum Direction { Left,Right};
    public int walkSpeed = 5;
    public int runSpeed = 10;
    Vector2 WalkVector;
    Vector2 RunVector;
    Vector2 LastSpeed;
    public float dashTime = .5f;
    float currDashTime = 0f;
    Direction dashDirection = Direction.Right;
    public int walkAcceleration = 10;
    public int runAcceleration = 20;
    bool isJumping;
    bool isDoubleJumping;
    public float jumpForce = 15f;
    AudioSource[] audioSources;
    int smokeDash;
    SpriteRenderer[] sprites;
    public float fallGravMult = 2f;
    float gravity;
    Magic magic;
    SaveLoad saveLoad;

    // animator
    private Animator anim;

	// Use this for initialization
	void Start () {
        GameObject saveData = GameObject.Find("SaveData");
        GameObject spawn = GameObject.Find("SpawnPoints");
        if (saveData != null)
        {
            saveLoad = saveData.GetComponent<SaveLoad>();
            if(saveLoad.currScene == "Scene_04")
            {
                int i = 0;
                if (saveLoad.lastScene == "Scene_Shotgun")
                    i = 1;
                else if (saveLoad.lastScene == "Scene_05")
                    i = 2;
                transform.position = new Vector3(spawn.transform.GetChild(i).position.x, spawn.transform.GetChild(i).position.y, spawn.transform.GetChild(i).position.z);
            }
        }

        playerRigidBody = this.GetComponent<Rigidbody2D>();
        currState = MovementState.Standing;
        playerRigidBody.AddForce(new Vector2(0, 0));
        rigidBodyForce = playerRigidBody.GetComponent<ConstantForce2D>();
        isJumping = false;
        WalkVector = new Vector2(walkSpeed, 0);
        RunVector = new Vector2(runSpeed, 0);
        audioSources = this.GetComponents<AudioSource>();
        smokeDash = audioSources.Length - 1;
        sprites = GetComponentsInChildren<SpriteRenderer>();
        gravity = playerRigidBody.gravityScale;
        magic = GetComponent<Magic>();

        

        // set animator
        anim = GetComponent<Animator>();
	}


    void HideSprite()
    {
        //anim.enabled = false;
        //for (int i = 0; i < sprites.Length; i++)
          //  sprites[i].enabled = false;
        anim.SetBool("SmokeDash", true);
    }


    void ShowSprite()
    {
        //anim.enabled = true;
        //for (int i = 0; i < sprites.Length; i++)
        //    sprites[i].enabled = true;
        anim.SetBool("SmokeDash", false);
    }


    void UpdateDashDirection()
    {
        Aiming aim = GetComponentInChildren<Aiming>();
        if (aim.isLeftFacing() && currState!=MovementState.Dash)
        {
            dashDirection = Direction.Left;
        }
        else if (currState != MovementState.Dash)
        {
            dashDirection = Direction.Right;
        }
    }


    void Walking(int dir)
    {
        if (dir == 1)
        {
            if (playerRigidBody.velocity.x < WalkVector.x * dir && rigidBodyForce.force.x != dir * walkAcceleration)
            {
                rigidBodyForce.force = new Vector2(dir * walkAcceleration, 0);
            }
            else if (playerRigidBody.velocity.x >= WalkVector.x * dir)
            {
                playerRigidBody.velocity = new Vector2(WalkVector.x * dir, playerRigidBody.velocity.y);
            }
        }
        else
        {
            if (playerRigidBody.velocity.x > WalkVector.x * dir && rigidBodyForce.force.x != dir * walkAcceleration)
            {
                rigidBodyForce.force = new Vector2(dir * walkAcceleration, 0);
            }
            else if (playerRigidBody.velocity.x <= WalkVector.x * dir)
            {
                playerRigidBody.velocity = new Vector2(WalkVector.x * dir, playerRigidBody.velocity.y);
            }
        }


    }
    void Running(int dir)
    {
        if (dir == 1)
        {
            if (playerRigidBody.velocity.x < RunVector.x * dir && rigidBodyForce.force.x != dir*runAcceleration)
            {
                rigidBodyForce.force = new Vector2(dir * runAcceleration, 0);
            }
            else if (playerRigidBody.velocity.x >= RunVector.x * dir)
            {
                playerRigidBody.velocity = new Vector2(RunVector.x*dir, playerRigidBody.velocity.y);
            }
        }
        else
        {
            if (playerRigidBody.velocity.x > RunVector.x * dir && rigidBodyForce.force.x != dir*runAcceleration)
            {
                rigidBodyForce.force = new Vector2(dir * runAcceleration, 0);
            }
            else if (playerRigidBody.velocity.x <= RunVector.x * dir)
            {
                playerRigidBody.velocity = new Vector2(RunVector.x*dir, playerRigidBody.velocity.y);
            }
        }


    }
    void Standing()
    {
        if (playerRigidBody.velocity.x !=0)
        {
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
            rigidBodyForce.force = new Vector2(0, rigidBodyForce.force.y);
            
        }
    }
    void Jump()
    {
        if (!isDoubleJumping)
        {
            playerRigidBody.gravityScale = gravity;
            if (magic.amount < 10)
            {
                playerRigidBody.AddForce(Vector2.up * jumpForce / 2, ForceMode2D.Impulse);
                magic.amount = 0;
            }
            else
            {
                playerRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                magic.amount -= 10;
            }
            if (!isJumping)
            {
                isJumping = true;
            }
            else if (!isDoubleJumping)
            {
                isDoubleJumping = true;
            }
        }

    }
    void Dashing()
    {
        anim.SetBool("SmokeDash", true);
        if (dashDirection == Direction.Right)
        {
            if(playerRigidBody.velocity.x != RunVector.x * 3)
            {
                playerRigidBody.velocity = new Vector2(RunVector.x * 3, playerRigidBody.velocity.y);
                if (rigidBodyForce.force.x != 0)
                {
                    rigidBodyForce.force = new Vector2(0, rigidBodyForce.force.y);
                }
            }
        }
        else
        {
            if (playerRigidBody.velocity.x != RunVector.x * -3)
            {
                playerRigidBody.velocity = new Vector2(RunVector.x * -3, playerRigidBody.velocity.y);
                if (rigidBodyForce.force.x != 0)
                {
                    rigidBodyForce.force = new Vector2(0, rigidBodyForce.force.y);
                }
            }
        }
        if (currDashTime > dashTime)
        {
            currState = lastState;
            playerRigidBody.velocity = new Vector2(LastSpeed.x, 0);
            currDashTime = 0f;
            Physics2D.IgnoreLayerCollision(10, 11, false);
            ShowSprite();
        }
        currDashTime += Time.deltaTime;
        
    }
    void updateState()
    {
        UpdateDashDirection();
        if(Mathf.Abs(playerRigidBody.velocity.y) <= .01 && isJumping)
        {
            isJumping = false;
            isDoubleJumping = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && currState!=MovementState.Dash && magic.amount >= 30)
        {
            lastState = currState;
            LastSpeed = playerRigidBody.velocity;
            currState = MovementState.Dash;
            HideSprite();
            audioSources[smokeDash].Play();
            Physics2D.IgnoreLayerCollision(10, 11, true);
            magic.amount -= 30;
        }
        else if(currState!=MovementState.Dash)
        {
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !Input.GetKey(KeyCode.LeftShift) && currState != MovementState.WalkingRight)
            {
                lastState = currState;
                currState = MovementState.RunningRight;
            }
            else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.LeftShift) && currState != MovementState.RunningRight)
            {
                lastState = currState;
                currState = MovementState.WalkingRight;
            }
            else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !Input.GetKey(KeyCode.LeftShift) && currState != MovementState.WalkingLeft)
            {
                lastState = currState;
                currState = MovementState.RunningLeft;
            }
            else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && Input.GetKey(KeyCode.LeftShift) && currState != MovementState.RunningLeft)
            {
                lastState = currState;
                currState = MovementState.WalkingLeft;
            }
            else if (currState != MovementState.Standing)
            {
                lastState = currState;
                currState = MovementState.Standing;
            }
        }
        
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        updateState();
        //-1 is for left, 1 is for right. It's multiplied with the acceleration magnitude to give proper direction.
        switch (currState)
        {
            case MovementState.Dash:
                Dashing();
                break;
            case MovementState.WalkingLeft:
                Walking(-1);
                break;
            case MovementState.WalkingRight:
                Walking(1);
                break;
            case MovementState.Standing:
                Standing();
                break;
            case MovementState.RunningLeft:
                Running(-1);
                break;
            case MovementState.RunningRight:
                Running(1);
                break;
        }
        
        if (playerRigidBody.velocity.y < -1)
            playerRigidBody.gravityScale = gravity * fallGravMult;
        else
            playerRigidBody.gravityScale = gravity;

        float speedX = Mathf.Abs(playerRigidBody.velocity.x);

        anim.SetFloat("SpeedX", speedX);
        anim.SetBool("IsJumping", isJumping);
      
	}
}
