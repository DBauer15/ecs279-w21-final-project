using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fitness : MonoBehaviour
{
    FitnessSensor[] sensors;

    // Start is called before the first frame update
    void Start()
    {
        sensors = GetComponentsInChildren<FitnessSensor>();
        InvokeRepeating("PrintFitness", 0.1f, 1);
    }

    public float GetFitness()
    {
        float fitness = 0;
        foreach(FitnessSensor sensor in sensors) {
            fitness += sensor.GetFitness();
        }

        return fitness;
    }

    public void PrintFitness()
    {
        print(GetFitness());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
