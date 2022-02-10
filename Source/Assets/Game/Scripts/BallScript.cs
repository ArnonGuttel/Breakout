using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    #region Constants

    private const float BallInitSpeed = 3; // Default Ball speed 
    private const float LeftDirection = 0.64f; // Default Ball go left arrow's direction 
    private const float EchoDelayTimer = 0.03f; // timer for Ball Echo effect
    private const int MaxSpeedIncrease = 3;

    #endregion

    #region Inspector

    public Rigidbody2D rb;

    /* GameObject with the echo animation, that we will use when ball at max speed */
    public GameObject ballEcho;

    #endregion

    #region Fields

    [SerializeField] private float ballSpeed = BallInitSpeed;

    private bool _isMoving;

    private Vector2 _prevVelocity;

    /*  An boolean flag that will change once the ball hit the paddle an predefined number of times. */
    private bool _changeSpeedFlag;
    private int _changeSpeedCounter;
    private bool _ballMaxSpeedFlag; 

    /*  Those variables will use us for the Echo effect (Bonus part) */
    private float _echoTimer;
    private bool _createEchoFlag;

    private int _ballStartDir = 1; // if _ballStartDir equal 1, the ball will go right on start, if -1 left.
    
    #endregion

    #region MonoBehaviour

    void Start()
    {
        rb.simulated = false; // set to false, will change when the player hits the "Enter" key.
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("BottomWall")) // check if ball dropped.
        {
            GameManager.DroppedBall = true;
            return;
        }

        // else, then the ball hit the frame , or an tile Object. so we will change the ball direction by changing
        // the Ball Velocity.
        Vector2 newVelocity = GetNewVelocity(other);
        if (other.gameObject.CompareTag("Paddle")) // update GameManger's paddle hit counter, check for new ball speed.
            newVelocity = PaddleHit(newVelocity);
        if (other.gameObject.CompareTag("MiddleTile") && !_ballMaxSpeedFlag) // increase ball speed. 
            newVelocity = MiddleTileHit(newVelocity);
        rb.velocity = newVelocity;
        _prevVelocity = newVelocity;
    }

    private void Update()
    {
        if (_createEchoFlag) // check if need to create the Echo effect.
            CreateEcho();
    }

    #endregion

    #region Methods

    private Vector2 GetNewVelocity(Collision2D other)
    {
        ContactPoint2D contact = other.contacts[0];
        Vector2 contactNormal = contact.normal;
        Vector2 newVelocity = Vector2.Reflect(_prevVelocity, contactNormal);
        return newVelocity;
    }

    private Vector2 MiddleTileHit(Vector2 newVelocity)
        // On collision with one of the middle tiles, We will increase the ball speed,
        // if the ball is already at it max speed, we will do nothing.
    {
        ballSpeed *= GameManager.ballSpeedMult;
        newVelocity *= GameManager.ballSpeedMult;
        print("Ball Speed Increased !!");
        _changeSpeedCounter++;
        if (_changeSpeedCounter == MaxSpeedIncrease)
        {
            _ballMaxSpeedFlag = true;
            print("Maximum Speed!");
            _createEchoFlag = true;
        }
        return newVelocity;
    }
    
    private Vector2 PaddleHit(Vector2 newVelocity)
        // On collision with the paddle, We will Update the update GameManger's paddle hit counter,
        // abd will if there is need to update the ball speed,
        // finally we will check if the ball got to it maximum speed, if sp we will change the createEcho flag.
    {
        GameManager.PaddleHitsCounter = GameManager.PaddleHitsCounter + 1;
        if (GameManager.PaddleHitSpeedChange.Contains(GameManager.PaddleHitsCounter) && !_ballMaxSpeedFlag)
        {
            _changeSpeedFlag = true;
            ballSpeed *= GameManager.ballSpeedMult;
            _changeSpeedCounter += 1;
            if (_changeSpeedCounter == MaxSpeedIncrease)
                _ballMaxSpeedFlag = true;
            if (GameManager.PaddleHitsCounter == GameManager.PaddleHitSpeedChange[2])
                _ballMaxSpeedFlag = true;
        }

        if (_changeSpeedFlag)
        {
            newVelocity *= GameManager.ballSpeedMult;
            print("Ball Speed Increased !!");
        }

        if (_ballMaxSpeedFlag)
        {
            print("Maximum Speed!");
            _createEchoFlag = true;
        }
        _changeSpeedFlag = false;
        return newVelocity;
    }

    private void CreateEcho()
        // We will use this method when the ball got to it maximum speed, we will create the echo effect by instantiate
        // a copy of the ball object, that has a disappear animation, to avoid to much ball objects, we will destroy
        // the object with enough delay for the animation to play.
    {
        if (_echoTimer <= 0)
        {
            GameObject echo = Instantiate(ballEcho, transform.position, Quaternion.identity);
            Destroy(echo, 0.15f);
            _echoTimer = EchoDelayTimer;
        }
        else
        {
            _echoTimer -= Time.deltaTime;
        }
    }

    public void StartBall()
        // We will use this method on the StartBall Event.
        // We will activate the physics on the ball with default speed (velocity).
    {
        if (!_isMoving)
        {
            _isMoving = true;
            rb.simulated = true;
            if (GameManager.ArrowDir >= LeftDirection)
            {
                _ballStartDir = -1;
            }
            rb.velocity = new Vector2(ballSpeed * _ballStartDir/ 2f, -ballSpeed);
            _prevVelocity = rb.velocity;
        }
    }

    public void ResetBall()
        // We will use this method on the BallDropped Event.
        // It will reset the ball values.
    {
        GameManager.DroppedBall = false;
        _isMoving = false;
        rb.simulated = false;
        _createEchoFlag = false;
        _ballMaxSpeedFlag = false;
        _ballStartDir = 1;
        _changeSpeedCounter = 0;
        gameObject.transform.position = new Vector3(0, -1.5f, 0);
        ballSpeed = BallInitSpeed;
    }

    public void RemoveBall()
        // We will use this method on the GameOver Event.
        // It will deActive the ball object.
    {
        gameObject.SetActive(false);
    }

    public void ActivateBall()
        // We will use this method on the StartGame Event.
        // It will Activate the ball object.
    {
        gameObject.SetActive(true);
    }
    #endregion
}