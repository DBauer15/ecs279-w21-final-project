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


    float timestamp;

    // Start is called before the first frame update
    void Start()
    {
        timestamp = -1f;
        fitness = 0;
    }

    //private void Update()
    //{
    //    if (isColliding)
    //    {
    //        if (sensorType == SensorType.POSITIVE)
    //            fitness += sensorWeight * Time.deltaTime;
    //        else
    //            fitness -= sensorWeight * Time.deltaTime;
    //    }
    //}

    public float GetFitness()
    {
        if (timestamp != -1f)
        {
            UpdateFitness();
        }

        return fitness;
    }


    void UpdateFitness()
    {
        if (sensorType == SensorType.POSITIVE)
        {
            fitness += (Time.time - timestamp) * sensorWeight;
        }
        else
        {
            fitness -= (Time.time - timestamp) * sensorWeight;
        }

        timestamp = -1f;
    }

    // Save a timestamp
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Floor"))
            return;

        isColliding = true;

        timestamp = Time.time;
    }

    // Calculate the difference between enter timestamp
    // Add difference to fitness
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Floor"))
            return;

        isColliding = false;

        UpdateFitness();
    }
}
