using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{

    #region Inspector

    public SpriteRenderer backgroundSP;

    #endregion

    #region Fields

    public float _ObjectCounter;

    #endregion
    
    
    private void Update()
    {
        if (_ObjectCounter <= 0)
        {
            backgroundSP.color = Color.green;
        }

    }
}
