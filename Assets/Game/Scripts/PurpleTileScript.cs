using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleTileScript : MonoBehaviour
{
    private int _hitCounter;
    public SpriteRenderer sp;
    public Sprite redSprite;
    public Sprite redCrackedSprite;
    
    private void OnCollisionEnter2D()
    {
        if (_hitCounter == 0)
        {
            sp.sprite = redSprite;
            _hitCounter++;
        }
        else if (_hitCounter == 1)
        {
            sp.sprite = redCrackedSprite;
            _hitCounter++;
        }
        else if (_hitCounter == 2)
        {
            GridManager.RemoveTile(gameObject);
        }
    }
}