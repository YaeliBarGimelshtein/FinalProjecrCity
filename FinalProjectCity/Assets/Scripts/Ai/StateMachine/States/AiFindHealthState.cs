using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFindHealthState : AiState
{
    GameObject pickup;
    GameObject[] pickups = new GameObject[1];


    public AiStateId GetId()
    {
        return AiStateId.FindHealth;
    }

    public void Enter(AiAgent agent)
    {
        pickup = null;
        agent.navMeshAgent.speed = agent.config.findHealthSpeed;
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
            agent.navMeshAgent.destination = worldBounds.RandomPosition();
        }

        if (!agent.health.IsLowHealth())
        {
            agent.stateMachine.ChangeState(AiStateId.FindTarger);
        }
    }

    public void Exit(AiAgent agent)
    {

    }

    GameObject FindPickup(AiAgent agent)
    {
        int count = agent.sensor.Filter(pickups, "Pickup", "Health");
        if (count > 0)
        {
            return pickups[0];
        }
        return null;
    }

    void CollectPickup(AiAgent agent, GameObject pickup)
    {
        agent.navMeshAgent.destination = pickup.transform.position;
    }


}
