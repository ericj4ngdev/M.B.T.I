using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    public List<Transform> spawnPoint = new List<Transform>();
    public static SceneMgr Instance { get; private set; }
    public int PreviousSceneIndex { get; private set; }
    // public int previousSceneIndex;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        // 처음엔 Main이라 0으로 초기화. 다시 로드될떄는 awake는 호출되지 않는다. 
        PreviousSceneIndex = 0;
    }
    public void SetPreviousSceneIndex(int index)
    {
        PreviousSceneIndex = index;
        // previousSceneIndex = PreviousSceneIndex;
    }
}