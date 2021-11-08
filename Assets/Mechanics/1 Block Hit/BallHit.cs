using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHit : MonoBehaviour
{
    #region Constants

    #endregion

    #region Inspector

    public Rigidbody2D rb;

    #endregion

    #region Fields

    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TrigerRec")
        {
            Destroy(other.gameObject);
        }
    }
}