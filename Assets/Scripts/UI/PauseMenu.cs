using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public PlayerControls playerInput;
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
        BroadcastPause(gameIsPaused);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        BroadcastPause(gameIsPaused);
    }

    //broadcast pause state to all players
    private void BroadcastPause(bool paused)
    {
        GameObject pM = GameObject.Find("playerManager");
        if (pM)
        {
            var players = FindObjectsOfType<PlayerMovement>();

            foreach (PlayerMovement player in players)
            {
                player.IsPaused(paused);
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadMenu()
    {
        //se destruye el gameManager para que no de problemas, luego se crea otro automaticamente
        Destroy(GameManager.Instance.gameObject);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void PressPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pausePressed = true;
        }
    }

    void Awake()
    {
        playerInput = new PlayerControls();
        playerInput.Player.OpenMenu.started += ctx => Pause();
    }

    void OnEnable()
    {
        playerInput.Player.Enable();
    }

    void OnDisable()
    {
        playerInput.Player.Disable();
    }

}
