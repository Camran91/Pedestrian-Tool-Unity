using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PedestrianController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float minSpeed = .8f;
    [SerializeField] float maxSpeed = 1.2f;
    [SerializeField] float stopDistance = 2.5f;
    [SerializeField] float rotationSpeed = 120f;
    
    Vector3 destination;
    Vector3 lastPosition;
    Vector3 velocity;

    float movementSpeed;
    Animator pedAnim;
    readonly string walkingBool = "IsWalking";

    [Header("Testing")]
    public bool reachedDestination = false;
    public bool stopWalking = false;

    private void Start()
    {
        movementSpeed = Random.Range(minSpeed, maxSpeed);
        pedAnim = GetComponent<Animator>();
        pedAnim.speed = movementSpeed;
        pedAnim.SetBool(walkingBool, true);
    }
    private void Update()
    {
        if (transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance >= stopDistance)
            {
                if (!stopWalking)
                {
                    if (pedAnim.GetBool(walkingBool) == false)
                    {
                        pedAnim.SetBool(walkingBool, true);
                    }

                    reachedDestination = false;
                    Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);  
                }
                else if (pedAnim.GetBool(walkingBool) == true)
                {
                    pedAnim.SetBool(walkingBool, false);
                }
            }
            else
            {
                reachedDestination = true;
            }

            velocity = (transform.position - lastPosition) / Time.deltaTime;
            velocity.y = 0;
            var velocityMagnitude = velocity.magnitude;
            velocity = velocity.normalized;
            var fwdDotProduct = Vector3.Dot(transform.forward, velocity);
            var rightDotProduct = Vector3.Dot(transform.right, velocity);
        }
    }

    public void SetDestination(Vector3 destination)
    {
        if(destination != null)
        {
            this.destination = destination;
            reachedDestination = false;
        }
    }

    public IEnumerator StopMovingForLengthOfTime(float remainingTime)
    {
        float offset = Random.Range(0.25f, .75f);

        yield return new WaitForSeconds(offset);
        stopWalking = true;

        yield return new WaitForSeconds(remainingTime + offset);
        stopWalking = false;
    }

    public IEnumerator SetSpeed()
    {
        movementSpeed *= 2.5f;
        pedAnim.speed = movementSpeed;

        yield return new WaitForSeconds(7f);
        movementSpeed /= 2.5f;
        pedAnim.speed = movementSpeed;
    }
}
