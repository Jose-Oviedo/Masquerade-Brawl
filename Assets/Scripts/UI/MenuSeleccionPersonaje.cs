using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSeleccionPersonaje : MonoBehaviour
{
    private int index;
    [SerializeField] private Image imagen;
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private TextMeshProUGUI timer;

    private GameManager gameManager;
    private float remainigTime;
    private float timerCount = 5f;
    private bool timerActive = false;

    private void Start()
    {
        gameManager = GameManager.Instance;
        
        
        index = PlayerPrefs.GetInt("JugadorIndex");

        if (index > gameManager.personajes.Count - 1)
        {
            index = 0;
        }
        timerPanel.SetActive(false);
    }

    private void Update()
    {
        if (timerActive)
        {
            remainigTime = remainigTime - Time.deltaTime;
            if (remainigTime<0)
            {
                timerEnded();
            }
            float timems = remainigTime * 1000;
            timer.SetText(string.Format("{0}:{1}", (int)remainigTime%60, (int)timems %1000));
        }
    }

    public void SetTimer()
    {
        timerPanel.SetActive(true);
        remainigTime = timerCount;
        timerActive = true;
    }

    public void UnSetTimer()
    {
        timerPanel.SetActive(false);
        timerActive = false;
    }

    private void timerEnded()
    {
        UnSetTimer();
        IniciarJuego();
    }

    public void IniciarJuego()
    {
        gameManager.SceneChanged();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}



/*public class Timer : MonoBehaviour
{
    float currentTime;
    public float startingTime = 10f;

    [SerializeField] Text countdownText;
    void Start()
    {
        currentTime = startingTime;
    }
    void Update()
    {
        currentTime = -1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            // Your Code Here
        }
    }
}*/
