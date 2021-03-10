using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SensorType
{
    POSITIVE,
    NEGATIVE
}

public class FitnessSensor : MonoBehaviour
{

    public SensorType sensorType = SensorType.POSITIVE;
    public float sensorWeight = 1.0f;
    private float fitness;

    // Start is called before the first frame update
    void Start()
    {
        fitness = 0;
    }

    public float GetFitness()
    {
        return fitness;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Floor"))
            return;

        if (sensorType == SensorType.POSITIVE)
            fitness += sensorWeight * Time.deltaTime;
        else
            fitness -= sensorWeight * Time.deltaTime;
    }
}
