using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    [HideInInspector]
    public float currentHealth;
    private AiAgent agent;
    private UiHealthBar healthBar;
    private AiLocomotion aiLocomotion;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<AiAgent>();
        healthBar = GetComponentInChildren<UiHealthBar>();
        aiLocomotion = GetComponent<AiLocomotion>();

        currentHealth = maxHealth;

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach(var rigidBody in rigidBodies)
        {
            HitBox hitBox = rigidBody.gameObject.AddComponent<HitBox>();
            hitBox.health = this;
        }

    }

    public void TakeDamage(float amount, Vector3 direction)
    {
        currentHealth -= amount;
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        if(currentHealth <= 0.0f)
        {
            Die(direction);
        }
    }

    private void Die(Vector3 direction)
    {
        AiDeathState deathState = agent.stateMachine.GetState(AiStateId.Death) as AiDeathState;
        deathState.direction = direction;
        agent.stateMachine.ChangeState(AiStateId.Death);
    }
}
