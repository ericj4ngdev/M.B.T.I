using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAUManager : MonoBehaviour
{
    public void CompleteChallenge()
    {
        ChallengeManager.GetInstance().CompleteCAUChallenge();
    }
}
