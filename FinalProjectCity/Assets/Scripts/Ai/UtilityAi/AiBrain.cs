using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBrain : MonoBehaviour
{
    public bool finishedDeciding { get; set; }

    public UtilityAiAction BestAction { get; set; }

    private UtilityAiAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UtilityAiAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(BestAction is null)
        {
            DecideBestAction(agent.actionsAvailable);
        }
    }

    // Loop through all the available actions 
    // Give me the highest scoring action
    public void DecideBestAction(UtilityAiAction[] actionsAvailable)
    {
        float score = 0f;
        int nextBestActionIndex = 0;
        for (int i = 0; i < actionsAvailable.Length; i++)
        {
            if (ScoreAction(actionsAvailable[i]) > score)
            {
                nextBestActionIndex = i;
                score = actionsAvailable[i].Score;
            }
        }

        BestAction = actionsAvailable[nextBestActionIndex];
        finishedDeciding = true;
    }

    // Loop through all the considerations of the action
    // Score all the considerations
    // Average the consideration scores ==> overall action score
    public float ScoreAction(UtilityAiAction action)
    {
        float score = 1f;
        for (int i = 0; i < action.considerations.Length; i++)
        {
            float considerationScore = action.considerations[i].ScoreConsideration();
            score *= considerationScore;

            if (score == 0)
            {
                action.Score = 0;
                return action.Score; // No point computing further
            }
        }

        // Averaging scheme of overall score
        float originalScore = score;
        float modFactor = 1 - (1 / action.considerations.Length);
        float makeupValue = (1 - originalScore) * modFactor;
        action.Score = originalScore + (makeupValue * originalScore);

        return action.Score;
    }
}
