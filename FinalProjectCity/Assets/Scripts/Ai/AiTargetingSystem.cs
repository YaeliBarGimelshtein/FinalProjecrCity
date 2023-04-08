using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AiTargetingSystem : MonoBehaviour
{
    AiSensoryMemory memory = new AiSensoryMemory(10);
    AiSensor sensor;

    // Start is called before the first frame update
    void Start()
    {
        sensor = GetComponent<AiSensor>();
    }

    // Update is called once per frame
    void Update()
    {
        memory.UpdateSenses(sensor);
    }

    private void OnDrawGizmos()
    {
        foreach(var memory in memory.memories)
        {
            Color color = Color.red;
            Gizmos.color = color;
            Gizmos.DrawSphere(memory.position, 0.5f);
        }
    }


}
