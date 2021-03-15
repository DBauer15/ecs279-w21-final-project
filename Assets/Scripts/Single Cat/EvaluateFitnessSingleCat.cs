using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluateFitness : MonoBehaviour
{
    Fitness fitness;

    void Start()
    {
        fitness = gameObject.AddComponent<Fitness>();

        Invoke("PrintFitness", 10f);
    }
    void PrintFitness()
    {
        Debug.Log(fitness.GetFitness());
    }
}
