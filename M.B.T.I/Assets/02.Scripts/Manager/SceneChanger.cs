using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ToMain()
    {
        SceneManager.LoadScene("MBTI_Main_Net");
    }

    public void ToCAU()
    {
        SceneManager.LoadScene("MBTI_CAU_Net");
    }
    public void ToJJU()
    {
        SceneManager.LoadScene("MBTI_JJU_Net");
    }

    public void ToKHU()
    {
        SceneManager.LoadScene("MBTI_KHU_Net");
    }
    public void ToKU()
    {
        SceneManager.LoadScene("MBTI_KU_Net");
    }

    public void ToKWU()
    {
        SceneManager.LoadScene("MBTI_KWU_Net");
    }

    public void ToPCU()
    {
        SceneManager.LoadScene("MBTI_PCU_Net");
    }

}
