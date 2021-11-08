using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Square : MonoBehaviour
{

    #region Inspector
    
    public BackgroundScript _backgroundScript;

    #endregion
    
    private void Start()
    {
        _backgroundScript._ObjectCounter++;
    }


    private void OnMouseDown()
    {
        _backgroundScript._ObjectCounter--;
        gameObject.SetActive(false);
    }
    
}
