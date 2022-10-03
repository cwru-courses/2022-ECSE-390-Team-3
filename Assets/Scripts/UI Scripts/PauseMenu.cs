using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void closeAll()
    {
        if (PauseMenuUI != null)
        {
            PauseMenuUI.SetActive(false);
        }
        if (ConfirmMenuUI != null)
        {
            ConfirmMenuUI.SetActive(false);
        }
        if (OptionsMenuUI != null)
        {
            OptionsMenuUI.SetActive(false);
        }
    }

    public void Resume()
    {
        closeAll();
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void Pause()
    {
        closeAll();
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Confirm()
    {
        closeAll();
        ConfirmMenuUI.SetActive(true);
    }

    public void ConfirmYes()
    {
        closeAll();
        Time.timeScale = 1f;
        GamePaused = false;
        SceneController.LoadSpecificScene("Main Menu");
    }

    public void ConfirmNo()
    {
        closeAll();
        PauseMenuUI.SetActive(true);
    }

    public void Options()
    {
        closeAll();
        OptionsMenuUI.SetActive(true);
    }

    public void Restart()
    {
        Resume();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
