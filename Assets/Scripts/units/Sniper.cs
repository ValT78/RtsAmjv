using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : AttackController
{
    [SerializeField] private float throwHeight; // La hauteur du lancer en cloche
    [SerializeField] private float throwDuration; // La durée totale du lancer
    [SerializeField] private GameObject trap; // Le prefab du projectile à lancer

    public override void SpecialAttack(Vector3 targetPosition)
    {
        // Instanciez le projectile à la position du joueur (ajustez cela en fonction de votre besoin)
        GameObject trapPrefab = Instantiate(trap, transform.position + new Vector3(0f, 3f, 0f), Quaternion.identity);
        // Démarrez une coroutine pour gérer le mouvement en cloche du projectile
        StartCoroutine(LaunchProjectileCoroutine(trapPrefab, targetPosition));
    }

    IEnumerator LaunchProjectileCoroutine(GameObject trap, Vector3 targetPosition)
    {
        float elapsedTime = 0f;

        while (elapsedTime < throwDuration * 3 / 4)
        {
            // Calculez la position actuelle du projectile en fonction du temps écoulé
            float t = elapsedTime / throwDuration;
            trap.transform.position = CalculateBezierPoint(t, trap.transform.position, targetPosition, throwHeight);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Lorsque le projectile atteint le sol, instanciez le prefab de l'effet de touche et détruisez le projectile
        InstantiateHitEffect(trap.transform.position);
        Destroy(trap);
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, float height)
    {
        // Formule du mouvement en cloche de Bezier
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * (p0 + Vector3.up * height);
        p += 3 * u * tt * (p1 + Vector3.up * height);
        p += ttt * p1;

        return p;
    }

    void InstantiateHitEffect(Vector3 position)
    {
        // Instanciez ici le prefab de l'effet de touche au sol
        // Assurez-vous de configurer correctement cet effet
        // par exemple, une explosion, des particules, etc.
    }
}
