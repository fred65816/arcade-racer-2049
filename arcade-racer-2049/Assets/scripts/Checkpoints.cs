using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public AudioClip lapSound;

    private Transform vehicleTransform;
    private AudioSource source;
    // Start is called before the first frame update

    private void Awake()
    {
        source = gameObject.GetComponent<AudioSource>();
    }
    void Start()
    {
        vehicleTransform = GameObject.Find("vehicle").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if it's not the vehicle that collides, exit
        if (!other.gameObject.CompareTag("vehicle"))
            return;

        VehicleCheckPoint vCheckpoint = vehicleTransform.GetComponent<VehicleCheckPoint>();

        // is it current checkpoint?
        if (transform == vCheckpoint.checkPointArray[vCheckpoint.getCurrentCheckPoint()].transform)
        {
            // if not last checkpoint
            if(vCheckpoint.getCurrentCheckPoint() + 1 < vCheckpoint.checkPointArray.Length)
            {
                // increment current lap if checkpoint is 0
                if(vCheckpoint.getCurrentCheckPoint() == 0)
                {
                    // play lap sound effect
                    if(vCheckpoint.getCurrentLap() != 0)
                    {
                        source.PlayOneShot(lapSound);
                    }

                    //Debug.Log("lap++");
                    vCheckpoint.incCurrentLap();
                }

                //Debug.Log("cp++");
                // increment current checkpoint
                vCheckpoint.incCurrentCheckPoint();
            }
            // if last checkpoint set current checkpoint to 0
            else
            {
                vCheckpoint.setCurrentCheckpoint(0);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
