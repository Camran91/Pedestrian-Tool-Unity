using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianStopGoManager : MonoBehaviour
{
    [SerializeField] float timerMin = 10f;
    [SerializeField] float timerMax = 30f;
    public float curWaitTime;
    public bool stopWalking = false;

    private void Start()
    {
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        curWaitTime = Random.Range(timerMin, timerMax);

        while (curWaitTime > 0.0f)
        {
            curWaitTime -= Time.deltaTime;
            yield return null;
        }
        stopWalking = !stopWalking;
        StartCoroutine(StartTimer());
    }

    public float GetCurWaitTime()
    {
        return curWaitTime;
    }

    public bool GetStopWalking()
    {
        return stopWalking;
    }
}
