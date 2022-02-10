using UnityEngine;

public class Lives : MonoBehaviour
{
    #region Constants

    #endregion

    #region Inspector

    public GameObject firstLife;
    public GameObject secondLife;
    public GameObject thirdLife;

    #endregion

    #region Fields

    private float _pressCounter = 0;
    
    #endregion
    

    // Update is called once per frame
    void Update()
    {
        if (_pressCounter < 3)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_pressCounter == 0)
                    firstLife.SetActive(false);
                else if (_pressCounter == 1)
                    secondLife.SetActive(false);
                else if (_pressCounter == 2)
                    thirdLife.SetActive(false);
                _pressCounter++;
            }
        }
    }
    
    
}
