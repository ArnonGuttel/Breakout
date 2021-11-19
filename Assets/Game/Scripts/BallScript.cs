using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    #region Constants

    private const float BallInitSpeed = 3;

    #endregion

    #region Inspector

    public Rigidbody2D rb;
    public GameObject ballEcho;

    #endregion

    #region Fields

    [SerializeField] private float ballSpeed = BallInitSpeed;

    private bool _isMoving;
    private bool _changeSpeedFlag;

    private Vector2 _prevVelocity;

    private float _echoDelayTimer = 0.05f;
    private float _echoTimer;
    private bool _createEchoFlag;

    #endregion

    #region MonoBehaviour

    void Start()
    {
        rb.simulated = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("BottomWall"))
        {
            GameManager.DroppedBall = true;
            return;
        }

        Vector2 newVelocity = GetNewVelocity(other);
        if (other.gameObject.CompareTag("Paddle"))
            newVelocity= PaddleHit(newVelocity);

        rb.velocity = newVelocity;
        _prevVelocity = newVelocity;
        _changeSpeedFlag = false;
    }

    private void Update()
    {
        if (_createEchoFlag)
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

    private Vector2 PaddleHit(Vector2 newVelocity)
    {
        GameManager.PaddleHitsCounter = GameManager.PaddleHitsCounter + 1;
        if (GameManager.PaddleHitSpeedChange.Contains(GameManager.PaddleHitsCounter))
        {
            _changeSpeedFlag = true;
            ballSpeed *= GameManager.ballSpeedMult;
            if (GameManager.PaddleHitsCounter == GameManager.PaddleHitSpeedChange[2])
            {
                print("Maximum Speed!");
                _createEchoFlag = true;
            }
        }

        if (_changeSpeedFlag)
        {
            newVelocity *= GameManager.ballSpeedMult;
            print("Ball Speed Increased !!");
        }

        return newVelocity;
    }

    private void CreateEcho()
    {
        if (_echoTimer <= 0)
        {
            GameObject echo = Instantiate(ballEcho, transform.position, Quaternion.identity);

            Destroy(echo, 0.25f);
            _echoTimer = _echoDelayTimer;
        }
        else
        {
            _echoTimer -= Time.deltaTime;
        }
    }

    public void StartBall()
    {
        if (!_isMoving)
        {
            _isMoving = true;
            rb.simulated = true;
            rb.velocity = new Vector2(ballSpeed / 2f, -ballSpeed);
            _prevVelocity = rb.velocity;
        }
    }

    public void ResetBall()
    {
        GameManager.DroppedBall = false;
        _isMoving = false;
        rb.simulated = false;
        _createEchoFlag = false;
        gameObject.transform.position = new Vector3(0, -1.5f, 0);
        ballSpeed = BallInitSpeed;
        rb.velocity = new Vector2(ballSpeed / 2f, ballSpeed);
        _prevVelocity = rb.velocity;
    }

    public void RemoveBall()
    {
        gameObject.SetActive(false);
    }

    #endregion
}