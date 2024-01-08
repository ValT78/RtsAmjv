using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSys;
    [SerializeField] private float emissionDuration;  // Dur�e d'�mission en secondes
    [SerializeField] private float killTime;  // Dur�e d'�mission en secondes

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
