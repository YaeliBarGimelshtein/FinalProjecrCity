using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackPlayerState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.AttackPlayer;
    }

    public void Enter(AiAgent agent)
    {
        agent.weapons.ActivateWeapon();
        agent.weapons.SetTarget(agent.playerTransform);
        agent.navMeshAgent.stoppingDistance = 5.0f;
    }

    public void Update(AiAgent agent)
    {
        agent.navMeshAgent.destination = agent.playerTransform.position;
        ReloadWeapon(agent);
        UpdateFiring(agent);
        if(agent.playerTransform.GetComponent<Health>().IsDead())
        {
            agent.stateMachine.ChangeState(AiStateId.Idle);
        }
    }
    private void UpdateFiring(AiAgent agent)
    {
        if (agent.sensor.IsInSight(agent.playerTransform.gameObject))
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
}
