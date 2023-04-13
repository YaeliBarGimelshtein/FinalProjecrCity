using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackTargetState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.AttackTarget;
    }

    public void Enter(AiAgent agent)
    {
        agent.weapons.ActivateWeapon();
        agent.navMeshAgent.stoppingDistance = agent.config.attackStoppingDistance;
        agent.navMeshAgent.speed = agent.config.attackSpeed;
    }

    public void Update(AiAgent agent)
    {
        if (!agent.targetingSystem.HasTarget)
        {
            agent.stateMachine.ChangeState(AiStateId.FindTarger);
            return;
        }

        agent.weapons.SetTarget(agent.targetingSystem.Target.transform);
        agent.navMeshAgent.destination = agent.targetingSystem.TargetPosition;

        ReloadWeapon(agent);
        UpdateFiring(agent);
        UpdateLowHealth(agent);
    }

    private void UpdateFiring(AiAgent agent)
    {
        if (agent.targetingSystem.TargetInSight)
        {
            agent.weapons.SetFiring(true);
        }
        else
        {
            agent.weapons.SetFiring(false);
        }
    }

    public void Exit(AiAgent agent)
    {
        agent.weapons.DeactivateWeapon();
        agent.navMeshAgent.stoppingDistance = 0.0f;
    }

    private void ReloadWeapon(AiAgent agent)
    {
        var weapon = agent.weapons.currentWeapon;
        if(weapon && weapon.ammoCount <= 0)
        {
            agent.weapons.ReloadWeapon();
        }
    }

    private void UpdateLowHealth(AiAgent agent)
    {
        if(agent.health.IsLowHealth())
        {
            agent.stateMachine.ChangeState(AiStateId.FindHealth);
        }
    }
}
