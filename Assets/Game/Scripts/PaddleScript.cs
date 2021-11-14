using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PaddleScript : MonoBehaviour
{
    #region Inspector

    public Rigidbody2D rb;

    #endregion
    
    #region Fields

    private float _curDirection;
    public float _paddleSpeed = 5;
    private static PaddleScript _shared;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _shared = this;
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
        rb.AddForce(new Vector2(_curDirection * _paddleSpeed,0));
    }

    #endregion

    #region Methods

    public static void RemovePaddle()
    {
        _shared.gameObject.SetActive(false);
    }

    #endregion

}
