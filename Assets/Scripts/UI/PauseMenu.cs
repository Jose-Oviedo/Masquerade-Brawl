using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public PlayerInput playerInput;
    public static bool gameIsPaused = false;

    private bool pausePressed = false;

    // Update is called once per frame
    void Update()
    {
        if (pausePressed)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            pausePressed = false;
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("quitting game....     TODO");
    }
    public void LoadMenu()
    {
        Debug.Log("loading game....     TODO");
    }

    public void PressPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pausePressed = true;
            //playerInput.SwitchCurrentActionMap("UI");//testing...
        }
            Debug.Log("aaaa");
    }

}
