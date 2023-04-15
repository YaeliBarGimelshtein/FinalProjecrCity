using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FindWeaponConsideration", menuName = "Ai/UtilityAI/Considerations/Find Weapon Consideration")]

public class FindWeaponConsideration : UtilityAiConsideration
{
    public override float ScoreConsideration()
    {
        return 0.2f;
    }
}
