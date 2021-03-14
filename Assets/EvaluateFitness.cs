using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluateFitness : MonoBehaviour
{
    Fitness fitness;
    // Start is called before the first frame update
    void Start()
    {
        fitness = gameObject.AddComponent<Fitness>();

        Invoke("PrintFitness", 10f);
    }

    // Update is called once per frame
    void PrintFitness()
    {
        Debug.Log(fitness.GetFitness());
    }
}
