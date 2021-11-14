using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTileScript : MonoBehaviour
{
    private void OnCollisionEnter2D()
    {
        GridManager.RemoveTile(gameObject);
    }
}
