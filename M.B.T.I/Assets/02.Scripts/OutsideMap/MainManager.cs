using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [SerializeField]
    private GameObject changeKU, changeKWU, changeKHU, changePCU, changeCAU, changeJJU;

    [SerializeField]
    private AudioSource mainBGM;

    // Start is called before the first frame update
    void Start()
    {
        mainBGM.Play();

        HashSet<string> completeChallenges = ChallengeManager.GetInstance().getCompleteChallenges();
        
        if (completeChallenges.Contains("KU"))
            changeKU.SetActive(true);
        if (completeChallenges.Contains("KWU"))
            changeKWU.SetActive(true);
        if (completeChallenges.Contains("KHU"))
            changeKHU.SetActive(true);
        if (completeChallenges.Contains("PCU"))
            changePCU.SetActive(true);
        if (completeChallenges.Contains("CAU"))
            changeCAU.SetActive(true);
        if (completeChallenges.Contains("JJU"))
            changeJJU.SetActive(true);        
    }
}
