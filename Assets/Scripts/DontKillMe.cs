using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontKillMe : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
