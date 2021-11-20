using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    #region Fields

    private Quaternion currentRotation;
    private float currentDirection;
    private int Directionflag = 1;
    private float speed = 1.65f;

    #endregion

    #region MonoBehaviour

    void Update()
    // While the arrow is active we will rotate it
    {
        currentRotation = transform.rotation;
        currentDirection = currentRotation.z;
        if (currentDirection >= 0.95f)
            Directionflag = -1;
        if (currentDirection <= 0.25f)
            Directionflag = 1;
        currentRotation.z += Time.deltaTime * speed * Directionflag;
        transform.rotation = currentRotation;
    }

    #endregion

    #region Methods

    public void setArrowDir()
        // We will use this method on the BallStart Event.
        // It will set the GameManager's arrow direction.
    {
        GameManager.ArrowDir = currentDirection;
    }

    public void RemoveArrow()
    {
        gameObject.SetActive(false);
    }
    
    public void ResetArrow()
        // We will use this method on the BallDropped Event.
        // It will activate the arrow.
    {
        gameObject.SetActive(true);
    }

    #endregion

}