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
    [SerializeField] private RectTransform sigle;

    [Header("Smooth Indicator")]
    [SerializeField] private Image healBar;
    [SerializeField] private Sprite greenBar;
    [SerializeField] private Sprite orangeBar;
    [SerializeField] private float reduceSpeed;
    private bool lastInteraction;

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
        targetHealth = 1;
        healthBar.color = new Color(255, 255, 255, 0);
        holder.color = new Color(255, 255, 255, 0);
        healBar.color = new Color(255, 255, 255, 0);

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - m_Camera.transform.position);
        if(!lastInteraction)
        {
            healthBar.fillAmount = Mathf.MoveTowards(healthBar.fillAmount, targetHealth, reduceSpeed * Time.deltaTime);
        }
        else
        {
            healBar.fillAmount = Mathf.MoveTowards(healBar.fillAmount, targetHealth, reduceSpeed * Time.deltaTime);

        }


    }

    
    public void DamageHealthBar(float health, float maxHealth)
    {
        sigle.anchoredPosition = new(-1117f, 0);
        targetHealth = health / maxHealth;
        healthBar.fillAmount = targetHealth;
        healBar.sprite = orangeBar;
        lastInteraction = true;
        shakeTimer = shakeDuration;
        fadeTimer = fadeTime;
        healthBar.color = new Color(255, 255, 255, 1);
        holder.color = new Color(255, 255, 255, 1);
        healBar.color = new Color(255, 255, 255, 1);
        StopCoroutine(co1);
        co1 = Shake();
        StartCoroutine(co1);
    }
    public void HealHealthBar(float health, float maxHealth)
    {
        sigle.anchoredPosition = new(-1117f, 0);
        targetHealth = health / maxHealth;
        fadeTimer = fadeTime;
        healBar.fillAmount = targetHealth; 
        healBar.sprite = greenBar;
        lastInteraction = false;
        healthBar.color = new Color(255, 255, 255, 1);
        holder.color = new Color(255, 255, 255, 1);
        healBar.color = new Color(255, 255, 255, 1);
        StopCoroutine(co2);
        if (targetHealth == 1)
        {
            co2 = Fade();
            StartCoroutine(Fade());
        }
        

    }

    private IEnumerator Shake()
    {

        while (shakeTimer > 0)
        {
            // Déplacer la barre de vie de manière aléatoire
            transform.position = new Vector3(transform.position.x, initialYPosition + Random.Range(-shakeAmount, shakeAmount), transform.position.z);

            shakeTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
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
                healBar.color = new Color(255, 255, 255, alpha);
            }
            yield return new WaitForFixedUpdate();
        }
        sigle.anchoredPosition = new(0f, 0);

    }
}
