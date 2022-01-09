using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleCheckPoint : MonoBehaviour
{
    public Transform[] checkPointArray;
    public int laps;
    public Text lapText;
    public float lapTextTimer;

    private int currentCheckPoint;
    private int currentLap;
    private int previousLap;
    private int remainingLaps;
    private bool isDisplayingText;
    private bool raceWon;
    private float waitTimer = 0.0f;
    private Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        previousLap = 0;
        currentLap = 0;
        raceWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentLap != previousLap)
        {
            previousLap++;
            remainingLaps = laps - currentLap;
            isDisplayingText = true;

            if(remainingLaps == 1)
            {
                lapText.text = "Last lap!";
            }
            else if(remainingLaps != 0)
            {
                lapText.text = remainingLaps.ToString() + " laps to go!";
            }
            else
            {
                lapText.text = "Race won!";
                raceWon = true;
            }
        }

        if (isDisplayingText)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= lapTextTimer)
            {
                waitTimer = 0;
                isDisplayingText = false;
                lapText.text = string.Empty;
            }
        }
    }

    public int getCurrentCheckPoint()
    {
        return currentCheckPoint;
    }

    public void setCurrentCheckpoint(int checkpoint)
    {
        currentCheckPoint = checkpoint;
    }

    public void incCurrentCheckPoint()
    {
        currentCheckPoint++;
    }

    public int getCurrentLap()
    {
        return currentLap;
    }

    public void setCurrentLap(int lap)
    {
        currentLap = lap;
    }

    public void incCurrentLap()
    {
        currentLap++;
    }

    public bool isRaceWon()
    {
        return raceWon;
    }
}
