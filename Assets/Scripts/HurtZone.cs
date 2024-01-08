using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtZone : MonoBehaviour
{
    [SerializeField] private float vitesseApparition;
    [SerializeField] private Renderer rend;
    [SerializeField] private float minAlpha;
    [SerializeField] private float maxAlpha;

    void Start()
    {
        StartCoroutine(ApparitionDisparitionRoutine());
    }

    private IEnumerator ApparitionDisparitionRoutine()
    {
        float alpha = minAlpha;
        while (alpha < maxAlpha)
        {
            SetAlpha(alpha);
            alpha += vitesseApparition * Time.deltaTime;
            yield return null;
        }

        // Disparition
        while (alpha > 0f)
        {
            alpha -= vitesseApparition * Time.deltaTime;
            SetAlpha(alpha);
            yield return null;
        }
        Destroy(gameObject);
    }

    void SetAlpha(float alpha)
    {
        Color couleurMat = rend.material.color;
        couleurMat.a = Mathf.Clamp01(alpha);
        rend.material.color = couleurMat;

    }
}

