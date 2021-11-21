using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    #region Constants

    private const float ArrowChangeSpeed = 1.65f;

    #endregion
    
    #region Fields

    private Quaternion _currentRotation;
    private float _currentDirection;
    private int _directionFlag = 1; // well set 1 (right) as default
    private Quaternion _defaultStartRotation; 

    #endregion

    #region MonoBehaviour

    void Update()
    // While the arrow is active we will rotate it
    {
        _currentRotation = transform.rotation;
        _currentDirection = _currentRotation.z;
        if (_currentDirection >= 0.95f)
            _directionFlag = -1;
        if (_currentDirection <= 0.25f)
            _directionFlag = 1;
        _currentRotation.z += Time.deltaTime * ArrowChangeSpeed * _directionFlag;
        transform.rotation = _currentRotation;
    }

    private void Start()
    {
        _defaultStartRotation = transform.rotation;
    }

    #endregion

    #region Methods

    public void SetArrowDir()
        // We will use this method on the BallStart Event.
        // It will set the GameManager's arrow direction.
    {
        GameManager.ArrowDir = _currentDirection;
    }

    public void RemoveArrow()
        // We will use this method on the GameOver Event.
        // It will deActivate the Arrow game object.
    {
        gameObject.SetActive(false);
    }
    
    public void ResetArrow()
        // We will use this method on the BallDropped Event.
        // It will activate the arrow.
    {
        gameObject.SetActive(true);
        transform.rotation = _defaultStartRotation;
    }

    public void ActivateArrow()
        // We will use this method on the StartGame Event.
        // It will deActivate the Arrow game object.
    {
        gameObject.SetActive(true);
    }
    #endregion

}