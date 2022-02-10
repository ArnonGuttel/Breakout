using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour
{
    
    #region Inspector

    public Rigidbody2D rb;

    #endregion
    
    #region Fields
    
    private bool _isMoving;

    #endregion

    private void Start()
    {
        ResetValvs();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isMoving)
                ResetValvs();
            else
            {
                rb.simulated = true;
                _isMoving = true;
            }
        }
        
    }

    void ResetValvs()
    {
        rb.simulated = false;
        rb.velocity = Vector2.zero;
        gameObject.transform.position = Vector3.zero;
        _isMoving = false;
    }
}
