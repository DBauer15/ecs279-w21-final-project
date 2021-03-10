using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    [SerializeField]
    int numberOfGenerations = 10; 
    [SerializeField]
    int catsPerGeneration = 50;
    [SerializeField]
    GameObject catPrefab;
    int generationCount;
    int spawnHeight;
    List<Generation> generations;

    void Start()
    {
        generationCount = 1;
        generations = new List<Generation>();

        if(numberOfGenerations > 0)
            InitGeneration();
    }

    void InitGeneration()
    {
        Generation generation = new Generation(catPrefab, catsPerGeneration, spawnHeight);
        generation.RunGeneration();
        generations.Add(generation);
        generation.generationFinishedEvent.AddListener(RunCompleted);
    }

    void RunCompleted()
    {
        generationCount++;

        if(generationCount < numberOfGenerations)
            InitGeneration();
    }

}