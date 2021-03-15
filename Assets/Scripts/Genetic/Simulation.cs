using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

class Simulation <G,S> where G : Gene, new() where S : MonoBehaviour, Strategy<G>
{
    GameObject catPrefab;
    int catsPerGeneration;
    int survivorCutoffPercentage;
    int mutationChance;
    int mutationRate;
    int survivorKeepPercentage;
    bool autoProcreation;
    int spawnHeight;

    public List<Generation<G,S>> generations;
    public int generationCount;
    public float generationMeanFitness, generationBestFitness;

    public List<DNA<G>> dNAs;

    public Simulation(GameObject catPrefab, int catsPerGeneration, int survivorCutoffPercentage, int mutationChance, int mutationRate, int survivorKeepPercentage, bool autoProcreation, int spawnHeight) {

        this.catPrefab = catPrefab;
        this.catsPerGeneration = catsPerGeneration;
        this.survivorCutoffPercentage = survivorCutoffPercentage;
        this.mutationChance = mutationChance;
        this.mutationRate = mutationRate;
        this.survivorKeepPercentage = survivorKeepPercentage;
        this.autoProcreation = autoProcreation;
        this.spawnHeight = spawnHeight;

        generations = new List<Generation<G, S>>();
    }

    public void StartGeneration()
    {
        Debug.Log("Running Generation");

        Generation<G, S> generation = new Generation<G, S>(catPrefab, catsPerGeneration, survivorCutoffPercentage, spawnHeight, dNAs);
        generation.RunGeneration();
        generations.Add(generation);
    }

    public void CompleteGeneration()
    {
        Debug.Log("Finished Generation");

        Generation<G, S> currentGeneration = generations[generationCount];
        currentGeneration.SerializeBest();

        // get fittest dnas & mean fitness
        List<DNA<G>> fittestDNAs = currentGeneration.GetFittestDNAs();
        generationMeanFitness = currentGeneration.GetAverageFitness();
        generationBestFitness = currentGeneration.GetBestFitness();
        Debug.Log($"Mean Fit.: {generationMeanFitness}, Best Fit.: {generationBestFitness}");

        // cleanup
        currentGeneration.Cleanup();

        // keep fittest, procreate and mutate
        dNAs = BuildNextGeneration(fittestDNAs);

        generationCount++;
    }

    public List<DNA<G>> BuildNextGeneration(List<DNA<G>> fittestCats)
    {
        List<DNA<G>> newDNAs = new List<DNA<G>>();

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

            DNA<G> dNAParent1 = fittestCats[randomParentIndex1];
            DNA<G> dNAParent2 = fittestCats[randomParentIndex2];
            // DNA<BasicGene> newDNA = dNAParent1 + dNAParent2;
            DNA<G> newDNA = dNAParent1 * dNAParent2;

            // mutate only mutationChance percentage of cats
            float randomValue = Random.Range(0, 101);

            if(randomValue < mutationChance)
                newDNA.Mutate(mutationRate);

            newDNAs.Add(newDNA);
        }

        return newDNAs;
    }
}