using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    public Canvas LevelLoseCanvas;

    // Start is called before the first frame update
    void Start()
    {
        LevelLoseCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleLoseCondition()
    {
        LevelLoseCanvas.enabled = true;
        
    }
}
