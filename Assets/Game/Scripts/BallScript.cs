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

    #endregion
    
    #region Fields

    private Vector2 _prevVelocity;
    private bool _isMoving;
    private bool _changeSpeedFlag;
    
    private static BallScript _shared;
    
    [SerializeField]
    private float _ballSpeed = BallInitSpeed;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _shared = this;
    }

    void Start()
    {
        rb.simulated = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "BottomWall")
        {
            GameManager.DroppedBall = true;
            return;
        }

        if (other.gameObject.tag == "Paddle")
        {
            GameManager.PaddleHitsCounter = GameManager.PaddleHitsCounter + 1;
            if (GameManager.PaddleHitSpeedChange.Contains(GameManager.PaddleHitsCounter))
            {
                _ballSpeed *= GameManager.ballSpeedMult;
                _changeSpeedFlag = true;
            }
        }
        ContactPoint2D contact = other.contacts[0];
        Vector2 contactNormal = contact.normal;
        Vector2 newVelocity = Vector2.Reflect(_prevVelocity, contactNormal);
        if (_changeSpeedFlag)
        {
            newVelocity *= GameManager.ballSpeedMult;
            print("Ball Speed Increased !!");
        }
        rb.velocity = newVelocity;
        _prevVelocity = newVelocity;
        _changeSpeedFlag = false;
    }

    #endregion

    #region Methods
    public static bool IsMoving
    {
        get => _shared._isMoving;
    }

    public float BallSpeed
    {
        get => _ballSpeed;
        set => _ballSpeed = value;
    }
    
    
    public static void StartBall()
    {
        if (!_shared._isMoving)
        {
            _shared._isMoving = true;
            _shared.rb.simulated = true;
            _shared.rb.velocity = new Vector2(_shared._ballSpeed/2f,-_shared._ballSpeed);
            _shared._prevVelocity = _shared.rb.velocity;
        }
    }

    public static void ResetBall()
    {
        _shared._isMoving = false;
        GameManager.DroppedBall = false;
        _shared.rb.simulated = false;
        _shared.gameObject.transform.position = new Vector3(0,-1.5f,0);
        _shared._ballSpeed = BallInitSpeed;
        _shared.rb.velocity = new Vector2(_shared._ballSpeed/2f,_shared._ballSpeed);
        _shared._prevVelocity = _shared.rb.velocity;
    }

    public static void RemoveBall()
    {
        _shared.gameObject.SetActive(false);
    }
    

    #endregion

}
