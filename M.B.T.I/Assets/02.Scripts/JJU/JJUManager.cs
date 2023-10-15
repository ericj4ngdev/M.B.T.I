using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JJUManager : MonoBehaviour
{
    [SerializeField]
    private GameObject lanternPrefab;

    [SerializeField]
    private List<GameObject> spawnPointList;

    void Start()
    {
        InvokeRepeating("SpawnLantern", 0.3f, 2f);
    }

    public void CompleteChallenge()
    {
        ChallengeManager.GetInstance().CompleteJJUChallenge();
    }

    private void SpawnLantern()
    {
        int randomIndex = Random.Range(0, 8);
        Instantiate(lanternPrefab, spawnPointList[randomIndex].transform);
    }
}
