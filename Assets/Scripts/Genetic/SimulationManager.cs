using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SimulationManager : MonoBehaviour
{
    [Range(0.5f, 50f)]
    public float timeScale = 1f;

    [SerializeField]
    int numberOfGenerations = 10;
    [SerializeField]
    int catsPerGeneration = 50;
    [SerializeField]
    int survivorKeepPercentage = 5;
    [SerializeField]
    int survivorCutoffPercentage = 30;
    [SerializeField]
    int mutationRate = 10;
    [SerializeField]
    int mutationChance = 10;
    [SerializeField]
    bool autoProcreation = false;
    [SerializeField]
    GameObject catPrefab;
    [SerializeField]
    Text generationText, fitnessText;
    [SerializeField]
    int spawnHeight = 2;
    int generationCount;
    float generationMeanFitness, generationBestFitness = 0;
    
    List<Generation<BasicGene, BasicStrategy>> generations;

    void Start()
    {
        generationCount = 0;
        generations = new List<Generation<BasicGene, BasicStrategy>>();

        if (numberOfGenerations > 0)
            StartGeneration();
    }

    void Update()
    {
        if(Time.timeScale != timeScale)
            Time.timeScale = timeScale;

        if(generationText.text != ("Generation " + generationCount))
            generationText.text = ("Generation " + generationCount);            
            
        if(fitnessText.text != ($"Mean Fit.: {generationMeanFitness}, Best Fit.: {generationBestFitness}"))
            fitnessText.text = ($"Mean Fit.: {generationMeanFitness}, Best Fit.: {generationBestFitness}"); 
    }

    void StartGeneration(List<DNA<BasicGene>> dNAs = null)
    {
        Generation<BasicGene, BasicStrategy> generation = new Generation<BasicGene, BasicStrategy>(catPrefab, catsPerGeneration, survivorCutoffPercentage, spawnHeight, dNAs);
        generation.RunGeneration();
        generations.Add(generation);

        Invoke("CompleteGeneration", 5);
    }

    void CompleteGeneration()
    {
        Generation<BasicGene, BasicStrategy> currentGeneration = generations[generationCount];
        currentGeneration.SerializeBest();

        // get fittest dnas & mean fitness
        List<DNA<BasicGene>> fittestDNAs = currentGeneration.GetFittestDNAs();
        generationMeanFitness = currentGeneration.GetAverageFitness();
        generationBestFitness = currentGeneration.GetBestFitness();
        Debug.Log($"Mean Fit.: {generationMeanFitness}, Best Fit.: {generationBestFitness}");

        // cleanup
        currentGeneration.Cleanup();

        // keep fittest, procreate and mutate
        List<DNA<BasicGene>> newDNAs = BuildNextGeneration(fittestDNAs);

        generationCount++;

        // run next generation with new dnas
        if (generationCount < numberOfGenerations)
            StartGeneration(newDNAs);
    }

    List<DNA<BasicGene>> BuildNextGeneration(List<DNA<BasicGene>> fittestCats)
    {
        List<DNA<BasicGene>> newDNAs = new List<DNA<BasicGene>>();

        int survivorKeep = Mathf.RoundToInt(survivorKeepPercentage * Mathf.Pow(10, -2) * catsPerGeneration);
        
        // add survivors
        for (int i = 0; i < survivorKeep; i++)
        {
            newDNAs.Add(fittestCats[i]);
        }

        // procreate and mutate for the rest of population
        while (newDNAs.Count < catsPerGeneration)
        {
            // choose random fittest cats
            int randomParentIndex1 = Random.Range(0, fittestCats.Count);
            int randomParentIndex2 = Random.Range(0, fittestCats.Count);
            while(randomParentIndex1 == randomParentIndex2 && !autoProcreation)
            {
                randomParentIndex2 = Random.Range(0, fittestCats.Count);
            }

            DNA<BasicGene> dNAParent1 = fittestCats[randomParentIndex1];
            DNA<BasicGene> dNAParent2 = fittestCats[randomParentIndex2];
            // DNA<BasicGene> newDNA = dNAParent1 + dNAParent2;
            DNA<BasicGene> newDNA = dNAParent1 * dNAParent2;

            // mutate only mutationChance percentage of cats
            float randomValue = Random.Range(0, 101);

            if(randomValue < mutationChance)
                newDNA.Mutate(mutationRate);

            newDNAs.Add(newDNA);
        }

        return newDNAs;
    }

}