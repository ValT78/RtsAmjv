using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSys;
    [SerializeField] private float emissionDuration;  // Durée d'émission en secondes
    [SerializeField] private float killTime;  // Durée d'émission en secondes

    void Start()
    {
        Invoke("StopParticleEmission", emissionDuration);
    }

    void StopParticleEmission()
    {
        particleSys.Stop();
        Destroy(gameObject, killTime);

    }
}
