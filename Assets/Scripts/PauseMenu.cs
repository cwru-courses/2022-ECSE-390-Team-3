using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject PauseMenuUI;
    public GameObject ConfirmMenuUI;
    public GameObject OptionsMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        ConfirmMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        ConfirmMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Confirm()
    {
        PauseMenuUI.SetActive(false);
        ConfirmMenuUI.SetActive(true);
        OptionsMenuUI.SetActive(false);
    }

    public void ConfirmYes()
    {
        PauseMenuUI.SetActive(false);
        ConfirmMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void ConfirmNo()
    {
        OptionsMenuUI.SetActive(false);
        ConfirmMenuUI.SetActive(false);
        PauseMenuUI.SetActive(true);
    }

    public void Options()
    {
        PauseMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(true);
        PauseMenuUI.SetActive(false);
    }
}
