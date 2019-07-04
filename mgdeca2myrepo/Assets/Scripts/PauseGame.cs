using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static bool GameIsPause = false;
    public GameObject PauseMenuUI;
    public GameObject joystick;
    public GameObject pausebutton;


    

    //// Update is called once per frame
    //void Update()
    //{
    //    if (GameIsPause)
    //    {
    //        Resume();
    //    }
    //    else
    //    {
    //        Pause();
    //    }   
    //}

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
        joystick.SetActive(true);
        pausebutton.SetActive(true);
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
        joystick.SetActive(false);
        pausebutton.SetActive(false);
    }
}