using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    
    #region Constants

    private int _InitVelocity = 3;
    
    #endregion

    #region Inspector

    public Rigidbody2D rb;

    #endregion

    #region Fields

    private Vector2 _prevVelocity;
    
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(_InitVelocity,-_InitVelocity);
        _prevVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D contact = other.contacts[0];
        Vector2 contactNormal = contact.normal;
        Vector2 newVelocity =  Vector2.Reflect(_prevVelocity, contactNormal);
        rb.velocity = newVelocity;
        _prevVelocity = newVelocity;
    }
}
