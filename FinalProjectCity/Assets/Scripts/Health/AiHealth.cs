using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHealth : Health
{
    private AiAgent agent;

    protected override void OnStart()
    {
        agent = GetComponent<AiAgent>();
    }

    protected override void OnDeath(Vector3 direction)
    {
        try
        {
            AiDeathState deathState = agent.stateMachine.GetState(AiStateId.Death) as AiDeathState;
            deathState.direction = direction;
            agent.stateMachine.ChangeState(AiStateId.Death);

            var singelton = GameObject.FindObjectOfType<GameMode>();
            if (singelton)
            {
                singelton.DeadSoldiers += 1;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected override void OnDamage(Vector3 direction)
    {

    }
}
