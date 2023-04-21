using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianNavigation : MonoBehaviour
{
    public PedestrianWaypoint currentWaypoint;

    PedestrianController controller;
    public int direction;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PedestrianController>();
        direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        controller.SetDestination(currentWaypoint.GetPosition());
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.reachedDestination)
        {
            bool shouldBranch = false;

            if(currentWaypoint.branches != null)
            {
                if(currentWaypoint.branches.Count > 0)
                {
                    shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio ? true : false;
                }
            }

            if (shouldBranch)
            {
                currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count)]; // picks a random brach from the list
            }
            else
            {
                if (direction == 0)
                {
                    if(currentWaypoint.nextWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.previousWaypoint;
                        direction = 1;
                    }
                }
                else if (direction == 1)
                {
                    if(currentWaypoint.previousWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.previousWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                        direction = 0;
                    }
                }
            }
            
            controller.SetDestination(currentWaypoint.GetPosition());
        }
    }

    public int GetDirection()
    {
        return direction;
    }
}
