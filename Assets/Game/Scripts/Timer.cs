using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerMinutes;
    public TextMeshProUGUI timerSeconds;
    public TextMeshProUGUI timerSeconds100;

    private float startTime;
    private float stopTime;
    private float timerTime;
    private bool isRunning = false;

    void Start()
    {
        // Si ya hay tiempo acumulado en el GameManager (otra escena),
        // arrancamos desde ese valor.
        if (GameManager.Instance != null)
        {
            stopTime = GameManager.Instance.GlobalTime;
        }

        TimerStart();
    }

    public void TimerStart()
    {
        if (!isRunning)
        {
            isRunning = true;
            startTime = Time.time;
        }
    }

    public void TimerStop()
    {
        if (isRunning)
        {
            isRunning = false;
            stopTime = timerTime;

            // Guardar el tiempo final en el GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SetTime(stopTime);
            }
        }
    }

    public void TimerReset()
    {
        stopTime = 0;
        isRunning = false;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetTime(0f);
        }

        timerMinutes.text = timerSeconds.text = timerSeconds100.text = "00";
    }

    void Update()
    {
        // Cronometro base
        timerTime = stopTime + (Time.time - startTime);
        int minutesInt = (int)timerTime / 60;
        int secondsInt = (int)timerTime % 60;
        int seconds100Int = (int)(Mathf.Floor((timerTime - (secondsInt + minutesInt * 60)) * 100));

        if (isRunning)
        {
            timerMinutes.text = (minutesInt < 10) ? "0" + minutesInt : minutesInt.ToString();
            timerSeconds.text = (secondsInt < 10) ? "0" + secondsInt : secondsInt.ToString();
            timerSeconds100.text = (seconds100Int < 10) ? "0" + seconds100Int : seconds100Int.ToString();
        }

        // Sincronizar con el GameManager cada frame
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetTime(timerTime);
        }
    }
}
