using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UtilityAiAction : ScriptableObject
{
    public string Name;
    private float score;
    public float Score
    {
        get { return score; }
        set { score = Mathf.Clamp01(value); }
    }
    public UtilityAiConsideration[] considerations;

    public virtual void Awake()
    {
        score = 0.0f;
    }

    public abstract void Execute();
}
