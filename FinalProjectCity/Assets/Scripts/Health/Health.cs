using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100.0f;
    [HideInInspector]
    public float currentHealth;
    public float lowHealth = 20.0f;
    private UiHealthBar healthBar;


    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponentInChildren<UiHealthBar>();

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

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        if (healthBar)
        {
            healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        }
        OnHeal(amount);
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

    public bool IsDead()
    {
        return currentHealth <= 0.0f;
    }

    public bool IsLowHealth()
    {
        return currentHealth <= lowHealth;
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

    protected virtual void OnHeal(float amount)
    {

    }
}
