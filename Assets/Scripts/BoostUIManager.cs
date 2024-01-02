using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostUIManager : MonoBehaviour
{
    private Camera m_Camera;

    [SerializeField] private List<Image> sprites = new();
    [SerializeField] private float x_space;
    [SerializeField] private float y_space;
    private bool[] activeSprites;

    private void Awake()
    {
        m_Camera = Camera.main;
        activeSprites = new bool[] { false, false, false} ;
        foreach (Image image in sprites)
        {
            image.enabled = false;
        }
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - m_Camera.transform.position);
    }

    public void ActivateBoost(int boost)
    {
        activeSprites[boost] = !activeSprites[boost];
        sprites[boost].enabled = activeSprites[boost];
        int odd = 0;
        for (int i = 0; i < activeSprites.Length; i++)
        {
            if (activeSprites[i])
            {
                sprites[i].rectTransform.anchoredPosition = new(odd*x_space,y_space);
                odd++;
            }
        }
        foreach (Image image in sprites)
        {
            image.rectTransform.anchoredPosition = image.rectTransform.anchoredPosition - new Vector2((odd-1)*x_space / 2, 0);
        }


    }




}
