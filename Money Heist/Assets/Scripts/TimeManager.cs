using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static bool GameIsSlowed = false;
    public float slowDownFactor = 0.05f;
    public float slowDownTime = 2f;

    public GameCanvas slowmoBar;

    private void Start()
    {
        if (slowmoBar != null)
        {
            slowmoBar.SetMaxSlowmo(slowDownTime);
        }
    }

    private void Update()
    {
        if(slowmoBar==null)
        {
            slowmoBar= FindObjectOfType<GameCanvas>();
        }
        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            if(GameIsSlowed)
            {
                NotSlomo();
            }
            else
            {
                if (slowDownTime > 0)
                {
                    DoSlowMotion();
                }
            } 
        }

        if(GameIsSlowed)
        {
            slowDownTime -= 0.01f;
            if(slowDownTime<=0)
            {
                NotSlomo();
            }
        }

        if(slowDownTime<=2 && !GameIsSlowed)
        {
            StartCoroutine(IncreaseTime());
        }

        slowDownTime = Mathf.Clamp(slowDownTime, 0, 2f);
        if (slowmoBar != null)
        {
            slowmoBar.SetSlowmo(slowDownTime);
        }
    }

    IEnumerator IncreaseTime()
    {
        yield return new WaitForSeconds(5f);
        slowDownTime += 0.01f;
    }

    private void NotSlomo()
    {
        GameIsSlowed = false;
        Time.timeScale = 1;
    }

    public void DoSlowMotion()
    {
        GameIsSlowed = true;
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
