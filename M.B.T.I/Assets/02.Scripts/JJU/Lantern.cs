using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField]
    private GameObject lantern, lanternVisual;
    private float randomYRotation;
    [SerializeField]
    private AudioSource collisionSound;
    [SerializeField]
    private ParticleSystem particle;

    void Start()
    {
        randomYRotation = Random.Range(0f, 360f);

        lantern.transform.rotation = Quaternion.Euler(0f, randomYRotation, 0f);
        Destroy(lantern, 10f);
    }

    public void OnTriggerEnter(Collider collision)
    {
        ChallengeManager.GetInstance().CompleteJJUChallenge();
        lanternVisual.SetActive(false);
        collisionSound.Play();
        particle.Play();

        Debug.Log("충돌");

    }
}
