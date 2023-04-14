using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AiTargetingSystem : MonoBehaviour
{
    public float memorySpan = 3.0f;
    public float distanceWeight = 1.0f;
    public float angleWeight = 1.0f;
    public float ageWeight = 1.0f;

    public bool HasTarget
    {
        get { return bestMemory != null; }
    }

    public GameObject Target
    {
        get { return bestMemory.gameObject; }
    }

    public Vector3 TargetPosition
    {
        get { return bestMemory.gameObject.transform.position; }
    }

    public bool TargetInSight
    {
        get { return bestMemory.Age < 0.5f; }
    }

    public float TargetDistance
    {
        get { return bestMemory.distance; }
    }

    private AiSensoryMemory memory = new AiSensoryMemory(10);
    private AiSensor sensor;
    private AiMemory bestMemory;

    // Start is called before the first frame update
    void Start()
    {
        sensor = GetComponent<AiSensor>();
    }

    // Update is called once per frame
    void Update()
    {
        memory.UpdateSenses(sensor);
        memory.ForgetMemories(memorySpan);

        EvaluateScores();
    }

    private void EvaluateScores()
    {
        bestMemory = null;

        foreach(var memory in memory.memories)
        {
            memory.score = CalculateScore(memory);
            if(bestMemory == null || memory.score > bestMemory.score)
            {
                bestMemory = memory;
            }
        }
    }

    private float Normalize(float value, float maxValue)
    {
        return 1.0f - (value / maxValue);
    }

    private float CalculateScore(AiMemory memory)
    {
        float distanceScore =  Normalize(memory.distance, sensor.distance) * distanceWeight;
        float angleScore = Normalize(memory.angle, sensor.angle) * angleWeight;
        float ageScore = Normalize(memory.Age, memorySpan) * ageWeight;
        return distanceScore + angleScore + ageScore;
    }

    private void OnDrawGizmos()
    {
        float maxScore = float.MinValue;
        foreach(var memory in memory.memories)
        {
            maxScore = Mathf.Max(maxScore, memory.score);
        }

        foreach(var memory in memory.memories)
        {
            Color color = Color.red;
            if(memory == bestMemory)
            {
                color = Color.magenta;
            }
            color.a = memory.score / maxScore;
            Gizmos.color = color;
            Gizmos.DrawSphere(memory.position, 0.2f);
        }
    }


}
