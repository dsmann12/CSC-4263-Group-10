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
    Vector2 WalkSpeed = new Vector2(5, 0);
    Vector2 RunSpeed = new Vector2(10, 0);
    Direction dashDirection = Direction.Right;
    int walkAcceleration = 10;
    int runAcceleration = 20;
    bool isJumping;
	// Use this for initialization
	void Start () {
        playerRigidBody = this.GetComponent<Rigidbody2D>();
        currState = MovementState.Standing;
        playerRigidBody.AddForce(new Vector2(0, 0));
        rigidBodyForce = playerRigidBody.GetComponent<ConstantForce2D>();
        isJumping = false;
	}
    void SetDashRight()
    {
        dashDirection = Direction.Right;
    }
    void SetDashLeft()
    {
        dashDirection = Direction.Left;
    }
    void Walking(int dir)
    {
        if (dir == 1)
        {
            if (playerRigidBody.velocity.x < WalkSpeed.x * dir && rigidBodyForce.force.x != dir * walkAcceleration)
            {
                rigidBodyForce.force = new Vector2(dir * walkAcceleration, 0);
            }
            else if (playerRigidBody.velocity.x >= WalkSpeed.x * dir)
            {
                playerRigidBody.velocity = new Vector2(WalkSpeed.x * dir, playerRigidBody.velocity.y);
            }
        }
        else
        {
            if (playerRigidBody.velocity.x > WalkSpeed.x * dir && rigidBodyForce.force.x != dir * walkAcceleration)
            {
                rigidBodyForce.force = new Vector2(dir * walkAcceleration, 0);
            }
            else if (playerRigidBody.velocity.x <= WalkSpeed.x * dir)
            {
                playerRigidBody.velocity = new Vector2(WalkSpeed.x * dir, playerRigidBody.velocity.y);
            }
        }


    }
    void Running(int dir)
    {
        if (dir == 1)
        {
            if (playerRigidBody.velocity.x < RunSpeed.x * dir && rigidBodyForce.force.x != dir*runAcceleration)
            {
                rigidBodyForce.force = new Vector2(dir * runAcceleration, 0);
            }
            else if (playerRigidBody.velocity.x >= RunSpeed.x * dir)
            {
                playerRigidBody.velocity = new Vector2(RunSpeed.x*dir, playerRigidBody.velocity.y);
            }
        }
        else
        {
            if (playerRigidBody.velocity.x > RunSpeed.x * dir && rigidBodyForce.force.x != dir*runAcceleration)
            {
                rigidBodyForce.force = new Vector2(dir * runAcceleration, 0);
            }
            else if (playerRigidBody.velocity.x <= RunSpeed.x * dir)
            {
                playerRigidBody.velocity = new Vector2(RunSpeed.x*dir, playerRigidBody.velocity.y);
            }
        }


    }
    void Standing()
    {
        if (rigidBodyForce.force.x != 0)
        {
            rigidBodyForce.force = new Vector2(0, 0);
        }
    }
    void Jump()
    {
        if (!isJumping)
        {
            playerRigidBody.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            isJumping = true;
        }

    }
    void Dash()
    {
        if (dashDirection == Direction.Right)
        {
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
            rigidBodyForce.force = new Vector2(0, 0);
            playerRigidBody.AddForce(new Vector2(20, 0), ForceMode2D.Impulse);
        }
        else
        {
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
            rigidBodyForce.force = new Vector2(0, 0);
            playerRigidBody.AddForce(new Vector2(-20, 0), ForceMode2D.Impulse);
        }
        
    }
    void updateState()
    {
        
        if(Mathf.Abs(playerRigidBody.velocity.y) <= .01 && isJumping)
        {
            isJumping = false;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftShift) && currState != MovementState.WalkingRight)
        {
            lastState = currState;
            currState = MovementState.RunningRight;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift) && currState != MovementState.RunningRight)
        {
            lastState = currState;
            currState = MovementState.WalkingRight;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.LeftShift) && currState != MovementState.WalkingLeft)
        {
            lastState = currState;
            currState = MovementState.RunningLeft;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftShift) && currState != MovementState.RunningLeft)
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
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Dash();  
        }
        updateState();
        //-1 is for left, 1 is for right. It's multiplied with the acceleration magnitude to give proper direction.

        switch (currState)
        {
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
	}
}
