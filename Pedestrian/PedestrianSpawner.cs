using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSpawner : MonoBehaviour
{
    [SerializeField] GameObject pedestrianPrefab;
    [SerializeField] int pedestriansToSpawn;
    readonly string spawnParentTag = "PedParent";
    Transform pedParent;

    // Start is called before the first frame update
    void Start()
    {
        pedParent = GameObject.FindGameObjectWithTag(spawnParentTag).transform;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int count = 0;
        while (count < pedestriansToSpawn)
        {
            GameObject obj = Instantiate(pedestrianPrefab);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<PedestrianNavigation>().currentWaypoint = child.GetComponent<PedestrianWaypoint>();
            obj.transform.position = child.position;
            obj.transform.SetParent(pedParent);

            yield return new WaitForEndOfFrame();

            count++;
        }
    }
}
