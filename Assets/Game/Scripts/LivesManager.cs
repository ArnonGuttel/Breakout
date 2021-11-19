using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

public class LivesManager : MonoBehaviour
{

    #region Constants

    private const float Spacing = 0.35f;
    private readonly Vector3 _heartScale = new Vector3(0.3f, 2f);

    #endregion
    
    #region Fields

    private int _lives = GameManager.lives;
    private int _livesCounter = 0;
    private List<GameObject> _heartList = new List<GameObject>(3);

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

    public void RemoveLife()
    {
        if (_livesCounter < _lives)
        {
            Animator animator = _heartList[_livesCounter].GetComponent<Animator>();
            animator.SetTrigger("RemoveHeart");
            _livesCounter++;
        }
    }

    public void RemoveFrame()
    {
        gameObject.SetActive(false);
    }

    #endregion

    #region MonoBehaviour

    void Start()
    {
        GenerateLives();
    }

    #endregion


    
}
