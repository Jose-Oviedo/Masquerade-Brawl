using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void PlayGameAgain()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadMenu()
    {
        //se destruye el gameManager para que no de problemas, luego se crea otro automaticamente
        Destroy(GameManager.Instance.gameObject);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
