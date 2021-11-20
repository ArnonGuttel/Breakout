using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PaddleScript : MonoBehaviour
{
    #region Inspector

    public Rigidbody2D rb;
    public GameObject paddleSprite;
    private Animator _paddleAnimator;
    
    #endregion
    
    #region Fields

    private float _curDirection;
    public float paddleSpeed = 5;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _paddleAnimator = paddleSprite.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            _curDirection = -1;
        else if (Input.GetKey(KeyCode.RightArrow))
            _curDirection = 1;
        else
            _curDirection = 0;

    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(_curDirection * paddleSpeed,0));
    }

    private void OnCollisionEnter2D(Collision2D other)
        // If the ball hit the Paddle, we will activate the paddle's jump animation.
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            _paddleAnimator.SetTrigger("PaddleHit");
        }
           
    }
    
    #endregion

    #region Methods

    public void RemovePaddle()
        // We will use this method on the GameOver Event.
        // It will deActivate paddle gameobject.
    {
        gameObject.SetActive(false);
    }

    #endregion

}
