using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class PortalTest : MonoBehaviour
{
    public int nextSceneIdx;

    [Range(0, 100)] public float percent;
    public float timer;
    public float fakeLoadingTime = 1f; // 페이크 로딩 시간 설정 (초 단위)
    
    // trigger되었을때 LoadScene호출
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 이전 씬의 인덱스 저장
            Debug.Log("현재 씬 인덱스 : " + SceneManager.GetActiveScene().buildIndex);
            SceneMgr.Instance.SetPreviousSceneIndex(SceneManager.GetActiveScene().buildIndex);
            other.GetComponent<FirstPersonMovement>().speed = 1;        // 닿는 순간 속도 제한
            LoadNextScene(nextSceneIdx);
        }
    }
    
    public void LoadNextScene(int sceneBuildIndex)
    {
        // 비동기적으로 Scene을 불러오기 위해 Coroutine을 사용한다.
        StartCoroutine(FadeOut());
        StartCoroutine(LoadMyAsyncScene(sceneBuildIndex));
    }
    public Image fadeImage;
    public float fadeDuration = 1.0f;
    private IEnumerator FadeOut()
    {
        Color originalColor = fadeImage.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            fadeImage.color = Color.Lerp(originalColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = targetColor;
    }
    
    IEnumerator LoadMyAsyncScene(int idx)
    {    
        // AsyncOperation을 통해 Scene Load 정도를 알 수 있다.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(idx);
        asyncLoad.allowSceneActivation = false;
        // SceneManager.LoadSceneAsync(idx);
        timer = 0;
        float fakeLoadingDuration = 1f / fakeLoadingTime; // 페이크 로딩 시간의 역수 계산
        
        // Scene을 불러오는 것이 완료될 때까지 대기한다.
        while (!asyncLoad.isDone)
        {
            // 진행상황 확인
            if (asyncLoad.progress < 0.9f)
            {
                percent = asyncLoad.progress * 100f;
            }
            else
            {
                // 1초간 페이크 로딩
                // 페이크 로딩
                timer += Time.deltaTime * fakeLoadingDuration;
                percent = Mathf.Lerp(90f, 100f, timer);
                if (percent >= 100)
                {
                    asyncLoad.allowSceneActivation = true;
                    yield break;
                }
            }
            yield return null;
        }
    }
    
}
