using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AmmoConsideration", menuName = "Ai/UtilityAI/Considerations/Ammo Consideration")]

public class AmmoConsideration : UtilityAiConsideration
{
    [SerializeField] private AnimationCurve responseCurve;
    public override float ScoreConsideration(UtilityAiAgent agent)
    {
        if (agent.weapons.HasWeapon())
        {
            int ammoCount = agent.weapons.currentWeapon.clipCount * 30 + agent.weapons.currentWeapon.ammoCount;
            Score = responseCurve.Evaluate(Mathf.Clamp01(ammoCount / agent.weapons.currentWeapon.maxAmmoCount));//max ammo is 90
            return Score;
        }
        return 0f;
    }
}

