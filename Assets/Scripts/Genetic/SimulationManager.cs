using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    [SerializeField]
    int numberOfGenerations = 10;
    [SerializeField]
    int catsPerGeneration = 50;
    [SerializeField]
    int survivorKeepPercentage = 10;
    [SerializeField]
    GameObject catPrefab;
    int generationCount;
    int spawnHeight;
    
    List<Generation<BasicGene, BasicStrategy>> generations;

    void Start()
    {
        generationCount = 0;
        generations = new List<Generation<BasicGene, BasicStrategy>>();

        if (numberOfGenerations > 0)
            StartGeneration();
    }

    void StartGeneration(List<DNA<BasicGene>> dNAs = null)
    {
        Generation<BasicGene, BasicStrategy> generation = new Generation<BasicGene, BasicStrategy>(catPrefab, catsPerGeneration, survivorKeepPercentage, spawnHeight, dNAs);
        generation.RunGeneration();
        generations.Add(generation);

        Invoke("CompleteGeneration", 5);
    }

    void CompleteGeneration()
    {
        List<DNA<BasicGene>> fittestDNAs = generations[generationCount].EvaluateGeneration();
        List<DNA<BasicGene>> dNAs = Procreate(fittestDNAs);

        generationCount++;

        if (generationCount < numberOfGenerations)
            StartGeneration(dNAs);
    }

    List<DNA<BasicGene>> Procreate(List<DNA<BasicGene>> fittestCats)
    {
        // TODO
        return null;
    }

}