using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

public class LivesManager : MonoBehaviour
{
    #region Constants

    private const float Spacing = 0.3f;
    private readonly Vector3 _heartScale = new Vector3(0.3f, 1.8f);

    #endregion
    
    #region Fields

    private int _lives = GameManager.lives;
    private int _livesCounter = 0;
    private List<GameObject> _heartList = new List<GameObject>(3);
    
    private static LivesManager _shared;

    #endregion

    #region Methods

    private void GenerateLives()
    {
        GameObject referenceHeart = (GameObject) Instantiate(Resources.Load("Heart"));
        for (int i = 0; i < _lives; i++)
        {
            GameObject heart = Instantiate(referenceHeart, transform);
            
            heart.transform.localPosition = new Vector3(-Spacing + (i*Spacing),0,0);
            heart.transform.localScale = _heartScale;
            _heartList.Add(heart);
        }
        Destroy(referenceHeart);
    }

    public static void RemoveLife()
    {
        if (_shared._livesCounter < _shared._lives)
        {
            Destroy(_shared._heartList[_shared._livesCounter]);
            _shared._livesCounter++;
        }
    }

    public static void RemoveFrame()
    {
        _shared.gameObject.SetActive(false);
    }

    #endregion

    #region GetSet

    public static int LivesCounter
    {
        get => _shared._livesCounter;
    }

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _shared = this;
    }
    
    void Start()
    {
        GenerateLives();
    }

    #endregion


    
}
