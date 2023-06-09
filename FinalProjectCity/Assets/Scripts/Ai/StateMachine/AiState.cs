using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStateId
{
    CahsePlayer,
    Death,
    Idle,
    FindWeapon,
    AttackTarget,
    FindTarger,
    FindHealth,
    FindAmmo
}

public interface AiState 
{
    AiStateId GetId();
    void Enter(AiAgent agent);
    void Update(AiAgent agent);
    void Exit(AiAgent agent);
}
