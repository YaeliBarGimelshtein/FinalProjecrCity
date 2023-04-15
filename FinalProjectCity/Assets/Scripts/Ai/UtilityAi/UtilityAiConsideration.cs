using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UtilityAiConsideration : ScriptableObject
{
    public string Name;
    private float score;
    public float Score
    {
        get { return score; }
        set { score = Mathf.Clamp01(value); }
    }

    public virtual void Awake()
    {
        score = 0.0f;
    }

    public abstract float ScoreConsideration(UtilityAiAgent agent);
}
