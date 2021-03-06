using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

class Simulation <G,S,P> where G : Gene, new() where S : MonoBehaviour, Strategy<G> where P : Procreation<G>, new()
{
    GameObject catPrefab;
    int generationSize;
    int survivorCutoffPercentage;
    int mutationChance;
    int mutationRate;
    int survivorKeepPercentage;
    bool autoProcreation;
    int spawnHeight;
    Dictionary<string, object> config;

    public P procreation;
    public List<Generation<G,S>> generations;
    public int generationCount;
    public float generationWorstFitness, generationMeanFitness, generationBestFitness, generationMedianFitness;

    public List<DNA<G>> dNAs;

    public Simulation(GameObject catPrefab, int generationSize, int survivorCutoffPercentage, int mutationChance, int mutationRate, int survivorKeepPercentage, bool autoProcreation, int spawnHeight, Dictionary<string, object> config) {

        this.catPrefab = catPrefab;
        this.generationSize = generationSize;
        this.survivorCutoffPercentage = survivorCutoffPercentage;
        this.mutationChance = mutationChance;
        this.mutationRate = mutationRate;
        this.survivorKeepPercentage = survivorKeepPercentage;
        this.autoProcreation = autoProcreation;
        this.spawnHeight = spawnHeight;
        this.config = config;

        procreation = new P();
        generations = new List<Generation<G, S>>();
    }

    public void StartGeneration()
    {
        Generation<G, S> generation = new Generation<G, S>(catPrefab, generationSize, survivorCutoffPercentage, spawnHeight, config, dNAs);
        generation.RunGeneration();
        generations.Add(generation);
    }

    public void CompleteGeneration()
    {
        Generation<G, S> currentGeneration = generations[generationCount];
        currentGeneration.SerializeBest();

        // get fittest dnas & mean fitness
        List<DNA<G>> fittestDNAs = currentGeneration.GetFittestDNAs();
        generationWorstFitness = currentGeneration.GetWorstFitness();
        generationMeanFitness = currentGeneration.GetAverageFitness();
        generationBestFitness = currentGeneration.GetBestFitness();
        generationMedianFitness = currentGeneration.GetMedianFitness();
        Debug.Log($"Mean Fit.: {generationMeanFitness}, Best Fit.: {generationBestFitness}");

        // cleanup
        currentGeneration.Cleanup();

        // keep fittest, procreate and mutate
        dNAs = procreation.BuildNextGeneration(fittestDNAs, generationSize, survivorKeepPercentage, mutationChance, mutationRate, autoProcreation);

        generationCount++;
    }
}