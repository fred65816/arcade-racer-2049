using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public int startTime;

    [HideInInspector]
    public bool isStarted;

    private float waitTime = 1.0f;
    private float waitTimer = 0.0f;
    private bool timerEnd = false;
    private Text countdownText;
    private startTimer startTimer;
    private Text startTimerText;
    private VehicleCheckPoint vCheckPoint;
    private Energy energy;

    // Start is called before the first frame update
    void Start()
    {
        energy = GameObject.Find("EnergyText").GetComponent<Energy>();
        vCheckPoint = GameObject.Find("vehicle").GetComponent<VehicleCheckPoint>();
        startTimer = GameObject.Find("StartTimer").GetComponent<startTimer>();
        startTimerText = GameObject.Find("StartTimer").GetComponent<Text>();
        countdownText = gameObject.GetComponent<Text>();
        countdownText.text = startTime.ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        // display game over when timer runs out
        if(timerEnd || !energy.hasEnergy())
        {
            startTimerText.text = "Game Over";
        }

        // if race is started or not won
        if (startTimer.isRaceStarted() && !timerEnd && !vCheckPoint.isRaceWon() && energy.hasEnergy())
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime)
            {
                waitTimer = 0;
                if (startTime > 0)
                {
                    startTime--;
                }
                else
                {
                    startTime = 0;
                    timerEnd = true;
                }
                countdownText.text = startTime.ToString("00");
            }
        }
    }

    public bool isTimerEnd()
    {
        return timerEnd;
    }

    public void subsTimer(int time)
    {
        startTime -= time;
    }
}
