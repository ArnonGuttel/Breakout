using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Constants

    public const int rows = 5;
    public const int cols = 11;

    public const int lives = 3; 
    
    public const float ballSpeedMult = 1.5f;
    
    public static readonly List<int> PaddleHitSpeedChange = new List<int>(3) {3,8,15};
    
    #endregion
    
    #region Inspector

    public GameObject gameWon;
    public GameObject gameOver;

    private static GameManager _shared;

    #endregion

    #region Fields

    private bool _droppedBall;
    
    [SerializeField]
    private float _objectCounter;

    [SerializeField] 
    private int _paddleHitsCounter = 0;

    #endregion

    #region GetSet

    public static float ObjectCounter
    {
        get => _shared._objectCounter;
        set => _shared._objectCounter = value;
    }

    public static bool DroppedBall
    {
        get => _shared._droppedBall;
        set => _shared._droppedBall = value;
    }

    public static int PaddleHitsCounter
    {
        get => _shared._paddleHitsCounter;
        set => _shared._paddleHitsCounter = value;
    }
    #endregion
    
    #region MonoBehaviour
    
    private void Awake()
    {
        _shared = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!BallScript.IsMoving)
            {
                BallScript.StartBall();
            }
        }
        if (_droppedBall)
        {
            if (LivesManager.LivesCounter == lives-1)
            {
                print("Game Over !");
                gameOver.SetActive(true);
                BallScript.RemoveBall();
                PaddleScript.RemovePaddle();
                LivesManager.RemoveFrame();
            }
            BallScript.ResetBall();
            LivesManager.RemoveLife();
            _paddleHitsCounter = 0;
        }
        if (_objectCounter <= 0 )
        {
            gameWon.SetActive(true);
            BallScript.RemoveBall();
            PaddleScript.RemovePaddle();
        }
    }

    #endregion
    
}
