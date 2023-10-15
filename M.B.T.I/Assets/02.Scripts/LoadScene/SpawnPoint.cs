using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject player;
    public List<Transform> spawnPoints = new List<Transform>();
    public int previousSceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 동기화
        player = GameObject.FindGameObjectWithTag("Player");
        
        // 스폰 지점 동기화
        spawnPoints.Clear();
        SceneMgr.Instance.spawnPoint.Clear();
        foreach (Transform child in transform)
        {
            spawnPoints.Add(child.transform);
            SceneMgr.Instance.spawnPoint.Add(child.transform);      // 씬 전환할때마다 동기화.
        }
        // 플레이어 위치시키기
        SetSpawnPoint();
    }
    public void SetSpawnPoint()
    {
        previousSceneIndex = SceneMgr.Instance.PreviousSceneIndex;
        // 이전 씬이 main이면 spawnPoint없이 스폰되는 걸로. 
        if (previousSceneIndex == 0)
        {
            return;
        }
        if (previousSceneIndex != 0)
        {
            // 이전 씬의 인덱스를 사용하여 작업 수행
            // 플레이어 스폰
            
            if (player != null)
            {
                print(" SetSpawnPoint");
                player.transform.position = spawnPoints[previousSceneIndex - 1].position;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
