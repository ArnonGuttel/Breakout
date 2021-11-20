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
    private float _tileSize = 1.1f;

    #endregion

    #region MonoBehaviour

    void Start()
    {
        GenerateGrid();
    }

    #endregion

    #region Methods

    private void GenerateGrid()
        // This method will generate the tile's grid.
        // It will do so by first Instantiate each of the game tiles for reference (that will save running time),
        // and then with a simple loop will instantiate tiles object's.
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
                }
                else
                {
                    tile = Instantiate(referenceDefaultTile, transform);
                }

                float posX = col * _tileSize;
                float posY = row * -_tileSize;

                tile.transform.position = new Vector3(posX, posY);
                tile.transform.localScale = new Vector3(1.1f, 1.1f, 1);

                tile.name = "(" + row.ToString() + "," + col.ToString() + ")";

                GameManager.ObjectCounter = GameManager.ObjectCounter + 1;
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
        GameManager.ObjectCounter = GameManager.ObjectCounter - 1;
    }

    #endregion
}