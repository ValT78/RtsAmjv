using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float despawnTime;
    [SerializeField] private bool despawnAlone;
    private bool isEnemy;

    private void Start()
    {
        if(despawnAlone)
        Destroy(gameObject, despawnTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AliveObject obj) && obj.isEnemy != isEnemy)
        {
            obj.ModifyHealth(-damage);
            if (!despawnAlone)
                Destroy(gameObject, despawnTime);
        }
    }
    

    public void Initialize(bool isEnemy)
    {
        this.isEnemy = isEnemy;
    }
}
