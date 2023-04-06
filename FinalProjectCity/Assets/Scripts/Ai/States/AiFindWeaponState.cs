using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFindWeaponState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.FindWeapon;
    }

    public void Enter(AiAgent agent)
    {
        WeaponPickup pickup = FindClosestWeapon(agent);
        agent.navMeshAgent.destination = pickup.transform.position;
        agent.navMeshAgent.speed = 5;
    }

    public void Update(AiAgent agent)
    {
        if(agent.weapons.HasWeapon())
        {
            agent.stateMachine.ChangeState(AiStateId.AttackPlayer);
        }
    }

    public void Exit(AiAgent agent)
    {
        
    }
    
    private WeaponPickup FindClosestWeapon(AiAgent agent)
    {
        WeaponPickup[] weapons = Object.FindObjectsOfType<WeaponPickup>();
        WeaponPickup closestWeapon = null;
        float closestDistance = float.MaxValue;

        foreach (var weapon in weapons)
        {
            if(weapon.gameObject.tag.Equals("PickUpRifle"))
            {
                float distanceToWeapon = Vector3.Distance(agent.transform.position, weapon.transform.position);
                if (distanceToWeapon < closestDistance)
                {
                    closestDistance = distanceToWeapon;
                    closestWeapon = weapon;
                }
            }
        }
        return closestWeapon;
    }
}