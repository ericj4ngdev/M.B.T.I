using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    private static int challengeCount;

    [SerializeField]
    private AudioSource completeSound;


    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Challenges"))
        {
            Debug.Log("PlayerPref 생성");
            challengeCount = 0;
            PlayerPrefs.SetInt("Challenges", challengeCount);
        }

        completeSound = GetComponent<AudioSource>();
    }
    
    public void CompleteChallenge()
    {
        PlayerPrefs.SetInt("Challenges", ++challengeCount);

        if (PlayerPrefs.GetInt("Challenges") == 6)
        {
            Debug.Log("불꽃놀이");
        }
    }

}
