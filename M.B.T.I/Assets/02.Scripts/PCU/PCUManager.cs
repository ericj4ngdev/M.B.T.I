using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCUManager : MonoBehaviour
{
    public void CompleteChallenge()
    {
        ChallengeManager.GetInstance().CompletePCUChallenge();
    }
}
