﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    int currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadPracticeLevel()
    {
        SceneManager.LoadScene("Demo Level");
    }

    public void LoadNextLevel()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex==2)
        {
            
            FindObjectOfType<GameCanvas>().InitialHealth();
        }
        SceneManager.LoadScene(currentScene + 1);
    }

    public void Restart()
    {
        if (FindObjectOfType<GameCanvas>().RestartTimes>0)
        {
            Time.timeScale = 1;
            FindObjectOfType<GameCanvas>().RestoreInitialStats();
            FindObjectOfType<GameCanvas>().RestartTimes--;
            SceneManager.LoadScene(currentScene);
        }

        else
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
