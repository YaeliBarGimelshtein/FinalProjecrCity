using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FindWeaponConsideration", menuName = "Ai/UtilityAI/Considerations/Find Weapon Consideration")]
public class FindWeaponConsideration : UtilityAiConsideration
{
    public override float ScoreConsideration(UtilityAiAgent agent)
    {
        if (agent.weapons.HasWeapon())
        {
            return 0f;
        }
        return 0.8f;//prioratize weapon if agent dosnt have one didnt put higher to let health be prioratize too just in case
    }
}
