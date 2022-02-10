using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    
    #region Inspector

    public Rigidbody2D rb;

    #endregion

    #region Fields

    private float _curDirection;
    private float _speed = 5;

    #endregion


    // Update is called once per frame
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
        rb.AddForce(new Vector2(_curDirection * _speed,0));
    }
}
