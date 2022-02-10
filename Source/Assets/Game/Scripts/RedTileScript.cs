
using UnityEngine;

public class RedTileScript : MonoBehaviour
{
    private int _hitCounter = 0;
    public SpriteRenderer sp;
    public Sprite newSprite;

    private void OnCollisionEnter2D()
    {
        // we will change to tile to cracked one, and on the next hit we will destroy it.
        if (_hitCounter == 0)
        {
            sp.sprite = newSprite;
            _hitCounter++;
        }
        else
        {
            GridManager.RemoveTile(gameObject);
        }
    }
}