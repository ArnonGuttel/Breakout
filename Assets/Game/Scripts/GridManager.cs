using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    #region Constants

    private static readonly Vector3 GridPos = new Vector3(-5.5f, 4.4f);

    #endregion
    
    #region Fields

    private int _rows = GameManager.rows;
    private int _cols = GameManager.cols;
    private float _tileSize = 1;
    
    #endregion

    #region MonoBehaviour

    void Start()
    {
        GenerateGrid();
    }

    #endregion

    #region Methods

    private void GenerateGrid()
    {
        GameObject referenceDefaultTile = (GameObject)Instantiate(Resources.Load("BlueTile"));
        GameObject referencerRedTile = (GameObject)Instantiate(Resources.Load("RedTile"));
        GameObject referencerPurpleTile = (GameObject)Instantiate(Resources.Load("PurpleTile"));
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
                }
                else
                {
                    tile = Instantiate(referenceDefaultTile, transform);
                }
                float posX = col * _tileSize;
                float posY = row * -_tileSize;

                tile.transform.position = new Vector3(posX, posY);

                GameManager.ObjectCounter = GameManager.ObjectCounter + 1;
            }
            
        }
        Destroy(referencerPurpleTile);
        Destroy(referenceDefaultTile);
        Destroy(referencerRedTile);
        transform.position = GridPos;
    }
    
    public static void RemoveTile(GameObject tile)
    {
        Destroy(tile);
        GameManager.ObjectCounter = GameManager.ObjectCounter - 1;
    }

    #endregion
    
}


