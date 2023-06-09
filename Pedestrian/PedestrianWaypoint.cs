using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianWaypoint : MonoBehaviour
{
    public PedestrianWaypoint previousWaypoint;
    public PedestrianWaypoint nextWaypoint;

    [Range(0f, 5f)] public float width = 2f;
    [Range(0f, 1f)] public float branchRatio = 0.5f;

    public List<PedestrianWaypoint> branches;

    public Vector3 GetPosition()
    {
        Vector3 minBound = transform.position + transform.right * width / 2f;
        Vector3 maxBound = transform.position - transform.right * width / 2f;

        return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
    }

    public int GetListLength()
    {
        return branches.Count;
    }
}
