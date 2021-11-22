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
    
    [SerializeField] private UnityEvent startGame;
    [SerializeField] private UnityEvent startBall;
    [SerializeField] private UnityEvent ballDropped;
    [SerializeField] private UnityEvent GameOver;

    #endregion

    #region Constants

    public const int rows = 5;
    public const int cols = 11;
    public const int lives = 3;
    public const float ballSpeedMult = 1.5f;

    /* We will use different paddle hit numbers til speed change, and also different speeds */
    public static readonly List<int> PaddleHitSpeedChange = new List<int>(3) {3, 6, 10};

    #endregion

    #region Inspector

    public GameObject gameWonFrame;
    public GameObject gameOverFrame;
    public Camera Camera;

    /* Hybrid script */
    private static GameManager _shared;

    #endregion

    #region Fields

    [SerializeField] private int _tilesCounter;

    [SerializeField] private int _paddleHitsCounter;

    private bool _finishStartAnimation;
    private bool _gameStartedFlag;
    private bool _ballMoving;
    private bool _droppedBall;
    private int _livesCounter = 3;

    private float _arrowDir; // we will use the arrow direction to set the ball start direction

    /* We will use an variable that holds the camera Animator to active the camera shake trigger */
    private Animator cameraAnimator;

    #endregion

    #region GetSet

    public static int TilesCounter
        /* Will be set by GridManager */
    {
        get => _shared._tilesCounter;
        set => _shared._tilesCounter = value;
    }
    public static bool FinishStartAnimation
        /* Will be set by ArrowScript */
    {
        set => _shared._finishStartAnimation = value;
    }
    public static bool DroppedBall
        /* Will be set by BallScript */
    {
        set => _shared._droppedBall = value;
    }

    public static int PaddleHitsCounter
        /* Will be updated by BallScript */
    {
        get => _shared._paddleHitsCounter;
        set => _shared._paddleHitsCounter = value;
    }

    public static float ArrowDir
        /* Will be updated by ArrowScript, and will be used by BallScript */
    {
        get => _shared._arrowDir;
        set => _shared._arrowDir = value;
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
        if (!_finishStartAnimation) // Do nothing until start animation finished
            return;
        if (!_gameStartedFlag) // invoke StartGame only once
        {
            startGame.Invoke();
            _gameStartedFlag = true;
        }
        if (!_ballMoving)
            checkPlayerInput();
        if (_droppedBall)
            droppedBall();
        checkGameWon();
    }

    #endregion

    #region Methods

    private void checkPlayerInput()
        // This method check whether the player wish to start the game.  
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startBall.Invoke();
            _ballMoving = true;
        }
    }

    private void droppedBall()
        // This method will be activated once the ball dropped, it will initiate right UnityEvent, and will reset game
        // values,in case that the player have no more lives, it will initiate the GameOver UnityEvent
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
        if (_tilesCounter <= 0)
        {
            gameWonFrame.SetActive(true);
            print("You Won!!");
        }
    }

    public void resetGame()
        // In case that the user have no more lives, and will press the "Play Again" button , we will reset the scene. 
    {
        SceneManager.LoadScene("GameScene");
    }

    public void exitGame()
        // In case that the user have no more lives, and will press the "Exit Game" button , we will Close the scene. 
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
    
    #endregion
}