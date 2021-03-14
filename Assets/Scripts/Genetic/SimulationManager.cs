using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SimulationManager : MonoBehaviour
{
    [Range(0.5f, 10f)]
    public float timeScale = 1f;

    [SerializeField]
    int numberOfGenerations = 10;
    [SerializeField]
    int catsPerGeneration = 50;
    [SerializeField]
    int survivorKeepPercentage = 25;
    [SerializeField]
    int mutationRate = 10;
    [SerializeField]
    GameObject catPrefab;
    [SerializeField]
    Text generationText, fitnessText;
    [SerializeField]
    int spawnHeight = 2;
    int generationCount;
    float generationMeanFitness = 0;
    
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
            
        if(fitnessText.text != ("Last Generation Mean Fitness " + generationMeanFitness))
            fitnessText.text = ("Last Generation Mean Fitness " + generationMeanFitness); 
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
        Generation<BasicGene, BasicStrategy> currentGeneration = generations[generationCount];
        currentGeneration.SerializeBest();

        // get fittest dnas & mean fitness
        List<DNA<BasicGene>> fittestDNAs = currentGeneration.GetFittestDNAs();
        generationMeanFitness = currentGeneration.GetAverageFitness();
        Debug.Log("Avg: " + generationMeanFitness);
        Debug.Log("Best: " + currentGeneration.GetBestFitness());

        // cleanup
        currentGeneration.Cleanup();
 
        // procreate & mutate
        List<DNA<BasicGene>> newDNAs = ProcreateAndMutate(fittestDNAs);

        generationCount++;

        // run next generation with procreated dnas
        if (generationCount < numberOfGenerations)
            StartGeneration(newDNAs);
    }

    List<DNA<BasicGene>> ProcreateAndMutate(List<DNA<BasicGene>> fittestCats)
    {
        List<DNA<BasicGene>> newDNAs = new List<DNA<BasicGene>>();

        for (int i = 0; i < catsPerGeneration; i++)
        {
            // choose random fittest cats without duplicates
            int randomParentIndex1 = Random.Range(0, fittestCats.Count);
            int randomParentIndex2 = Random.Range(0, fittestCats.Count);
            while(randomParentIndex1 == randomParentIndex2)
            {
                randomParentIndex2 = Random.Range(0, fittestCats.Count);
            }

            DNA<BasicGene> dNAParent1 = fittestCats[randomParentIndex1];
            DNA<BasicGene> dNAParent2 = fittestCats[randomParentIndex2];
            // DNA<BasicGene> newDNA = dNAParent1 + dNAParent2;
            DNA<BasicGene> newDNA = dNAParent1 * dNAParent2;

            newDNA.Mutate(mutationRate);

            newDNAs.Add(newDNA);
        }

        return newDNAs;
    }

}