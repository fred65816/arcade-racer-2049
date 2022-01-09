using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBMovement : MonoBehaviour
{
    public float speed;
    public float xMovement;
    public float zMovement;
    public AudioClip collisionSound;
    private Vector3 movementVector;

    private bool enemyCollideVehicle;
    private float xInverted;
    private float zInverted;
    private AudioSource source;
    private timer timer;
    private Energy energy;
    private Rigidbody enemyRigidbody;


    // Start is called before the first frame update
    void Start()
    {
        energy = GameObject.Find("EnergyText").GetComponent<Energy>();
        timer = GameObject.Find("Timer").GetComponent<timer>();
        source = gameObject.GetComponent<AudioSource>();
        enemyRigidbody = GetComponent<Rigidbody>();
        movementVector = new Vector3(xMovement * speed, 0, zMovement * speed);
        xInverted = -xMovement;
        zInverted = -zMovement;
        enemyCollideVehicle = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += movementVector;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("track"))
        {
            movementVector = new Vector3(xInverted * speed, 0, zInverted * speed);
            xInverted = -xInverted;
            zInverted = -zInverted;
        }
        else if (other.gameObject.CompareTag("vehicle") && !enemyCollideVehicle)
        {
            enemyCollideVehicle = true;
            energy.subsEnergy(15);
            source.PlayOneShot(collisionSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("vehicle"))
        {
            enemyCollideVehicle = false;
        }
    }

    public bool isCollidingEnemy()
    {
        return enemyCollideVehicle;
    }

}
