using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public int energy;
    public int maxEnergy;
    public int wallCollisionTime;
    public AudioClip collisionSound;

    private AudioSource source;
    private Text energyText;
    private HoverMotor hoverMotor;
    private EnemyMovement enemyMovement;
    private startTimer startTimer;
    private timer timer;
    private VehicleCheckPoint vCheckPoint;
    private float waitTime = 1.0f;
    private float waitTimer = 0.0f;
    private bool isOutOfEnergy = false;
    private bool hasCollidedTrack = false;
    private int collisionTime;

    private void Awake()
    {
        startTimer = GameObject.Find("StartTimer").GetComponent<startTimer>();
        timer = GameObject.Find("Timer").GetComponent<timer>();
        hoverMotor = GameObject.Find("vehicle").GetComponent<HoverMotor>();
        vCheckPoint = GameObject.Find("vehicle").GetComponent<VehicleCheckPoint>();
        energyText = gameObject.GetComponent<Text>();
        source = gameObject.GetComponent<AudioSource>();
        energyText.text = "Energy: " + energy.ToString();
        collisionTime = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollidedTrack)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime)
            {
                waitTimer = 0;
                if (collisionTime > 0)
                {
                    collisionTime--;
                }
                else
                {
                    hasCollidedTrack = false;
                }
            }
        }

        if (hoverMotor.isCollidingTrack() && !hasCollidedTrack && startTimer.isRaceBegun() && collisionTime == 0 && energy != 0 && !timer.isTimerEnd() && !vCheckPoint.isRaceWon())
        {
            collisionTime = wallCollisionTime;
            hasCollidedTrack = true;
            energy -= 15;
            source.PlayOneShot(collisionSound);
        }

        if (energy <= 0)
        {
            energy = 0;
            isOutOfEnergy = true;
        }

        if(energy > maxEnergy)
        {
            energy = maxEnergy;
        }

        energyText.text = "Energy: " + energy.ToString();
    }

    public void subsEnergy(int points)
    {
        energy -= points;
    }

    public void addEnergy(int points)
    {
        energy += points;
    }

    public bool hasEnergy()
    {
        return !isOutOfEnergy;
    }
}
