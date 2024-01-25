using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloche : MonoBehaviour
{
    [SerializeField] private GameObject product;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float launchAngle = 1.57f;
    [SerializeField] private float spawnHeight = 1;
    [SerializeField] private float endHeight = 0.0f;
    private float throwDistance;


    private Vector3 targetPosition;
    private float throwDuration;
    private float v0;
    private bool isEnemy;

    public void Initialize(Vector3 targetPosition, float ProjectileSpeed, bool isEnemy)
    {
        this.targetPosition = targetPosition;
        throwDistance = Vector3.Distance(targetPosition, transform.position);
        this.throwDuration = throwDistance/ProjectileSpeed;
        this.isEnemy = isEnemy;


        transform.position += new Vector3(0f, spawnHeight, 0f);
        v0 = (gravity * throwDuration / 2 - (spawnHeight - endHeight) / throwDuration) / Mathf.Sin(launchAngle);
        StartCoroutine(LifeCycle());

    }

    IEnumerator LifeCycle()
    {
        float elapsedTime = 0f;
        Vector3 initialPos = transform.position;

        while (elapsedTime < throwDuration)
        {
            // Calculez la position actuelle du projectile en fonction du temps écoulé
            float t = elapsedTime / throwDuration;
            transform.position = CalculateNewtonPoint(t, initialPos, targetPosition);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Lorsque le projectile atteint le sol, instanciez le prefab de l'effet de touche et détruisez le projectile
        InstantiateHitEffect(targetPosition);
        Destroy(gameObject);
    }

    Vector3 CalculateNewtonPoint(float t, Vector3 launchPosition, Vector3 targetPosition)
    {
        // Interpolez la position en fonction du temps normalisé
        Vector3 interpolatedPosition = Vector3.Lerp(launchPosition, targetPosition, t);

        interpolatedPosition.y = Mathf.Sin(launchAngle) * v0 * t*throwDuration - Mathf.Pow(t*throwDuration, 2) * gravity / 2 + spawnHeight;

        return interpolatedPosition;
    }

    void InstantiateHitEffect(Vector3 position)
    {
        Vector3 pos = transform.position;
        pos.y = 0f;
        GameObject productPrefab = Instantiate(product, pos, Quaternion.identity);
        if(productPrefab.TryGetComponent(out Trap trap))
        {
            trap.Initialize(isEnemy);
        }
        else if (productPrefab.TryGetComponent(out Bait bait))
        {
            bait.Initialize(isEnemy);
        }
    }
}
