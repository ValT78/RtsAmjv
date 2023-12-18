using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private int damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<AliveObject>(out AliveObject obj) && obj.isEnemy)
        {
            obj.ModifyHealth(-damage);
            Destroy(gameObject);
        }
    }
}
