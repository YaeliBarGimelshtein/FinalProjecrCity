using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float amount = 50.0f;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if(health)
        {
            health.Heal(amount);
            Destroy(gameObject);
        }
    }
}
