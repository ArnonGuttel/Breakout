using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GridManager : MonoBehaviour
{
    #region Constants

    private static readonly Vector3 GridPos = new Vector3(-5.5f, 4.4f);
    private const float _tileSize = 1.1f;
    private const int TilesNumber = 55;
    private const float AnimationSpeed = 13f;

    #endregion

    #region Fields

    private int _rows = GameManager.rows;
    private int _cols = GameManager.cols;
    private bool _startAnimationFlag = true;
    private int _animationTileCounter;
    private List<GameObject> _tileList = new List<GameObject>(55);

    #endregion

    #region MonoBehaviour

    void Start()
    {
        GenerateGrid();
    }

    private void Update()
    {
        if (_startAnimationFlag)
            DoStartAnimation();
    }
    
    #endregion

    #region Methods

    private void GenerateGrid()
        // This method will generate the tile's grid.
        // It will do so by first Instantiate each of the game tiles for reference (that will save running time),
        // and then with a simple loop will instantiate tiles object's, we will set each tile to be very small in favor
        // of the GameStart animation.
    {
        GameObject referenceDefaultTile = (GameObject) Instantiate(Resources.Load("BlueTile"));
        GameObject referencerRedTile = (GameObject) Instantiate(Resources.Load("RedTile"));
        GameObject referencerPurpleTile = (GameObject) Instantiate(Resources.Load("PurpleTile"));
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                GameObject tile;
                if (row == 0)
                {
                    tile = Instantiate(referencerPurpleTile, transform);
                }
                else if (row == 1 | row == 2)
                {
                    tile = Instantiate(referencerRedTile, transform);
                    if (row == 2)
                        tile.tag = "MiddleTile";
                }
                else
                {
                    tile = Instantiate(referenceDefaultTile, transform);
                }

                float posX = col * _tileSize;
                float posY = row * -_tileSize;

                tile.transform.position = new Vector3(posX, posY);
                tile.transform.localScale = new Vector3(0.01f, 0.01f, 1);
                _tileList.Add(tile);
                tile.name = "(" + row.ToString() + "," + col.ToString() + ")";

                GameManager.TilesCounter = GameManager.TilesCounter + 1;
            }
        }

        // We will destroy the referenced til's to save memory and avoid confusion 
        Destroy(referencerPurpleTile);
        Destroy(referenceDefaultTile);
        Destroy(referencerRedTile);
        transform.position = GridPos;
    }

    public static void RemoveTile(GameObject tile)
        // This method when be called from the tile's script's.
    {
        Destroy(tile);
        GameManager.TilesCounter = GameManager.TilesCounter - 1;
    }
    
    private void DoStartAnimation()
        // To animate the tiles, we will go over each tile and increase its scale.
    {
        GameObject curTile = _tileList[_animationTileCounter];
        if (curTile.transform.localScale.x < _tileSize)
        {
            Vector3 scale = curTile.transform.localScale;
            float change = AnimationSpeed * Time.deltaTime;
            curTile.transform.localScale = new Vector3(scale.x + change, scale.y + change, scale.y + change);
        }
        else
        {
            curTile.transform.localScale = new Vector3(_tileSize,_tileSize,_tileSize) ;
            _animationTileCounter++;
            if (_animationTileCounter == TilesNumber)
            {
                _startAnimationFlag = false;
                GameManager.FinishStartAnimation = true;
            }
        }

    }
    #endregion
}