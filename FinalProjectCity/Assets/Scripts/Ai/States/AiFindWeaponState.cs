using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFindWeaponState : AiState
{
    GameObject pickup;
    GameObject[] pickups = new GameObject[1];


    public AiStateId GetId()
    {
        return AiStateId.FindWeapon;
    }

    public void Enter(AiAgent agent)
    {
        pickup = null;
        agent.navMeshAgent.speed = 5;
    }

    public void Update(AiAgent agent)
    {
        // Find pickup
        if (!pickup)
        {
            pickup = FindPickup(agent);

            if (pickup)
            {
                CollectPickup(agent, pickup);
            }
        }
        
        // Wander
        if (!agent.navMeshAgent.hasPath && !pickup)// added !pickup to fix soldier not taking gun if it is infront of him at the start of the game
        {
            WorldBounds worldBounds = GameObject.FindObjectOfType<WorldBounds>();
            Vector3 min = worldBounds.min.position;
            Vector3 max = worldBounds.max.position;

            Vector3 randomPosition = new Vector3(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y),
                Random.Range(min.z, max.z)
                );
            agent.navMeshAgent.destination = randomPosition;
        }

        if(agent.weapons.HasWeapon())
        {
            agent.stateMachine.ChangeState(AiStateId.AttackPlayer);
        }
    }

    public void Exit(AiAgent agent)
    {
        
    }
    
    GameObject FindPickup(AiAgent agent)
    {
        int count = agent.sensor.Filter(pickups, "Pickup");
        if(count > 0)
        {
            return pickups[0];
        }
        return null;
    }

    void CollectPickup(AiAgent agent, GameObject pickup)
    {
        agent.navMeshAgent.destination = pickup.transform.position;
    }



    /*private WeaponPickup FindClosestWeapon(AiAgent agent)
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
    }*/



}
