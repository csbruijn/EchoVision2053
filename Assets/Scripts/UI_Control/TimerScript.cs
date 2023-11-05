using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    int timeTimer = 20;
    public TMP_Text timerUI;
    private bool restartCountdownStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        countDownTimer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void countDownTimer()
    {
        if (timeTimer > 0)
        {
            TimeSpan spanTime = TimeSpan.FromSeconds(timeTimer);
            timerUI.text = "Find your phone: " + spanTime.Minutes + ":" + spanTime.Seconds;
            timeTimer--;
            Invoke("countDownTimer", 1.0f);
        }
        else
        {
            if (!restartCountdownStarted)
            {
                StartCoroutine(StartRestartCountdown());
            }
        }
    }

    IEnumerator StartRestartCountdown()
    {
        restartCountdownStarted = true;
        timerUI.text = "Mission Failed!";
        yield return new WaitForSeconds(2.0f);
        timerUI.text = "The scene will restart in 5 seconds";
        yield return new WaitForSeconds(1.0f);
        timerUI.text = "The scene will restart in 4 seconds";
        yield return new WaitForSeconds(1.0f);
        timerUI.text = "The scene will restart in 3 seconds";
        yield return new WaitForSeconds(1.0f);
        timerUI.text = "The scene will restart in 2 seconds";
        yield return new WaitForSeconds(1.0f);
        timerUI.text = "The scene will restart in 1 seconds";
        yield return new WaitForSeconds(1.0f);
        timerUI.text = "The scene will restart in 0 seconds";

        // Restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}