using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Camera m_Camera;
    private float targetHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image holder;
    [SerializeField] private float reduceSpeed;

    [Header("shake")]
    public float shakeDuration;
    public float shakeAmount;

    private float initialYPosition;
    private float shakeTimer;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;
        initialYPosition = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - m_Camera.transform.position);
        healthBar.fillAmount = Mathf.MoveTowards(healthBar.fillAmount, targetHealth, reduceSpeed * Time.deltaTime);


    }

    public void DamageHealthBar(float health, float maxHealth)
    {
        targetHealth = health / maxHealth;
        shakeTimer = shakeDuration;
        StopCoroutine(Shake());
        StartCoroutine(Shake());
    }
    public void HealHealthBar(float health, float maxHealth)
    {
        targetHealth = health / maxHealth;
        shakeTimer = shakeDuration;
        StopCoroutine(Shake());
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        while (shakeTimer > 0)
        {
            // Déplacer la barre de vie de manière aléatoire
            transform.position = new Vector3(transform.position.x, initialYPosition + Random.Range(-shakeAmount, shakeAmount), transform.position.z);

            shakeTimer -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        
        transform.position = new Vector3(transform.position.x, initialYPosition, transform.position.z);
    }
}
