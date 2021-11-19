using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    
    #region Events

    [SerializeField] private UnityEvent startBall;
    [SerializeField] private UnityEvent ballDropped;
    [SerializeField] private UnityEvent GameOver;
    

    #endregion
    
    #region Constants

    public const int rows = 5;
    public const int cols = 11;
    public const int lives = 3;
    public const float ballSpeedMult = 1.5f;
    public static readonly List<int> PaddleHitSpeedChange = new List<int>(3) {3,6,10};
    
    #endregion
    
    #region Inspector

    public GameObject gameWonFrame;
    public GameObject gameOverFrame;
    public Camera Camera;

    private static GameManager _shared;

    #endregion

    #region Fields

    [SerializeField]
    private float _objectCounter;

    [SerializeField] 
    private int _paddleHitsCounter;
    
    private bool _ballMoving; 
    private bool _droppedBall;
    private int _livesCounter = 3;
    private Animator cameraAnimator;
    
    #endregion

    #region GetSet

    public static float ObjectCounter
    {
        get => _shared._objectCounter;
        set => _shared._objectCounter = value;
    }

    public static bool DroppedBall
    {
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
        cameraAnimator = _shared.Camera.GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (!_ballMoving)
            checkPlayerInput();
        if (_droppedBall) 
            droppedBall();
        checkGameWon();
    }

    #endregion

    #region Methods

    private void checkPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startBall.Invoke();
            _ballMoving = true;
        }
    }
    
    private void droppedBall()
    {
        ballDropped.Invoke();
        cameraAnimator.SetTrigger("CameraShake");
        _paddleHitsCounter = 0;
        _livesCounter--;
        _ballMoving = false;
        if (_livesCounter == 0)
        {
            GameOver.Invoke();
            gameOverFrame.SetActive(true);
            print("Game Over !");
        }
    }
    
    private void checkGameWon()
    {
        if (_objectCounter <= 0 )
        {
            gameWonFrame.SetActive(true);
            print("You Won!!");
        }
    }
    
    public void resetGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    #endregion



}
