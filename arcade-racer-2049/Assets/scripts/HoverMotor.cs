using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoverMotor : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public float hoverForce;
    public float hoverHeight;
    private float powerInput;
    private float turnInput;
    private Rigidbody carRigidbody;
    private timer timer;
    private startTimer startTimer;
    private VehicleCheckPoint vCheckPoint;
    private Energy energy;
    private bool isTurningLeft = false;
    private bool isTurningRight = false;
    private int maxTurningAngle = 45;
    private int turningAngle = 0;
    private Vector3 rbVelocity;
    private bool vehicleCollideTrack = false;
    private float waitTime = 1.0f;
    private float waitTimer = 0.0f;
    private int endWaitTime = 2;

    void Awake()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        energy = GameObject.Find("EnergyText").GetComponent<Energy>();
        timer = GameObject.Find("Timer").GetComponent<timer>();
        startTimer = GameObject.Find("StartTimer").GetComponent<startTimer>();
        vCheckPoint = gameObject.GetComponent<VehicleCheckPoint>();
    }

    void Update()
    {
        rbVelocity = carRigidbody.velocity;
        //Debug.Log(rbVelocity.x + " " + rbVelocity.y + " " + rbVelocity.z);
        powerInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        if (Input.GetKey("left"))
        {
            isTurningLeft = true;
            isTurningRight = false;
        }
        else if (Input.GetKey("right"))
        {
            isTurningRight = true;
            isTurningLeft = false;
        }
        else
        {
            isTurningLeft = false;
            isTurningRight = false;
        }
    }

    void FixedUpdate()
    {
        // quit game is Escape pressed
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
   
        if (!timer.isTimerEnd() && startTimer.isRaceStarted() && !vCheckPoint.isRaceWon() && energy.hasEnergy())
        {
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hit;

            // does not work properly
            /*if (Input.GetKey("left") && !isTurningLeft)
            {
                isTurningRight = false;
                isTurningLeft = true;
                transform.Rotate(0, 0, -turningAngle, Space.Self);
                turningAngle = 0;
            }
            else if (Input.GetKey("right") && !isTurningRight)
            {
                isTurningRight = true;
                isTurningLeft = false;
                transform.Rotate(0, 0, turningAngle, Space.Self);
                turningAngle = 0;
            }
            else if (Input.GetKey("left") && turningAngle != maxTurningAngle)
            {
                transform.Rotate(0, 0, 1, Space.Self);
                turningAngle++;
            }
            else if (Input.GetKey("right") && turningAngle != maxTurningAngle)
            {
                transform.Rotate(0, 0, -1, Space.Self);
                turningAngle++;
            }
            else if (isTurningLeft && turningAngle != 0)
            {
                turningAngle--;
                transform.Rotate(0, 0, -1, Space.Self);
            }
            else if (isTurningRight && turningAngle != 0)
            {
                turningAngle--;
                transform.Rotate(0, 0, 1, Space.Self);
            }
            else if (turningAngle == 0)
            {
                isTurningRight = false;
                isTurningLeft = false;
            }*/

            if (Physics.Raycast(ray, out hit, hoverHeight))
            {
                float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
                Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
                carRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
            }

            carRigidbody.AddRelativeForce(0f, 0f, powerInput * speed);
            //carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);

            if (isTurningLeft)
            {
                //carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
                transform.Rotate(0, -turnSpeed, 0, Space.World);
            }
            else if (isTurningRight)
            {
                //carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
                transform.Rotate(0, turnSpeed, 0, Space.World);
            }
            else
            {
                //carRigidbody.AddRelativeTorque(0f, 0f, 0f);
            }
        }
        else if (timer.isTimerEnd() || vCheckPoint.isRaceWon() || !energy.hasEnergy())
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime)
            {
                waitTimer = 0;
                if (endWaitTime > 0)
                {
                    endWaitTime--;
                }
                else
                {
                    SceneManager.LoadScene(2, LoadSceneMode.Single);
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "track")
        {
            Debug.Log(rbVelocity.x + " " + rbVelocity.y + " " + rbVelocity.z);
            //Vector3 reflexion = new Vector3(-rbVelocity.x, rbVelocity.y, rbVelocity.z / 2);
            carRigidbody.velocity = Vector3.zero;
            vehicleCollideTrack = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "track")
        {
            vehicleCollideTrack = false;
        }
    }

    public bool isCollidingTrack()
    {
        return vehicleCollideTrack;
    }


}
