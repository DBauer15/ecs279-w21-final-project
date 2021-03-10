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
    private bool isColliding;
    // Start is called before the first frame update
    void Start()
    {
        fitness = 0;
    }

    private void Update()
    {
        if (isColliding)
        {
            if (sensorType == SensorType.POSITIVE)
                fitness += sensorWeight * Time.deltaTime;
            else
                fitness -= sensorWeight * Time.deltaTime;
        }
    }

    public float GetFitness()
    {
        return fitness;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Floor"))
            return;

        isColliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Floor"))
            return;

        isColliding = false;
    }
}
