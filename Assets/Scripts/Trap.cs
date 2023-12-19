using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private int damage;
    private bool isEnemy;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<AliveObject>(out AliveObject obj) && obj.isEnemy != isEnemy)
        {
            obj.ModifyHealth(-damage);
            Destroy(gameObject);
        }
    }

    public void Initialize(bool isEnemy)
    {
        this.isEnemy = isEnemy;
    }
}
