using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startTimer : MonoBehaviour
{
    public int startTime;
    public float goTime;
    public AudioClip beepSfx1;
    public AudioClip beepSfx2;

    private bool raceStarted = false;
    private bool raceBegun = false;
    private float waitTime = 1.0f;
    private float waitTimer = 0.0f;
    private Text countdownText;
    private AudioSource source;

    private void Awake()
    {
        countdownText = gameObject.GetComponent<Text>();
        source = gameObject.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // remove "GO!" when race start
        if (raceStarted && waitTimer >= goTime)
        {
            countdownText.text = string.Empty;
            raceBegun = true;
        }

        waitTimer += Time.deltaTime;

        if (!raceStarted && waitTimer >= waitTime)
        {
            waitTimer = 0;
            if (startTime > 0)
            {
                startTime--;
            }

            if (startTime > 0)
            {
                source.PlayOneShot(beepSfx1);
                countdownText.text = startTime.ToString("0");
            }
            else
            {
                source.PlayOneShot(beepSfx2);
                countdownText.text = "GO!";
                raceStarted = true;
            }
        }
    }

    public bool isRaceStarted()
    {
        return raceStarted;
    }

    public bool isRaceBegun()
    {
        return raceBegun;
    }
}
