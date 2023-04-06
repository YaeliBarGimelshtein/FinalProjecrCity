using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    [HideInInspector]
    public float currentHealth;
    private UiHealthBar healthBar;
    private AiLocomotion aiLocomotion;


    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponentInChildren<UiHealthBar>();
        aiLocomotion = GetComponent<AiLocomotion>();

        currentHealth = maxHealth;

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach(var rigidBody in rigidBodies)
        {
            HitBox hitBox = rigidBody.gameObject.AddComponent<HitBox>();
            hitBox.health = this;
            if(hitBox.gameObject != gameObject)
            {
                hitBox.gameObject.layer = LayerMask.NameToLayer("Hitbox");
            }
        }
        OnStart();
    }

    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;
        if(healthBar)
        {
            healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        }
        OnDamage(direction);
        if(currentHealth <= 0.0f)
        {
            Die(direction);
        }
    }

    private void Die(Vector3 direction)
    {
        OnDeath(direction);
    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnDeath(Vector3 direction)
    {

    }

    protected virtual void OnDamage(Vector3 direction)
    {

    }
}
