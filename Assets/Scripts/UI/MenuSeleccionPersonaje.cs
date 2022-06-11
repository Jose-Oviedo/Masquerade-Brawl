using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSeleccionPersonaje : MonoBehaviour
{
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private TextMeshProUGUI timer;

    private GameManager gameManager;
    private float remainigTime;
    private float timerCount = 5f;
    private bool timerActive = false;

    private void Start()
    {
        gameManager = GameManager.Instance;
        timerPanel.SetActive(false);
    }

    private void Update()
    {
        //if timer is active update the counter GUI with the remaining time, if it reached 00:00 load next scene
        if (timerActive)
        {
            remainigTime = remainigTime - Time.deltaTime;
            if (remainigTime<0)
            {
                UnSetTimer();
                IniciarJuego();
            }
            float timems = remainigTime * 1000;
            timer.SetText(string.Format("{0}:{1}", (int)remainigTime%60, (int)timems %1000));
        }
    }

    //start 5 sec timer
    public void SetTimer()
    {
        timerPanel.SetActive(true);
        remainigTime = timerCount;
        timerActive = true;
    }
    // unset timer
    public void UnSetTimer()
    {
        timerPanel.SetActive(false);
        timerActive = false;
    }

    //load gameplay scene
    public void IniciarJuego()
    {
        gameManager.SceneChanged();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
