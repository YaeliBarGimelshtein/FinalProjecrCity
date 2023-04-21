using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AmmoConsideration", menuName = "Ai/UtilityAI/Considerations/Ammo Consideration")]

public class AmmoConsideration : UtilityAiConsideration
{

    public override float ScoreConsideration(UtilityAiAgent agent)
    {
        
        if (agent.weapons.HasWeapon())
        {
            int ammoCount = agent.weapons.currentWeapon.clipCount * 30 + agent.weapons.currentWeapon.ammoCount;
            if(ammoCount <= 0)
            {
                return 0.85f;
            }
        }
        return 0f;
    }
}

