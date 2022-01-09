using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oneleg : MonoBehaviour
{
    public Transform[] target;
    public float speed;
    public AudioClip collisionSound;

    private int current = 0;
    private bool opposite = false;
    private bool onelegCollideVehicle = false;
    private Energy energy;
    private AudioSource source;

    private void Start()
    {
        energy = GameObject.Find("EnergyText").GetComponent<Energy>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (transform.position != target[current].position)
        {
            Vector3 position = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(position);
        }
        else
        {
            if (current == 0)
            {
                opposite = false;
            }

            if (current == target.Length - 1)
            {
                opposite = true;
            }

            if (!opposite)
            {
                current++;
            }
            else
            {
                current--;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("vehicle") && !onelegCollideVehicle)
        {
            Debug.Log("isColliding");
            onelegCollideVehicle = true;
            energy.addEnergy(10);
            source.PlayOneShot(collisionSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("vehicle"))
        {
            onelegCollideVehicle = false;
        }
    }
}