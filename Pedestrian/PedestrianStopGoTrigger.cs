using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianStopGoTrigger : MonoBehaviour
{
    readonly string pedestrian = "Pedestrian";
    PedestrianStopGoManager psgm;
    PedestrianNavigation pn;
    PedestrianController pc;
    [Tooltip("Only triggers if direction is a certain way | 0 or 1")]
    [Range(0, 1)] public int direction;
    public bool stopWalking = false;

    private void Start()
    {
        psgm = GetComponentInParent<PedestrianStopGoManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(pedestrian) && stopWalking)
        {
            pn = other.GetComponent<PedestrianNavigation>();

            if (direction == pn.GetDirection()) //is going the right way to stop at the crosswalk
            {
                pc = other.GetComponent<PedestrianController>();
            }
        }
        else if (other.CompareTag(pedestrian) && !stopWalking)
        {
            pn = other.GetComponent<PedestrianNavigation>(); 
        }
    }

    public void StopWalking(bool value)
    {
        stopWalking = value;
    }
}