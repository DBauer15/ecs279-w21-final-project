using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimulationManager : MonoBehaviour
{
    [Range(0.5f, 50f)]
    public float timeScale = 1f;

    [SerializeField]
    int numberOfGenerations = 10;
    [SerializeField]
    int generationSize = 50;
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
    [SerializeField]
    SimulationConfig simulationConfig;
    [SerializeField]
    SimulationLogger simulationLogger;
    

    Simulation<LimbGene, LimbStrategy, RandomProcreation<LimbGene>> simulation;

    void Start()
    {
        simulation = new Simulation<LimbGene, LimbStrategy, RandomProcreation<LimbGene>>(
            catPrefab,
            generationSize,
            survivorCutoffPercentage,
            mutationChance,
            mutationRate,
            survivorKeepPercentage,
            autoProcreation,
            spawnHeight,
            simulationConfig.config
        );

        StartCoroutine("RunSimulation");
    }

    void Update()
    {
        if(Time.timeScale != timeScale)
            Time.timeScale = timeScale;
    }

    IEnumerator RunSimulation() 
    {
        while (simulation.generationCount < numberOfGenerations) {
            // Run Simulation
            simulation.StartGeneration();
            yield return new WaitForSeconds(5f);
            simulation.CompleteGeneration();

            // Log Results
            simulationLogger.Log(simulation.generationCount, simulation.generationWorstFitness, simulation.generationBestFitness, simulation.generationMeanFitness, simulation.generationMedianFitness);

            // Update UI
            generationText.text = ("Generation " + simulation.generationCount);
            fitnessText.text = ($"Mean Fit.: {simulation.generationMeanFitness}, Best Fit.: {simulation.generationBestFitness}"); 
        }
    }

}