using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JJUManager : MonoBehaviour
{
    [SerializeField]
    private GameObject lanternPrefab;
    [SerializeField]
    private Transform spawnPoint;
    private float spawnTransformX;
    private float spawnTransformZ;
    private float randomYRotation;

    private void Start()
    {
       
        InvokeRepeating("SpawnLantern", 0f, 1f);
    }
    public void CompleteChallenge()
    {
        ChallengeManager.GetInstance().CompleteJJUChallenge();
    }

    private void SpawnLantern()
    {
        spawnTransformX = Random.Range(4f, 17f);
        spawnTransformZ = Random.Range(32f, 37f);
        randomYRotation = Random.Range(0f, 360f);

        Vector3 startPosition = new Vector3(spawnTransformX, 2, spawnTransformZ);

        spawnPoint.position = startPosition;

        Instantiate(lanternPrefab, spawnPoint);
    }
}
