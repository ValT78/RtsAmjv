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
    [SerializeField] private Image sigle;
    [SerializeField] private float reduceSpeed;

    [Header("shake")]
    public float shakeDuration;
    public float shakeAmount;
    private float initialYPosition;
    private float shakeTimer;
    private IEnumerator co1;

    [Header("fade")]
    public float fadeTime;
    public float timeBeforeFade;
    private float fadeTimer;
    private IEnumerator co2;

    // Start is called before the first frame update
    void Awake()
    {
        m_Camera = Camera.main;
        initialYPosition = transform.position.y;
        co1 = Shake();
        co2 = Fade();


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
        fadeTimer = fadeTime;
        healthBar.color = new Color(255, 255, 255, 1);
        holder.color = new Color(255, 255, 255, 1);
        sigle.color = new Color(255, 255, 255, 1);
        StopCoroutine(co1);
        StopCoroutine(co2);
        co1 = Shake();
        co2 = Fade();
        StartCoroutine(co1);
        StartCoroutine(co2);
    }
    public void HealHealthBar(float health, float maxHealth)
    {
        targetHealth = health / maxHealth;
        fadeTimer = fadeTime;
        healthBar.color = new Color(255, 255, 255, 1);
        holder.color = new Color(255, 255, 255, 1);
        sigle.color = new Color(255, 255, 255, 1);
        StopCoroutine(co2);
        co2 = Fade();
        StartCoroutine(Fade());

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

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(timeBeforeFade);
        while (fadeTimer > 0)
        {
            fadeTimer -= Time.deltaTime;

            if (fadeTimer >= 0)
            {
                float alpha = Mathf.Lerp(1f, 0f, (fadeTime - fadeTimer) / fadeTime);
                healthBar.color = new Color(255, 255, 255, alpha);
                holder.color = new Color(255, 255, 255, alpha);
                sigle.color = new Color(255, 255, 255, alpha);
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
