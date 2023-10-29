using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject lanternPrefab;

    // Start is called before the first frame update
    void Start()
    {
        float randomDuration = Random.Range(1, 3);
        InvokeRepeating("SpawnLantern", randomDuration, randomDuration);
    }

    private void SpawnLantern()
    {
        Instantiate(lanternPrefab, transform);
    }
}
